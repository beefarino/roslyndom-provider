using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
using CodeOwls.PowerShell.Provider.PathNodes;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    //TODO migrate the root path treatment into the P2F lib
    public class RoslynDomPathResolver : CodeOwls.PowerShell.Paths.Processors.PathResolverBase
    {
        internal static PSDriveInfo UpdateRootForDriveInfo(PSDriveInfo drive)
        {
            if (drive.Root.StartsWith("["))
            {
                return drive;
            }

            return new PSDriveInfo(drive.Name, drive.Provider, "[" + drive.Root + "]", drive.Description,drive.Credential);
        }

        static readonly Regex PathRegex = new Regex(@"^.*\[(.+?)\](.*)$");
        public override IEnumerable<IPathNode> ResolvePath(PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext, string path)
        {            
            var codePath = PathRegex.Replace(path, "$1");
            path = PathRegex.Replace(path, "$2");

            var domRoot = RoslynDomDrive.GetRootDomNodeForPath(codePath);
            _rootPathNode = new RoslynDomPathNode<IRoot>(domRoot);

            return base.ResolvePath(providerContext, path);
        }

        IPathNode _rootPathNode;
        protected override IPathNode Root
        {
            get
            {
                return _rootPathNode;
            }
        }
    }
}