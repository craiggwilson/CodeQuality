using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQuality.Rules.Naming
{
    public interface INamingRequirement
    {
        bool Matches(string name);
    }
}