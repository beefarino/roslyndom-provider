using System.Collections.Generic;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    internal class NullTypeMemberContainer : ITypeMemberContainer
    {
        public IEnumerable<ITypeMember> Members { get { return new ITypeMember[0]; } }
        public IEnumerable<IProperty> Properties { get{ return new IProperty[0]; } }
        public IEnumerable<IMethod> Methods { get { return new IMethod[0]; } }
        public IEnumerable<IField> Fields { get { return new IField[0]; } }
    }
}