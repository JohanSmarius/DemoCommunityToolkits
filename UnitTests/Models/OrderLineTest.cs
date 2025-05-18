using UnitTests.Models;
using JetBrains.Annotations;
using ShopAPI.Model;
using Xunit;

namespace UnitTests.Models;

[TestSubject(typeof(OrderLine))]
public class OrderLineTest
{
    [Fact]
    public void Quantity_SetValidValue_ShouldUpdateQuantity()
    {
        var orderLine = new OrderLine();

        orderLine.Quantity = 50;

        Assert.Equal(50, orderLine.Quantity);
    }
    

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Quantity_SetInvalidValue_ShouldThrowArgumentException_NegativeOrZero(int invalidValue)
    {
        var orderLine = new OrderLine();

        Assert.Throws<ArgumentOutOfRangeException>(() => orderLine.Quantity = invalidValue);
    }

    [Fact]
    public void Quantity_SetValidValueAtMax_ShouldUpdateQuantity()
    {
        var orderLine = new OrderLine();

        orderLine.Quantity = 100;

        Assert.Equal(100, orderLine.Quantity);
    }

    
    [Fact]
    public void Quantity_SetInvalidValue_ShouldThrowArgumentException_AboveMaximum()
    {
        var orderLine = new OrderLine();

        Assert.Throws<ArgumentOutOfRangeException>(() => orderLine.Quantity = 101);
    }

    [Fact]
    public void DiscountPercentage_SetValidValue_ShouldUpdateDiscount()
    {
        var orderLine = new OrderLine();

        orderLine.DiscountPercentage = 25.5m;

        Assert.Equal(25.5m, orderLine.DiscountPercentage);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(150)]
    [InlineData(-1)]
    [InlineData(101)]
    public void DiscountPercentage_SetInvalidValue_ShouldThrowArgumentException(decimal invalidValue)
    {
        var orderLine = new OrderLine();

        Assert.Throws<ArgumentOutOfRangeException>(() => orderLine.DiscountPercentage = invalidValue);
    }

    [Fact]
    public void Order_SetAndGet_ShouldWorkProperly()
    {
        var order = new Order { Id = 1 };
        var orderLine = new OrderLine { Order = order };

        Assert.Equal(order, orderLine.Order);
    }

    [Fact]
    public void Product_SetAndGet_ShouldWorkProperly()
    {
        var product = new Product { Id = 1, Name = "Test Product" };
        var orderLine = new OrderLine { Product = product };

        Assert.Equal(product, orderLine.Product);
    }

    [Fact]
    public void Id_Property_ShouldSetAndGetCorrectly()
    {
        var orderLine = new OrderLine { Id = 5 };

        Assert.Equal(5, orderLine.Id);
    }

    [Fact]
    public void OrderId_Property_ShouldSetAndGetCorrectly()
    {
        var orderLine = new OrderLine { OrderId = 10 };

        Assert.Equal(10, orderLine.OrderId);
    }

    [Fact]
    public void ProductId_Property_ShouldSetAndGetCorrectly()
    {
        var orderLine = new OrderLine { ProductId = 20 };

        Assert.Equal(20, orderLine.ProductId);
    }
}