using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.Common;

namespace CodeQuality.Rules
{
    public class RuleResult<TLocation> : IRuleResult<TLocation> where TLocation : CommonLocation
    {
        private readonly string _description;
        private readonly TLocation _location;
        private readonly RuleName _name;
        private readonly RuleResultType _type;

        public RuleResult(RuleName name, RuleResultType type, TLocation location, string description)
        {
            _name = name;
            _type = type;
            _location = location;
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }

        public TLocation Location
        {
            get { return _location; }
        }

        public RuleName Name
        {
            get { return _name; }
        }

        public RuleResultType Type
        {
            get { return _type; }
        }

        public static RuleResult<TLocation> Error(RuleName name, TLocation location, string description)
        {
            return new RuleResult<TLocation>(name, RuleResultType.Error, location, description);
        }

        public static RuleResult<TLocation> Information(RuleName name, TLocation location, string description)
        {
            return new RuleResult<TLocation>(name, RuleResultType.Information, location, description);
        }

        public static RuleResult<TLocation> Warning(RuleName name, TLocation location, string description)
        {
            return new RuleResult<TLocation>(name, RuleResultType.Warning, location, description);
        }
    }
}