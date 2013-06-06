using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules.Naming.CSharp
{
    public class FieldDeclarationNamingRule : CSharpRuleBase<FieldDeclarationSyntax>
    {
        private readonly INamingRequirement _namingRequirement;
        private readonly List<SyntaxKind> _modifierKinds;

        public FieldDeclarationNamingRule(IEnumerable<SyntaxKind> modifierKinds, INamingRequirement namingRequirement)
            : base(RuleNames.Namespace, RuleNames.FieldDeclarationNamingRule)
        {
            _namingRequirement = namingRequirement;
            _modifierKinds = modifierKinds.ToList();
        }

        protected override IEnumerable<IRuleResult<Location>> Process(IRuleExecutionContext<SyntaxTree> context, FieldDeclarationSyntax node)
        {
            if (!_modifierKinds.All(kind => node.Modifiers.Any(m => m.Kind == kind)))
            {
                yield break;
            }

            foreach (var variable in node.Declaration.Variables)
            {
                var fieldName = variable.Identifier.ValueText;
                if (!_namingRequirement.Matches(fieldName))
                {
                    yield return Error(variable, "'{0}' does not match the naming requirement: {1}", fieldName, _namingRequirement);
                }
            }
        }
    }
}