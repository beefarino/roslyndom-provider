using System;
using System.Collections.Generic;
using System.Management.Automation;
using CodeOwls.PowerShell.Provider;
using RoslynDom;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    class RoslynDomDrive : Drive
    {
        public RoslynDomDrive(PSDriveInfo info)
            : base(info)
        {
        }

        static RoslynDomDrive()
        {
            _rootDomNodeMap = new Dictionary<string, IRoot>(StringComparer.InvariantCultureIgnoreCase);
        }

        IRoot _rootDomNode;
        static readonly IDictionary<string, IRoot> _rootDomNodeMap;

        static internal IRoot GetRootDomNodeForPath(string path)
        {
            IRoot rootDomNode;
            var result = _rootDomNodeMap.TryGetValue(path, out rootDomNode);

            if (!result)
            {
                rootDomNode = RDomFactory.GetRootFromFile(path);
                _rootDomNodeMap.Add(path, rootDomNode);
            }
            return rootDomNode;
        }

        internal IRoot RootDomNode
        {
            get
            {
                if (null == _rootDomNode)
                {
                    _rootDomNode = GetRootDomNodeForPath(Root);
                }
                return _rootDomNode;
            }
        }
    }
}