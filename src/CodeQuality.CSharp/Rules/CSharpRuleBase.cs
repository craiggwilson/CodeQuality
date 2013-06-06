using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules
{
    public abstract class CSharpRuleBase<TSyntaxNode> : ICSharpRule
        where TSyntaxNode : SyntaxNode
    {
        private static readonly IEnumerable<IRuleResult<Location>> _noViolations = Enumerable.Empty<IRuleResult<Location>>();
        private readonly RuleName _name;

        protected CSharpRuleBase(string category, string checkId)
        {
            _name = new RuleName(category, checkId);
        }

        public RuleName Name
        {
            get { return _name; }
        }

        protected static IEnumerable<IRuleResult<Location>> NoViolations
        {
            get { return _noViolations; }
        }

        public IEnumerable<IRuleResult<Location>> Process(IRuleExecutionContext<SyntaxTree> context, SyntaxNode node)
        {
            var typedNode = node as TSyntaxNode;
            if (typedNode == null)
            {
                return NoViolations;
            }

            return Process(context, typedNode);
        }

        protected abstract IEnumerable<IRuleResult<Location>> Process(IRuleExecutionContext<SyntaxTree> context, TSyntaxNode node);

        protected IRuleResult<Location> Error(SyntaxNode node, string description)
        {
            return new RuleResult<Location>(_name, RuleResultType.Error, node.GetLocation(), description);
        }

        protected IRuleResult<Location> Error(SyntaxNode node, string format, params object[] args)
        {
            return Error(node, string.Format(format, args));
        }

        protected IRuleResult<Location> Information(SyntaxNode node, string description)
        {
            return new RuleResult<Location>(_name, RuleResultType.Information, node.GetLocation(), description);
        }

        protected IRuleResult<Location> Information(SyntaxNode node, string format, params object[] args)
        {
            return Information(node, string.Format(format, args));
        }

        protected IRuleResult<Location> Warning(SyntaxNode node, string description)
        {
            return new RuleResult<Location>(_name, RuleResultType.Warning, node.GetLocation(), description);
        }

        protected IRuleResult<Location> Warning(SyntaxNode node, string format, params object[] args)
        {
            return Warning(node, string.Format(format, args));
        }
    }
}
