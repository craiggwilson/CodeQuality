using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQuality.Rules.Naming
{
    public class PrefixedNamingRequirement : PipelinedNamingRequirement
    {
        private readonly string _prefix;

        public PrefixedNamingRequirement(string prefix, INamingRequirement next = null)
            : base(next)
        {
            _prefix = prefix;
        }

        public override bool Matches(string name)
        {
            if (!name.StartsWith(_prefix))
            {
                return false;
            }

            return base.Matches(name.Substring(_prefix.Length));
        }

        public override string ToString()
        {
            var @base = base.ToString();
            var mine = string.Format("Must begin with {0}.", _prefix);
            if (!string.IsNullOrWhiteSpace(@base))
            {
                var first = char.ToLower(@base[0]);
                return mine + " The rest " + first + @base.Substring(1);
            }

            return mine;
        }
    }
}