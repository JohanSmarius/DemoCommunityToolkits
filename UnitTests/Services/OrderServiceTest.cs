using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Moq;
using ShopAPI.Exceptions;
using ShopAPI.Model;
using ShopAPI.Repository;
using ShopAPI.Services;

namespace UnitTests.Services;

[TestSubject(typeof(OrderService))]
public class OrderServiceTest
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTest()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderService = new OrderService(_orderRepositoryMock.Object);
    }

    [Fact]
    public void AddOrder_ValidOrderAndCustomer_ShouldAddOrder()
    {
        // Arrange
        var order = new Order { OrderStatus = OrderStatus.NEW };
        var customer = new Customer { Name = "Test", HasBackPayments = false };

        // Act
        _orderService.AddOrder(order, customer);

        // Assert
        _orderRepositoryMock.Verify(repo => repo.AddOrder(order), Times.Once);
    }
    
    [Fact]
    public void AddOrder_CustomerWithBackPayments_ShouldThrowOrderException()
    {
        // Arrange
        var order = new Order { OrderStatus = OrderStatus.NEW };
        var customer = new Customer { Name = "Test", HasBackPayments = true };

        // Act & Assert
        var isThrown = false;
        try
        {
            _orderService.AddOrder(order, customer);
        }
        catch (OrderException ex)
        {
            isThrown = true;
        }
        Assert.True(isThrown);
    }
    
    [Fact]
    public void AddOrder_OrderStatusNotNew_ShouldThrowOrderException()
    {
        // Arrange
        var order = new Order { OrderStatus = OrderStatus.SHIPPED };
        var customer = new Customer { Name = "Test", HasBackPayments = false };

        // Act & Assert
        Assert.Throws<OrderException>(() => _orderService.AddOrder(order, customer));
    }
    
    [Fact]
    public void AddOrder_NullOrder_ShouldThrowArgumentNullException()
    {
        // Arrange
        Order order = null;
        var customer = new Customer { Name = "Test", HasBackPayments = false };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _orderService.AddOrder(order, customer));
    }
    
    [Fact]
    public void AddOrder_NullCustomer_ShouldThrowArgumentNullException()
    {
        // Arrange
        var order = new Order { OrderStatus = OrderStatus.NEW };
        Customer customer = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _orderService.AddOrder(order, customer));
    }
    
    [Fact]
    public void Constructor_NullRepository_ShouldThrowArgumentNullException()
    {
        // Arrange
        IOrderRepository repository = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new OrderService(repository));
    }
    
}