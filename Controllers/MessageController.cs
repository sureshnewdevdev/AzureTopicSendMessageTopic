using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ServiceBusHelper _serviceBusHelper;

    public MessageController(ServiceBusHelper serviceBusHelper)
    {
        _serviceBusHelper = serviceBusHelper;
    }

    // Action method to send a message with msg = "m"
    [HttpPost("sendMorningMessage")]
    public async Task<IActionResult> SendMorningMessage([FromBody] string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return BadRequest("Message cannot be empty.");
        }

        try
        {
            await _serviceBusHelper.SendMessageAsync(message, "m");
            return Ok("Morning message sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Action method to send a message with msg = "e"
    [HttpPost("sendEveningMessage")]
    public async Task<IActionResult> SendEveningMessage([FromBody] string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return BadRequest("Message cannot be empty.");
        }

        try
        {
            await _serviceBusHelper.SendMessageAsync(message, "e");
            return Ok("Evening message sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
