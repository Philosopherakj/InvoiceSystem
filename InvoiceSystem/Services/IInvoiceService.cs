using InvoiceSystem.Models;
using System;
using System.Collections.Generic;

public interface IInvoiceService
{
    Invoice CreateInvoice(decimal amount, DateTime dueDate);
    List<Invoice> GetInvoices();
    void PayInvoice(string id, decimal amount);
    void ProcessOverdue(decimal lateFee, int overdueDays);
}
