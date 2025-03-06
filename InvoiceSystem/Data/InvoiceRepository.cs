
using InvoiceSystem.Models;
using System.Collections.Generic;
using System.Linq;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly List<Invoice> _invoices = new();

    public void Add(Invoice invoice) => _invoices.Add(invoice);

    public Invoice? GetById(string id) => _invoices.FirstOrDefault(i => i.Id == id);

    public List<Invoice> GetAll() => _invoices;

    public void Update(Invoice invoice)
    {
        var index = _invoices.FindIndex(i => i.Id == invoice.Id);
        if (index != -1)
            _invoices[index] = invoice;
    }
}
