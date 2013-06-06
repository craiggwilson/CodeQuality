using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;
using Xunit;
using FluentAssertions;

namespace CodeQuality.Rules.Naming.CSharp.Tests
{
    public class FieldDeclarationNamingRuleTests : RuleTestBase
    {
        [Fact]
        public void Should_not_return_violations_when_no_violations_exist()
        {
            const string clazz = @"
                public class Test
                {
                    private string field1;
                    public string Field2;
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new FieldDeclarationNamingRule(
                new[] { SyntaxKind.PrivateKeyword },
                new CamelCaseNamingRequirement());

            var results = Execute(tree, rule);
            results.Should().BeEmpty();
        }

        [Fact]
        public void Should_return_a_violation_when_name_does_not_match_requirement()
        {
            const string clazz = @"
                public class Test
                {
                    private string Field1;
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new FieldDeclarationNamingRule(
                new[] { SyntaxKind.PrivateKeyword },
                new CamelCaseNamingRequirement());

            var results = Execute(tree, rule);
            results.Count().Should().Be(1);
        }

        [Fact]
        public void Should_return_a_violation_from_a_nested_class()
        {
            const string clazz = @"
                public class Test
                {
                    private class Nested
                    {
                        private string Field;
                    }
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new FieldDeclarationNamingRule(
                new[] { SyntaxKind.PrivateKeyword },
                new CamelCaseNamingRequirement());

            var results = Execute(tree, rule);
            results.Count().Should().Be(1);
        }
    }
}