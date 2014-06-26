using System.Collections.Generic;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    internal class NullHasAttributes : IHasAttributes
    {
        public IEnumerable<IAttribute> Attributes { get { return new IAttribute[0]; } }
    }
}