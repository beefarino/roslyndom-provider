using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.InteropServices;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using RoslynDom.Common;

namespace CodeOwls.RoslynDomProvider
{
    class RoslynDomPathNode<T> : PathNodeBase
        where T : IDom
    {
        private readonly T _roslynDomNode;
        
        public RoslynDomPathNode( T roslynDomNode )
        {
            _roslynDomNode = roslynDomNode;            
        }

        public override IEnumerable<IPathNode> GetNodeChildren(IProviderContext providerContext)
        {
            if( Namespaces.Any() ) 
                yield return Namespaces.ToContainerPathNode(NamespaceContainerName);
            if(Classes.Any()) 
                yield return Classes.ToContainerPathNode(ClassContainerName);
            if(Interfaces.Any()) 
                yield return Interfaces.ToContainerPathNode(InterfaceContainerName);
            if (Structures.Any()) 
                yield return Structures.ToContainerPathNode(StructureContainerName);
            if (Types.Any()) 
                yield return Types.ToContainerPathNode(TypeContainerName);
            if (Enums.Any())
                yield return Enums.ToContainerPathNode(EnumContainerName);
            if (Usings.Any()) 
                yield return Usings.ToContainerPathNode(UsingContainerName);
            if (TypeMemberContainer.Methods.Any()) 
                yield return TypeMemberContainer.Methods.ToContainerPathNode(MethodContainerName);
            if (TypeMemberContainer.Properties.Any()) 
                yield return TypeMemberContainer.Properties.ToContainerPathNode(PropertyContainerName);
            if (TypeMemberContainer.Fields.Any()) 
                yield return TypeMemberContainer.Fields.ToContainerPathNode(FieldContainerName);
            if (PropertyOrMethod.Parameters.Any()) 
                yield return PropertyOrMethod.Parameters.ToContainerPathNode(ParameterContainerName);
            if (Attributes.Any())
                yield return Attributes.ToContainerPathNode(AttributeContainerName);
            if (AttributeValues.Any())
                yield return AttributeValues.ToContainerPathNode(AttributeValueContainerName);
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue( _roslynDomNode, Name );
        }

        public override string Name
        {
            get { return _roslynDomNode.Name; }
        }
        
        IEnumerable<INamespace> Namespaces
        {
            get { return StemContainer.Namespaces; }
        }

        IEnumerable<IClass> Classes
        {
            get { return StemContainer.Classes; }
        }

        IEnumerable<IEnum> Enums
        {
            get { return StemContainer.Enums; }
        }

        IEnumerable<IInterface> Interfaces
        {
            get { return StemContainer.Interfaces; }
        }

        IEnumerable<IMember> Members
        {
            get { return StemContainer.Members; }
        }

        IEnumerable<IStructure> Structures
        {
            get { return StemContainer.Structures; }
        }

        IEnumerable<IStemMember> Types
        {
            get { return StemContainer.Types; }
        }

        IEnumerable<IUsing> Usings
        {
            get { return StemContainer.Usings; }
        }

        IEnumerable<IAttributeValue> AttributeValues
        {
            get { return Attribute.AttributeValues; }
        }

        IEnumerable<IAttribute> Attributes
        {
            get { return AttributeContainer.Attributes; }
        }

        IHasAttributes AttributeContainer
        {
            get
            {
                var attrs = _roslynDomNode as IHasAttributes ?? new NullHasAttributes();
                return attrs;
            }
        }
        IStemContainer StemContainer
        {
            get
            {
                var container = _roslynDomNode as IStemContainer ?? new NullStemContainer();
                return container;
            }
        }

        IPropertyOrMethod PropertyOrMethod
        {
            get
            {
                var impl = _roslynDomNode as IPropertyOrMethod ?? new NullPropertyOrMethod();
                return impl;
            }
        }

        ITypeMemberContainer TypeMemberContainer
        {
            get
            {
                var container = _roslynDomNode as ITypeMemberContainer ?? new NullTypeMemberContainer();
                return container;
            }
        }

        IAttribute Attribute
        {
            get
            {
                var attr = _roslynDomNode as IAttribute ?? new NullAttribute();
                return attr;
            }
        }

        private const string NamespaceContainerName = "Namespaces";
        private const string UsingContainerName = "Usings";
        private const string EnumContainerName = "Enums";
        private const string InterfaceContainerName = "Interfaces";
        private const string StructureContainerName = "Structures";
        private const string ClassContainerName = "Classes";
        private const string TypeContainerName = "Types";
        private const string ParameterContainerName = "Parameters";
        private const string MethodContainerName = "Methods";
        private const string PropertyContainerName = "Properties";
        private const string FieldContainerName = "Fields";
        private const string AttributeContainerName = "Attributes";
        private const string AttributeValueContainerName = "Values";

    }
}