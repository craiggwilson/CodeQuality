using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules
{
    public abstract class RuleTestBase
    {

        protected IEnumerable<IRuleResult<Location>> Execute(SyntaxTree syntaxTree, params IRule<SyntaxTree, SyntaxNode, Location>[] rules)
        {
            var executor = new CSharpRuleExecutor(rules);
            var context = new CSharpRuleExecutionContext(syntaxTree);
            return executor.Execute(context);
        }

    }
}