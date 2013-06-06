﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality.Rules
{
    public interface ICSharpRule : IRule<SyntaxTree, SyntaxNode, Location>
    {
    }
}