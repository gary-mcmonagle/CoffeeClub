using System.Dynamic;
using CoffeeClub.Core.Api.Hubs;
using CoffeeClub.Core.Api.Services;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace CoffeeClub.Test.Unit.Api.Services;

public class OrderDispatchServiceShould
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IHubUserConnectionProviderService<OrderHub>> _hubUserConnectionProviderServiceMock = new();
    private readonly Mock<IHubContext<OrderHub>> _hubContextMock = new();
    private OrderDispatchService _sut;

    public OrderDispatchServiceShould()
    {
        _sut = new OrderDispatchService(
            _orderRepositoryMock.Object,
            _userRepositoryMock.Object,
            _hubUserConnectionProviderServiceMock.Object,
            _hubContextMock.Object);
    }

    [Fact]
    public async Task OrderCreatedEmitsOrderCreatedEvent()
    {
        var (mockClients, mockClientProxy) = GetMockIHubClients("OrderCreated");
        var mockOrder = new Mock<OrderDto>();

        _hubContextMock.SetupGet(x => x.Clients).Returns(mockClients.Object);
        await _sut.OrderCreated(mockOrder.Object, Guid.NewGuid());

        mockClientProxy.Verify(
            m => m.SendCoreAsync(
                "OrderCreated",
                It.IsAny<object[]>(),
                default),
            Times.Once());
    }

    [Fact]
    public async Task UpdateOrderEmits()
    {
        var (mockClients, mockClientProxy) = GetMockIHubClients("OrderUpdated");

        var mockOrder = new Mock<OrderDto>();
        var fakeUser = new User { AuthId = "authId", AuthProvider = default, Id = Guid.NewGuid() };
        var fakeOrder = new Order
        {
            Id = Guid.NewGuid(),
            User = fakeUser,
            DrinkOrders = new List<DrinkOrder>(),
            Status = Domain.Enumerations.OrderStatus.Pending
        };
        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(fakeUser);
        _orderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(fakeOrder);

        _hubUserConnectionProviderServiceMock.Setup(x => x.GetConnectionsForUserAsync(It.IsAny<Guid>()))
            .Returns(new List<string> { "connectionId1", "connectionId2" });

        _hubUserConnectionProviderServiceMock.Setup(x => x.GetAllWorkerConnections())
            .Returns(new List<string> { "connectionId3", "connectionId4" });

        _hubContextMock.SetupGet(x => x.Clients).Returns(mockClients.Object);
        await _sut.UpdateOrder(Guid.NewGuid(), Domain.Enumerations.OrderStatus.Ready, Guid.NewGuid());

        mockClientProxy.Verify(
            m => m.SendCoreAsync(
                "OrderUpdated",
                It.IsAny<object[]>(),
                default),
            Times.Once());
    }

    [Fact]
    public async Task UpdateOrderEmitsOrderUpdated()
    {
        var (mockClients, mockClientProxy) = GetMockIHubClients("OrderUpdated");

        var mockOrder = new Mock<OrderDto>();
        var fakeUser = new User { AuthId = "authId", AuthProvider = default, Id = Guid.NewGuid() };
        var fakeOrder = new Order
        {
            Id = Guid.NewGuid(),
            User = fakeUser,
            DrinkOrders = new List<DrinkOrder>(),
            Status = Domain.Enumerations.OrderStatus.Pending
        };
        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(fakeUser);
        _orderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(fakeOrder);

        _hubUserConnectionProviderServiceMock.Setup(x => x.GetConnectionsForUserAsync(It.IsAny<Guid>()))
            .Returns(new List<string> { "connectionId1", "connectionId2" });

        _hubUserConnectionProviderServiceMock.Setup(x => x.GetAllWorkerConnections())
            .Returns(new List<string> { "connectionId3", "connectionId4" });

        _hubContextMock.SetupGet(x => x.Clients).Returns(mockClients.Object);
        var updatedToStatus = Domain.Enumerations.OrderStatus.Ready;
        await _sut.UpdateOrder(Guid.NewGuid(), updatedToStatus, Guid.NewGuid());

        _orderRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Order>(order => order.Status == updatedToStatus)), Times.Once);
    }


    private static (Mock<IHubClients>, Mock<Microsoft.AspNetCore.SignalR.IClientProxy>) GetMockIHubClients(string mockedEvent)
    {
        var mockClients = new Mock<IHubClients>();
        var mockClientProxy = new Mock<Microsoft.AspNetCore.SignalR.IClientProxy>();
        mockClientProxy.Setup(m => m.SendCoreAsync(
            mockedEvent,
            It.IsAny<object[]>(),
            default))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var mockContext = new Mock<IHubContext<OrderHub>>();

        var workerConnections = new List<string> { "connectionId1", "connectionId2" };

        mockContext.SetupGet(x => x.Clients).Returns(mockClients.Object);
        mockClients.Setup(clients => clients.Clients(It.IsAny<IReadOnlyList<string>>()))
                   .Returns(mockClientProxy.Object);
        return (mockClients, mockClientProxy);
    }
}
