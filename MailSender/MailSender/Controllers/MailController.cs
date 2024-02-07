using MailSender.Models;
using MailSender.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.Controllers;

[ApiController]
[Route("[controller]")]
public class MailController : ControllerBase
{
    private readonly SenderService _senderService;

    public MailController(SenderService senderService)
    {
        _senderService = senderService;
    }

    [HttpPost]
    public ActionResult<bool> Send([FromBody] SenderDto sender)
    {
        try
        {
            _senderService.SendToMe(sender);
            return Ok(true);
        }
        catch
        {
            return BadRequest("Nie uda³o siê wys³aæ wiadmoœci");
        }
    }
}