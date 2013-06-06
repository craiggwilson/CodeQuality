using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeQuality.Rules;
using Roslyn.Compilers.Common;

namespace CodeQuality.Rules
{
    public interface IRuleExecutor<TSyntaxTree, TLocation> 
        where TSyntaxTree : CommonSyntaxTree
        where TLocation : CommonLocation
    {
        IEnumerable<IRuleResult<TLocation>> Execute(IRuleExecutionContext<TSyntaxTree> context);
    }
}