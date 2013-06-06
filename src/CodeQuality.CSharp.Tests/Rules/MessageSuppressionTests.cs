using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace CodeQuality.Rules.CSharp.Naming.Tests
{
    public class MessageSuppressionTests : RuleTestBase
    {
        [Fact]
        public void Should_not_return_a_violation_when_the_message_has_been_suppressed_at_the_same_level()
        {
            const string clazz = @"
                public class Test
                {
                    [System.Diagnostics.CodeAnalysis.SuppressMessage(""category"", ""checkId"")]
                    private string Field1;
                }";

            var tree = SyntaxTree.ParseText(clazz);
            var rule = new DummyRule();

            var results = Execute(tree, rule);
            results.Should().BeEmpty();
        }

        [Fact]
        public void Should_not_return_a_violation_when_the_message_has_been_suppressed_at_a_parent_level()
        {
            const string clazz = @"
                [System.Diagnostics.CodeAnalysis.SuppressMessage(""category"", ""checkId"")]
                public class Test
                {
                    private string Field1;
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new DummyRule();

            var results = Execute(tree, rule);
            results.Should().BeEmpty();
        }

        private class DummyRule : CSharpRuleBase<FieldDeclarationSyntax>
        {
            public DummyRule()
                : base("category", "checkId")
            { }

            protected override IEnumerable<IRuleResult<Location>> Process(IRuleExecutionContext<SyntaxTree> context, FieldDeclarationSyntax node)
            {
                yield return Error(node, "Oops");
            }
        }
    }
}