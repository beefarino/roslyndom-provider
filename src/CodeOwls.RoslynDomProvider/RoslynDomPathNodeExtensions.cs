using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodes;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    static class RoslynDomPathNodeExtensions
    {
        public static IPathNode ToContainerPathNode<T>(this IEnumerable<T> items, string containerName) where T : IDom
        {
            return new RoslynDomContainerPathNode<T>(containerName, items);
        }

        public static IEnumerable<IPathNode> ToPathNodes<T>(this IEnumerable<T> items) where T : IDom
        {
            return from item in items select new RoslynDomPathNode<T>(item);
        }
    }
}