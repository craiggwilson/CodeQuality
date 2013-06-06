using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules.Naming.CSharp
{
    public class PropertyDeclarationNamingRule : CSharpRuleBase<PropertyDeclarationSyntax>
    {
        private readonly INamingRequirement _namingRequirement;
        private readonly List<SyntaxKind> _modifierKinds;

        public PropertyDeclarationNamingRule(IEnumerable<SyntaxKind> modifierKinds, INamingRequirement namingRequirement)
            : base(RuleNames.Namespace, RuleNames.FieldDeclarationNamingRule)
        {
            _namingRequirement = namingRequirement;
            _modifierKinds = modifierKinds.ToList();
        }

        protected override IEnumerable<IRuleResult<Location>> Process(IRuleExecutionContext<SyntaxTree> context, PropertyDeclarationSyntax node)
        {
            // take the modifiers on the node and combine them with the modifiers on each accessor
            // to get a flattened view
            var modifierGroups = node.AccessorList.ChildNodes().OfType<AccessorDeclarationSyntax>().Select(x => x.Modifiers.Concat(node.Modifiers));

            if (!_modifierKinds.All(kind => modifierGroups.Any(mg => mg.Any(m => m.Kind == kind))))
            {
                yield break;
            }

            string propertyName = node.Identifier.ValueText;

            if (!_namingRequirement.Matches(propertyName))
            {
                yield return Error(node, "'{0}' does not match the naming requirement: {1}", propertyName, _namingRequirement);
            }
        }
    }
}