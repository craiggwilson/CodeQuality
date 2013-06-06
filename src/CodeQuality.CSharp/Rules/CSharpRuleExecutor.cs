using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules
{
    public class CSharpRuleExecutor : SyntaxVisitor<IEnumerable<IRuleResult<Location>>>, ICSharpRuleExecutor
    {
        private readonly IEnumerable<IRule<SyntaxTree, SyntaxNode, Location>> _rules;
        private IRuleExecutionContext<SyntaxTree> _context;

        public CSharpRuleExecutor(IEnumerable<IRule<SyntaxTree, SyntaxNode, Location>> rules)
        {
            _rules = rules;
        }

        public IEnumerable<IRuleResult<Location>> Execute(IRuleExecutionContext<SyntaxTree> context)
        {
            _context = context;
            return context.SyntaxTree.GetRoot().Accept(this);
        }

        public override IEnumerable<IRuleResult<Location>> DefaultVisit(SyntaxNode node)
        {
            return node.ChildNodes().SelectMany(n => Visit(n));
        }

        public override IEnumerable<IRuleResult<Location>> Visit(SyntaxNode node)
        {
            IEnumerable<RuleName> rulesToIgnore = null;
            switch (node.Kind)
            {
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    rulesToIgnore = GetRuleNamesToIgnore(((AccessorDeclarationSyntax)node).AttributeLists);
                    break;
                case SyntaxKind.ClassDeclaration:
                    rulesToIgnore = GetRuleNamesToIgnore(((ClassDeclarationSyntax)node).AttributeLists);
                    break;
                case SyntaxKind.CompilationUnit:
                    rulesToIgnore = GetRuleNamesToIgnore(((CompilationUnitSyntax)node).AttributeLists);
                    break;
                case SyntaxKind.FieldDeclaration:
                    rulesToIgnore = GetRuleNamesToIgnore(((FieldDeclarationSyntax)node).AttributeLists);
                    break;
                case SyntaxKind.Parameter:
                    rulesToIgnore = GetRuleNamesToIgnore(((ParameterSyntax)node).AttributeLists);
                    break;
                case SyntaxKind.PropertyDeclaration:
                    rulesToIgnore = GetRuleNamesToIgnore(((PropertyDeclarationSyntax)node).AttributeLists);
                    break;
            }

            return _rules
                .SelectMany(x => x.Process(_context, node))
                .Concat(base.Visit(node))
                .Where(r => !rulesToIgnore.Any(i => i.Category == r.Name.Category && i.CheckId == r.Name.CheckId));
        }

        private static IEnumerable<RuleName> GetRuleNamesToIgnore(SyntaxList<AttributeListSyntax> attributeLists)
        {
            string[] ignoreNames = new [] 
            {
                "SuppressMessage",
                "CodeAnalysis.SuppressMessage",
                "Diagnostics.CodeAnalysis.SuppressMessage",
                "System.Diagnostics.CodeAnalysis.SuppressMessage"
            };

            return attributeLists
                .SelectMany(x => x.Attributes)
                .Where(a => ignoreNames.Contains(a.Name.ToString()) && a.ArgumentList.Arguments.Count == 2)
                .Where(a => a.ArgumentList.Arguments[0].Expression.Kind == SyntaxKind.StringLiteralExpression && a.ArgumentList.Arguments[1].Expression.Kind == SyntaxKind.StringLiteralExpression)
                .Select(a => new RuleName(
                    ((LiteralExpressionSyntax)a.ArgumentList.Arguments[0].Expression).ToFullString().Replace("\"", ""),
                    ((LiteralExpressionSyntax)a.ArgumentList.Arguments[1].Expression).ToFullString().Replace("\"", "")
                ));
        }
    }
}