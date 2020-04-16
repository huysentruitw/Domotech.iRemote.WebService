using System;
using System.Threading;
using System.Threading.Tasks;
using Domotech.iRemote.WebService.Hubs;
using Domotech.iRemote.WebService.Services;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Domotech.iRemote.WebService.Tests.Services
{
    public sealed class ConnectionStateServiceTests
    {
        private readonly Mock<IServiceProvider> _serviceProviderMock = new Mock<IServiceProvider>();
        private readonly Mock<IClientProxy> _clientProxyMock = new Mock<IClientProxy>();

        public ConnectionStateServiceTests()
        {
            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(() =>
                {
                    var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
                    serviceScopeFactoryMock
                        .Setup(x => x.CreateScope())
                        .Returns(() =>
                        {
                            var serviceScopeMock = new Mock<IServiceScope>();
                            serviceScopeMock
                                .SetupGet(x => x.ServiceProvider)
                                .Returns(_serviceProviderMock.Object);

                            return serviceScopeMock.Object;
                        });

                    return serviceScopeFactoryMock.Object;
                });

            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IHubContext<ConnectionStateHub>)))
                .Returns(() =>
                {
                    var hubContextMock = new Mock<IHubContext<ConnectionStateHub>>();
                    hubContextMock
                        .SetupGet(x => x.Clients)
                        .Returns(() =>
                        {
                            var hubClientsMock = new Mock<IHubClients>();
                            hubClientsMock
                                .SetupGet(x => x.All)
                                .Returns(_clientProxyMock.Object);

                            return hubClientsMock.Object;
                        });

                    return hubContextMock.Object;
                });
        }

        [Fact]
        public void GetState_AfterConstruction_ShouldBeUnknown()
        {
            // Arrange
            var service = new ConnectionStateService(_serviceProviderMock.Object);

            // Act
            ConnectionState state = service.GetState();

            // Assert
            state.Should().Be(ConnectionState.Unknown);
        }

        [Fact]
        public void GetDownloadProgressInPercent_AfterConstruction_ShouldBeZero()
        {
            // Arrange
            var service = new ConnectionStateService(_serviceProviderMock.Object);

            // Act
            int downloadProgressInPercent = service.GetDownloadProgressInPercent();

            // Assert
            downloadProgressInPercent.Should().Be(0);
        }

        [Fact]
        public async Task UpdateState_ShouldUpdateState()
        {
            // Arrange
            var service = new ConnectionStateService(_serviceProviderMock.Object);

            // Act
            await service.UpdateState(ConnectionState.ConnectionLost);

            // Assert
            service.GetState().Should().Be(ConnectionState.ConnectionLost);
        }

        [Fact]
        public async Task UpdateStateAndDownloadProgress_ShouldUpdateStateAndDownloadProgress()
        {
            // Arrange
            var service = new ConnectionStateService(_serviceProviderMock.Object);

            // Act
            await service.UpdateStateAndDownloadProgress(ConnectionState.Downloading, 12);

            // Assert
            service.GetState().Should().Be(ConnectionState.Downloading);
            service.GetDownloadProgressInPercent().Should().Be(12);
        }

        [Fact]
        public async Task UpdateState_ShouldNotifyWebClients()
        {
            // Arrange
            var service = new ConnectionStateService(_serviceProviderMock.Object);

            // Act
            await service.UpdateState(ConnectionState.ConnectionLost);

            // Assert
            _clientProxyMock.Verify(x => x.SendCoreAsync("Changed", It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateStateAndDownloadProgress_ShouldNotifyWebClients()
        {
            // Arrange
            var service = new ConnectionStateService(_serviceProviderMock.Object);

            // Act
            await service.UpdateStateAndDownloadProgress(ConnectionState.Downloading, 12);

            // Assert
            _clientProxyMock.Verify(x => x.SendCoreAsync("Changed", It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
