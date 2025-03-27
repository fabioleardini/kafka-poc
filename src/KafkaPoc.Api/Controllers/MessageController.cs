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
        private const string Topic = "poems";

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string[] jsonContent, CancellationToken cancellationToken)
        {
            try
            {
                if (jsonContent == null || jsonContent.Length == 0)
                {
                    return BadRequest(new { message = "No content provided" });
                }

                foreach (var content in jsonContent)
                {
                    var message = new Message
                    {
                        Id = Guid.NewGuid().ToString(),
                        Content = content,
                        Topic = Topic
                    };

                    await _messageProducer.ProduceAsync(message, cancellationToken);
                }

                return Ok(new { message = "Message sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}