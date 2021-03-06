﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace CodeQuality.Rules.Naming
{
    public class PascalCaseNamingRequirementTests
    {
        [Fact]
        public void Should_return_true_when_string_starts_with_a_lower_letter()
        {
            var subject = new PascalCaseNamingRequirement();

            subject.Matches("Funny").Should().BeTrue();
        }

        [Fact]
        public void Should_return_false_when_string_does_not_start_with_a_lower_letter()
        {
            var subject = new PascalCaseNamingRequirement();

            subject.Matches("funny").Should().BeFalse();
        }

        [Fact]
        public void Should_invoke_pipelined_requirement_when_name_starts_with_a_lower_letter()
        {
            var inner = Substitute.For<INamingRequirement>();

            var subject = new PascalCaseNamingRequirement(inner);

            subject.Matches("Funny");

            inner.Received(1).Matches("Funny");
        }

        [Fact]
        public void Should_not_invoke_pipelined_requirement_when_name_does_not_start_with_a_lower_letter()
        {
            var inner = Substitute.For<INamingRequirement>();

            var subject = new PascalCaseNamingRequirement(inner);

            subject.Matches("funny");

            inner.DidNotReceive().Matches("funny");
        }
    }
}