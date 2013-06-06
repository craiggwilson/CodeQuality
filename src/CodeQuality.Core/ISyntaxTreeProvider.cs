using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.Common;

namespace CodeQuality
{
    public interface ISyntaxTreeProvider<TSyntaxTree> where TSyntaxTree : CommonSyntaxTree
    {
        IEnumerable<TSyntaxTree> GetSyntaxTrees();
    }
}