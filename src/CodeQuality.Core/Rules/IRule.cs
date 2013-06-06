using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.Common;

namespace CodeQuality.Rules
{
    public interface IRule<TSyntaxTree, TSyntaxNode, TLocation>
        where TSyntaxTree : CommonSyntaxTree
        where TSyntaxNode : CommonSyntaxNode
        where TLocation : CommonLocation
    {
        IEnumerable<IRuleResult<TLocation>> Process(IRuleExecutionContext<TSyntaxTree> context, TSyntaxNode node);
    }
}