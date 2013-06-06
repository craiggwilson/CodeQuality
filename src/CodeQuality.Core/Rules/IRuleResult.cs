using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.Common;

namespace CodeQuality.Rules
{
    public interface IRuleResult<TLocation> where TLocation : CommonLocation
    {
        string Description { get; }

        TLocation Location { get; }

        RuleName Name { get; }

        RuleResultType Type { get; }
    }
}