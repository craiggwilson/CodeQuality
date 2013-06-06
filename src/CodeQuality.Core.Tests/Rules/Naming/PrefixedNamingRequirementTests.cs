using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace CodeQuality.Rules.Naming
{
    public class PrefixedNamingRequirementTests
    {
        [Fact]
        public void Should_return_true_when_string_starts_with_the_prefix()
        {
            var subject = new PrefixedNamingRequirement("_");

            subject.Matches("_funny").Should().BeTrue();
        }

        [Fact]
        public void Should_return_false_when_string_does_not_start_with_the_prefix()
        {
            var subject = new PrefixedNamingRequirement("_");

            subject.Matches("Funny").Should().BeFalse();
        }

        [Fact]
        public void Should_invoke_pipelined_requirement_with_prefix_stripped_when_name_starts_with_the_prefix()
        {
            var inner = Substitute.For<INamingRequirement>();

            var subject = new PrefixedNamingRequirement("_", inner);

            subject.Matches("_funny");

            inner.Received(1).Matches("funny");
        }

        [Fact]
        public void Should_not_invoke_pipelined_requirement_when_name_does_not_start_with_the_prefix()
        {
            var inner = Substitute.For<INamingRequirement>();

            var subject = new PrefixedNamingRequirement("_", inner);

            subject.Matches("Funny");

            inner.DidNotReceive().Matches("Funny");
        }
    }
}