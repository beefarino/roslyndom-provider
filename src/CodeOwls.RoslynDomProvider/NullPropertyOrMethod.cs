using System.Collections.Generic;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    internal class NullPropertyOrMethod : IPropertyOrMethod
    {
        public object RawItem { get; private set; }
        public string Name { get; private set; }
        public string QualifiedName { get; private set; }
        public string OuterName { get; private set; }
        public string Namespace { get; private set; }
        public object RequestValue(string name)
        {
            return null;
        }

        public IEnumerable<IAttribute> Attributes { get; private set; }
        public AccessModifier AccessModifier { get; private set; }
        public IReferencedType ReturnType { get; private set; }
        public bool IsStatic { get; private set; }
        public bool IsAbstract { get; private set; }
        public bool IsVirtual { get; private set; }
        public bool IsOverride { get; private set; }
        public bool IsSealed { get; private set; }
        public IEnumerable<IParameter> Parameters { get { return new IParameter[0]; } }
        public MemberType MemberType { get; private set; }
    }
}