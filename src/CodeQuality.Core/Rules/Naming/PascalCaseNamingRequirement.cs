using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQuality.Rules.Naming
{
    public class PascalCaseNamingRequirement : PipelinedNamingRequirement
    {
        public PascalCaseNamingRequirement(INamingRequirement next = null)
            : base(next)
        { }

        public override bool Matches(string name)
        {
            if (name.Length > 0 && char.IsUpper(name, 0))
            {
                return base.Matches(name);
            }

            return false;
        }

        public override string ToString()
        {
            const string mine = "Must start with an uppercase letter.";
            var @base = base.ToString();
            if (!string.IsNullOrWhiteSpace(@base))
            {
                return mine + " " + @base;
            }

            return mine;
        }
    }
}