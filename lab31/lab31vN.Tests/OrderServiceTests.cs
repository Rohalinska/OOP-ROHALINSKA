using Xunit;
using Moq;
using lab31vN;
using System;

namespace lab31vN.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _repoMock;
        private readonly Mock<IEmailService> _emailMock;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _repoMock = new Mock<IOrderRepository>();
            _emailMock = new Mock<IEmailService>();
            _service = new OrderService(_repoMock.Object, _emailMock.Object);
        }

        // ---------- TESTS ----------
        //№1
        [Fact]
        public void CreateOrder_ValidOrder_CallsSave()
        {
            var order = new Order { Id = 1, Email = "test@test.com", Amount = 100 };

            _service.CreateOrder(order);

            _repoMock.Verify(r => r.Save(order), Times.Once);
        }
        //№2
        [Fact]
        public void CreateOrder_ValidOrder_SendsEmail()
        {
            var order = new Order { Id = 1, Email = "test@test.com", Amount = 100 };

            _service.CreateOrder(order);

            _emailMock.Verify(e => e.Send(order.Email, "Order created"), Times.Once);
        }
        //№3
        [Fact]
        public void CreateOrder_NullOrder_Throws()
        {
            Assert.Throws<ArgumentException>(() => _service.CreateOrder(null));
        }
        //№4
        [Fact]
        public void CreateOrder_InvalidAmount_Throws()
        {
            var order = new Order { Amount = 0 };

            Assert.Throws<ArgumentException>(() => _service.CreateOrder(order));
        }
        //№5  
        [Fact]
        public void GetOrder_ReturnsOrder_FromRepository()
        {
            var order = new Order { Id = 1 };

            _repoMock.Setup(r => r.GetById(1)).Returns(order);

            var result = _service.GetOrder(1);

            Assert.Equal(order, result);
        }
        //№6
        [Fact]
        public void GetOrder_CallsRepository()
        {
            _repoMock.Setup(r => r.GetById(1)).Returns(new Order());

            _service.GetOrder(1);

            _repoMock.Verify(r => r.GetById(1), Times.Once);
        }
        //№7
        [Fact]
        public void CreateOrder_SaveCalledOnce()
        {
            var order = new Order { Id = 2, Email = "a@a.com", Amount = 50 };

            _service.CreateOrder(order);

            _repoMock.Verify(r => r.Save(order), Times.Once);
        }
        //№8
        [Fact]
        public void CreateOrder_EmailCalledOnce()
        {
            var order = new Order { Id = 2, Email = "a@a.com", Amount = 50 };

            _service.CreateOrder(order);

            _emailMock.Verify(e => e.Send(order.Email, "Order created"), Times.Once);
        }
    }
}