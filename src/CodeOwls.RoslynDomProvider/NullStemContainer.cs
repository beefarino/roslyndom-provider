using System.Collections.Generic;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{

    internal class NullStemContainer : IStemContainer
    {
        public IEnumerable<IStemMember> Types { get { return new IStemMember[0]; } }
        public IEnumerable<IClass> Classes { get { return new IClass[0]; } }
        public IEnumerable<IStructure> Structures { get { return new IStructure[0]; } }
        public IEnumerable<IInterface> Interfaces { get { return new IInterface[0]; } }
        public IEnumerable<IEnum> Enums { get { return new IEnum[0]; } }
        public IEnumerable<IUsing> Usings { get { return new IUsing[0]; } }
        public IEnumerable<IStemMember> Members { get { return new IStemMember[0]; } }
        public IEnumerable<INamespace> Namespaces { get { return new INamespace[0]; } }
        public IEnumerable<INamespace> AllChildNamespaces { get { return Namespaces; } }
        public IEnumerable<INamespace> NonEmptyNamespaces { get { return Namespaces; } }
    }

   
}
