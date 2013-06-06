using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.Common;

namespace CodeQuality.Rules
{
    public class RuleExecutionContext<TSyntaxTree> : IRuleExecutionContext<TSyntaxTree>
        where TSyntaxTree : CommonSyntaxTree
    {
        private readonly TSyntaxTree _syntaxTree;

        public RuleExecutionContext(TSyntaxTree syntaxTree)
        {
            _syntaxTree = syntaxTree;
        }

        public TSyntaxTree SyntaxTree
        {
            get { return _syntaxTree; }
        }
    }
}