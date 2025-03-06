using InvoiceSystem.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("invoices")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _service;

    public InvoicesController(IInvoiceService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult CreateInvoice([FromBody] Invoice request)
    {
        var invoice = _service.CreateInvoice(request.Amount, request.DueDate);
        return Created($"/invoices/{invoice.Id}", new { id = invoice.Id });
    }

    [HttpGet]
    public IActionResult GetInvoices() => Ok(_service.GetInvoices());

    [HttpPost("{id}/payments")]
    public IActionResult PayInvoice(string id, [FromBody] PaymentRequest request)
    {
        _service.PayInvoice(id, request.Amount);
        return NoContent();
    }

    [HttpPost("process-overdue")]
    public IActionResult ProcessOverdue([FromBody] OverdueRequest request)
    {
        _service.ProcessOverdue((decimal)request.LateFee, (int)request.OverdueDays);
        return NoContent();
    }
}
