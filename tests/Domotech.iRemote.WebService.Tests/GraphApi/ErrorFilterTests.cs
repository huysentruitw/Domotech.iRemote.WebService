using System;
using System.Collections.Generic;
using Domotech.iRemote.WebService.GraphApi;
using FluentAssertions;
using HotChocolate;
using Moq;
using Xunit;

namespace Domotech.iRemote.WebService.Tests.GraphApi
{
    public sealed class ErrorFilterTests
    {
        [Fact]
        public void OnError_ErrorWithoutException_ShouldReturnErrorAsIs()
        {
            // Arrange
            IError error = ErrorBuilder.New()
                .SetMessage("Error occurred")
                .Build();
            var filter = new ErrorFilter();

            // Act
            IError result = filter.OnError(error);

            // Assert
            result.Should().Be(error);
        }

        [Fact]
        public void OnError_WithException_ShouldSetErrorCode()
        {
            // Arrange
            IError error = ErrorBuilder.New()
                .SetMessage("Error occurred")
                .SetException(new NotImplementedException())
                .Build();
            var filter = new ErrorFilter();

            // Act
            IError result = filter.OnError(error);

            // Assert
            result.Code.Should().Be("NOT_IMPLEMENTED");
        }

        [Fact]
        public void OnError_WithExceptionContainingProperties_ShouldSetErrorData()
        {
            // Arrange
            IError error = ErrorBuilder.New()
                .SetMessage("Error occurred")
                .SetException(new PropertiesException(123, "abc"))
                .Build();
            var filter = new ErrorFilter();

            // Act
            IError result = filter.OnError(error);

            // Assert
            result.Extensions.Keys.Should().Contain("data");
            var data = result.Extensions["data"] as IReadOnlyDictionary<string, object>;
            data.Should().NotBeNull();
            data["paramA"].Should().Be(123);
            data["paramB"].Should().Be("abc");
        }

        private sealed class PropertiesException : Exception
        {
            public PropertiesException(int paramA, string paramB)
                : base("Error occurred")
            {
                ParamA = paramA;
                ParamB = paramB;
            }

            public int ParamA { get; }

            public string ParamB { get; }
        }
    }
}
