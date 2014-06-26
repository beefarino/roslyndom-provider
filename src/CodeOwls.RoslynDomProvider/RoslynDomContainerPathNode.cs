using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    class RoslynDomContainerPathNode<T> : PathNodeBase where T : IDom
    {
        private readonly string _name;
        private readonly IEnumerable<T> _children;

        public RoslynDomContainerPathNode(string name, IEnumerable<T> children )
        {
            _name = name;
            _children = children;
        }

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            return _children.ToList().ConvertAll(n => new RoslynDomPathNode<T>(n));
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue( new ShellContainer(_name, _children.Count()), _name );
        }

        public override string Name
        {
            get { return _name; }
        }
    }
}