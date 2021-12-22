using System;
using System.IO;
using FluentAssertions;
using Vantage.Infra.Crosscutting.Extensions;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Extensions
{
    public class Stream_ToByteArray
    {
        [Fact]
        public void ReturnValidByteArrayGivenNotNullStream()
        {
            using (var stream = new MemoryStream())
            {
                byte[] byteArray = stream.ToByteArray();

                byteArray.Should().NotBeNull();
                byteArray.Length.Should().Be(0);
            }
        }

        [Fact]
        public void ThrowExceptionGivenNullStream()
        {
            Action act = () =>
            {
                MemoryStream stream = null;
                stream.ToByteArray();
            };

            act.Should().Throw<NullReferenceException>();
        }
    }
}
