using System.Collections.Generic;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    internal class NullAttribute : IAttribute
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

        public IEnumerable<IAttributeValue> AttributeValues { get { return new IAttributeValue[0]; } }
    }
}