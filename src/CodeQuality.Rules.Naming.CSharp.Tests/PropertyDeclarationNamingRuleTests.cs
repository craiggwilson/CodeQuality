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
    public class PropertyDeclarationNamingRuleTests : RuleTestBase
    {
        [Fact]
        public void Should_not_return_violations_when_no_violations_exist()
        {
            const string clazz = @"
                public class Test
                {
                    public string Prop { get; set; }
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new PropertyDeclarationNamingRule(
                new SyntaxKind[0], // all properties
                new PascalCaseNamingRequirement());

            var results = Execute(tree, rule);
            results.Should().BeEmpty();
        }

        [Fact]
        public void Should_return_a_violation_when_name_does_not_match_requirement()
        {
            const string clazz = @"
                public class Test
                {
                    public string prop { get; set; }
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new PropertyDeclarationNamingRule(
                new SyntaxKind[0], // all properties
                new PascalCaseNamingRequirement());

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
                        public string prop { get; private set; }
                    }
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new PropertyDeclarationNamingRule(
                new SyntaxKind[0], // all properties
                new PascalCaseNamingRequirement());

            var results = Execute(tree, rule);
            results.Count().Should().Be(1);
        }

        [Fact]
        public void Should_return_a_violation_based_on_a_combined_modifier_list()
        {
            const string clazz = @"
                public class Test
                {
                    private class Nested
                    {
                        public string prop { get; private set; }
                    }
                }";

            var tree = SyntaxTree.ParseText(clazz);

            var rule = new PropertyDeclarationNamingRule(
                new[] { SyntaxKind.PrivateKeyword }, // properties with a private modifier
                new PascalCaseNamingRequirement());

            var results = Execute(tree, rule);
            results.Count().Should().Be(1);
        }
    }
}