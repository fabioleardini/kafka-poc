using KafkaPoc.Domain.Entities;
using KafkaPoc.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KafkaPoc.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController(IMessageProducer messageProducer) : ControllerBase
    {
        private readonly IMessageProducer _messageProducer = messageProducer;

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] TestObject content, CancellationToken cancellationToken)
        {
            try
            {
                await _messageProducer.ProduceAsync(content, cancellationToken);

                return Ok(new { message = "Message sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}