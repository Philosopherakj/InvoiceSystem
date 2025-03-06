using InvoiceSystem.Models;
using System.Collections.Generic;

public interface IInvoiceRepository
{
    void Add(Invoice invoice);
    Invoice? GetById(string id);
    List<Invoice> GetAll();
    void Update(Invoice invoice);
}
