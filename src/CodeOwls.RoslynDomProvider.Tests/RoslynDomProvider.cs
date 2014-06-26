using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using RoslynDom.Common;
using Xunit;

namespace CodeOwls.RoslynDomProvider.Tests
{
    public class RoslynDomProvider 
    {
        public RoslynDomProvider()
        {
            _codePath = Path.GetTempFileName();
            File.WriteAllText(_codePath, CSharpCode);
        }

        [Fact]
        public void Can_Load_Test_Script()
        {
            using (var ps = CreateNewPowerShell())
            {
                ps.Invoke();
                if (ps.HadErrors)
                {
                    var errors = ps.Streams.Error.ToList();
                    Assert.False(ps.HadErrors);
                }
            }
        }

        [Fact]
        public void Can_GetItem_a_Namespace()
        {
            var script = @"get-item code:\Namespaces\MyNamespace";
            var result = ExecuteScript(script);

            AssertFirstResultType(result, typeof(INamespace) );
        }

        [Fact]
        public void Can_GetItem_a_Class()
        {
            var script = @"get-item code:\Namespaces\MyNamespace\classes\MyClass";
            var result = ExecuteScript(script);

            AssertFirstResultType(result, typeof(IClass));
        }

        [Fact]
        public void Can_GetItem_an_Attribute()
        {
            var script = @"get-item code:\Namespaces\MyNamespace\classes\MyClass\attributes\Serializable";
            var result = ExecuteScript(script);

            AssertFirstResultType(result, typeof(IAttribute));
        }

        [Fact]
        public void Can_GetItem_a_Method()
        {
            var script = @"get-item code:\Namespaces\MyNamespace\classes\MyClass\methods\MyMethod";
            var result = ExecuteScript(script);

            AssertFirstResultType(result, typeof(IMethod));
        }

        [Fact]
        public void Can_GetItem_a_Property()
        {
            var script = @"get-item code:\Namespaces\MyNamespace\classes\MyClass\properties\MyProperty";
            var result = ExecuteScript(script);

            AssertFirstResultType(result, typeof(IProperty));
        }

        [Fact]
        public void Can_GetItem_a_Method_Parameter()
        {
            var script = @"get-item code:\Namespaces\MyNamespace\classes\MyClass\methods\MyMethod\parameters\value";
            var result = ExecuteScript(script);

            AssertFirstResultType(result, typeof(IParameter));
        }


        private static void AssertFirstResultType(Collection<PSObject> result, Type type)
        {
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Count);
            Assert.NotNull(result.FirstOrDefault());
            Assert.IsAssignableFrom(type, result.First().BaseObject);
        }

        private Collection<PSObject> ExecuteScript(string script)
        {
            using (var ps = CreateNewPowerShell())
            {
                ps.AddScript(script);
                var result = ps.Invoke();

                if (ps.HadErrors)
                {
                    var errors = ps.Streams.Error.ToList();
                    Assert.False(ps.HadErrors);
                }

                Assert.NotNull(result);
            
                return result;
            }
        }

        private string PowerShellSetupCode
        {
            get
            {
                
                return string.Format(@"
[System.Environment]::currentDirectory | set-location;
get-childitem codeowls.roslyndomprovider.dll | import-module;
new-psdrive -name code -psp roslyndom -root '{0}';
",
                    _codePath);
            }
        }

        private readonly string _codePath;
        const string CSharpCode = @"
using System;
namespace MyNamespace { 
    [Serializable]
    public class MyClass{ 
        public int MyProperty { get;set; }
        void MyMethod( string value ) { 
            Console.WriteLine(value); 
        } 
    } 
}";

        PowerShell CreateNewPowerShell()
        {
            var ps = PowerShell.Create();
            ps.AddScript(PowerShellSetupCode);
            return ps;
        }
    }
}
