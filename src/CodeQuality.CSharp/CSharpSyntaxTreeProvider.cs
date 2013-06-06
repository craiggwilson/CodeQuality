using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace CodeQuality
{
    public class CSharpSyntaxTreeProvider : ISyntaxTreeProvider<SyntaxTree>
    {
        private readonly List<string> _sourceFiles;

        public CSharpSyntaxTreeProvider(IEnumerable<string> sourceFiles)
        {
            _sourceFiles = sourceFiles.ToList();
        }

        public IEnumerable<SyntaxTree> GetSyntaxTrees()
        {
            foreach (var sourceFile in _sourceFiles)
            {
                yield return SyntaxTree.ParseFile(sourceFile);
            }
        }
    }
}