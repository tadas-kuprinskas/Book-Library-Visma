using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Enums;
using VismaBookLibrary.Domain.Extensions;
using Xunit;

namespace VismaBookLibrary.UnitTests.Extensions
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void ToDescriptionString_GivenObject_ReturnsString()
        {
            CommandEnums commandEnums = new();

            var result = EnumExtensions.ToDescriptionString(commandEnums);

            result.Should().BeOfType<string>();
        }
    }
}
