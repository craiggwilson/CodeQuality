using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQuality.Rules
{
    public class RuleName
    {
        private readonly string _category;
        private readonly string _checkId;

        public RuleName(string category, string checkId)
        {
            _category = category;
            _checkId = checkId;
        }

        public string Category
        {
            get { return _category; }
        }

        public string CheckId
        {
            get { return _checkId; }
        }
    }
}