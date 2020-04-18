using Domotech.iRemote.WebService.GraphApi;
using Domotech.iRemote.WebService.GraphApi.OutputTypes;
using Domotech.iRemote.WebService.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Domotech.iRemote.WebService.Tests.GraphApi
{
    public sealed class QueryTests
    {
        [Fact]
        public void Connection_ShouldReturnConnectionDetails()
        {
            // Arrange
            var connectionStateServiceMock = new Mock<IConnectionStateService>();
            connectionStateServiceMock.Setup(x => x.GetState()).Returns(ConnectionState.Downloading);
            connectionStateServiceMock.Setup(x => x.GetDownloadProgressInPercent()).Returns(66);
            var query = new Query();

            // Act
            ConnectionDetails connectionDetails = query.Connection(connectionStateServiceMock.Object);

            // Assert
            connectionDetails.State.Should().Be(ConnectionState.Downloading);
            connectionDetails.DownloadProgressInPercent.Should().Be(66);
        }
    }
}
