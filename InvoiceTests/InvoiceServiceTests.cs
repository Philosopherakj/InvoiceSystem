using System;
using System.Collections.Generic;
using InvoiceSystem.Models;
using Moq;
using Xunit;

public class InvoiceServiceTests
{
    private readonly Mock<IInvoiceRepository> _mockRepo;
    private readonly InvoiceService _service;

    public InvoiceServiceTests()
    {
        _mockRepo = new Mock<IInvoiceRepository>();
        _service = new InvoiceService(_mockRepo.Object);
    }

    [Fact]
    public void CreateInvoice_ShouldReturnInvoiceWithId()
    {
        var invoice = _service.CreateInvoice(199.99m, DateTime.UtcNow.AddDays(10));

        Assert.NotNull(invoice.Id);
        Assert.Equal(199.99m, invoice.Amount);
        Assert.Equal("pending", invoice.Status);
    }

    [Fact]
    public void PayInvoice_ShouldMarkAsPaid_WhenFullyPaid()
    {
        var invoice = new Invoice { Id = "1234", Amount = 100, PaidAmount = 0, Status = "pending" };
        _mockRepo.Setup(repo => repo.GetById("1234")).Returns(invoice);

        _service.PayInvoice("1234", 100);

        Assert.Equal("paid", invoice.Status);
    }

    [Fact]
    public void ProcessOverdue_ShouldCreateNewInvoice_WhenInvoiceIsOverdue()
    {
        var overdueInvoice = new Invoice
        {
            Id = "1234",
            Amount = 100,
            DueDate = DateTime.UtcNow.AddDays(-11),
            Status = "pending"
        };

        _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<Invoice> { overdueInvoice });

        _service.ProcessOverdue(10, 10);

        _mockRepo.Verify(repo => repo.Add(It.IsAny<Invoice>()), Times.Once);
        Assert.Equal("void", overdueInvoice.Status);
    }
}
