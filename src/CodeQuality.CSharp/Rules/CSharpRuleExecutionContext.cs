using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules
{
    public class CSharpRuleExecutionContext : RuleExecutionContext<SyntaxTree>
    {
        public CSharpRuleExecutionContext(SyntaxTree syntaxTree)
            : base(syntaxTree)
        {
        }
    }
}