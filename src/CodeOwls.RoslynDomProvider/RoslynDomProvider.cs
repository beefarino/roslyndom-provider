using System.Management.Automation;
using CodeOwls.PowerShell.Provider;

namespace CodeOwls.RoslynDomProvider
{
    [System.Management.Automation.Provider.CmdletProvider("RoslynDom", System.Management.Automation.Provider.ProviderCapabilities.ShouldProcess)]
    public class RoslynDomProvider : Provider
    {
        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            drive = RoslynDomPathResolver.UpdateRootForDriveInfo(drive);
            return new RoslynDomDrive( drive );
        }

        protected override PowerShell.Paths.Processors.IPathResolver PathResolver
        {
            get { return new RoslynDomPathResolver(); }
        }
    }
}