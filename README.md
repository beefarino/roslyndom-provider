# RoslynDomProvider

Allows the creation of PowerShell drives based on RoslynDom syntax trees.

Note this project is essentially a proof of concept against the [RoslynDom](https://github.com/KathleenDollard/RoslynDOM) project from Kathleen Dollard.  That project is moving very rapidly and I'm not making plans to keep this provider up-to-date.  At the moment this project is using the 1.0.5 alpha NuGet package of RoslynDom.

# Quick Start
## Installing
The module requires [psake 4.3.2](https://github.com/psake/psake) to build.
	> import-module psake
	> invoke-psake ./default.ps1 -task install
## Usage
The first step is to import the module:
	> import-module roslyndom

Once that's done, you'll need to mount a roslyndom drive.  Specify the full path to a source code file as the root parameter of new-psdrive:
	> new-psdrive -name code -psp roslyndom -root 'c:\code\sample.cs'

With the drive created, you can now use the standard item cmdlets to spelunker the code tree in the source file:
	> cd code:
	> dir

