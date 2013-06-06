using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQuality.Rules.Naming
{
    public abstract class PipelinedNamingRequirement : INamingRequirement
    {
        private readonly INamingRequirement _namingRequirement;

        public PipelinedNamingRequirement(INamingRequirement namingRequirement = null)
        {
            _namingRequirement = namingRequirement;
        }

        public virtual bool Matches(string name)
        {
            if (_namingRequirement != null)
            {
                return _namingRequirement.Matches(name);
            }

            return true;
        }

        public override string ToString()
        {
            if (_namingRequirement != null)
            {
                return _namingRequirement.ToString();
            }

            return string.Empty;
        }
    }
}