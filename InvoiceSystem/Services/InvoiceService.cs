using InvoiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;

    public InvoiceService(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Invoice CreateInvoice(decimal amount, DateTime dueDate)
    {
        var invoice = new Invoice { Amount = amount, DueDate = dueDate };
        _repository.Add(invoice);
        return invoice;
    }

    public List<Invoice> GetInvoices() => _repository.GetAll();

    public void PayInvoice(string id, decimal amount)
    {
        var invoice = _repository.GetById(id);
        if (invoice == null)
            throw new Exception("Invoice not found");

        invoice.PaidAmount += amount;

        if (invoice.PaidAmount >= invoice.Amount)
            invoice.Status = "paid";

        _repository.Update(invoice);
    }

    public void ProcessOverdue(decimal lateFee, int overdueDays)
    {
        var overdueInvoices = _repository.GetAll()
            .Where(i => i.Status == "pending" && i.DueDate < DateTime.UtcNow) 
            .ToList();

        foreach (var invoice in overdueInvoices)
        {
            if (invoice.PaidAmount > 0) 
            {
                var newInvoice = new Invoice
                {
                    Id = Guid.NewGuid().ToString(),  
                    Amount = invoice.Amount - invoice.PaidAmount + lateFee,  
                    DueDate = DateTime.UtcNow.AddDays(overdueDays),  
                    Status = "pending"  
                };
                _repository.Add(newInvoice);

                invoice.PaidAmount = invoice.Amount; 
                invoice.Status = "paid"; 
            }
            else 
            {
                var newInvoice = new Invoice
                {
                    Id = Guid.NewGuid().ToString(),  
                    Amount = invoice.Amount + lateFee,  
                    DueDate = DateTime.UtcNow.AddDays(overdueDays),  
                    Status = "pending"  
                };
                _repository.Add(newInvoice);

                invoice.Status = "void"; 
            }

            _repository.Update(invoice); 
        }
    }

}
