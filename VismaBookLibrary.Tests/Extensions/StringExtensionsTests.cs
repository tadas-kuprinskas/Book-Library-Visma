using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Extensions;
using Xunit;

namespace VismaBookLibrary.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void FirstLetterToUpper_GivenStringInput_ReturnsFirstLetterUppercaseString()
        {
            var input = "filter";

            var result = StringExtensions.FirstLetterToUpper(input);

            result.Substring(0, 1).Should().Be("F");
        }

        [Fact]
        public void ParseStringToDate_GivenCorrectStringInput_ReturnsDateTimeType()
        {
            var input = "2021-05-16";

            var parsedInput = StringExtensions.ParseStringToDate(input);

            var expected = new DateTime(2021, 05, 16);

            Assert.Equal(expected, parsedInput);
        }
    }
}
