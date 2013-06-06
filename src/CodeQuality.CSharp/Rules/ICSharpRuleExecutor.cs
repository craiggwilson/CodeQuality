using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeQuality.Rules;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules
{
    public interface ICSharpRuleExecutor : IRuleExecutor<SyntaxTree, Location> 
    {
    }
}