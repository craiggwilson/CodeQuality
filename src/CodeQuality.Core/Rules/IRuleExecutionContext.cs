using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.Common;

namespace CodeQuality.Rules
{
    public interface IRuleExecutionContext<TSyntaxTree>
        where TSyntaxTree : CommonSyntaxTree
    {
        TSyntaxTree SyntaxTree { get; }
    }
}