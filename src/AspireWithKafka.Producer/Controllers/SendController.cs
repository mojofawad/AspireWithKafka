using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace AspireWithKafka.Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class SendController : ControllerBase
{
    private readonly IProducer<string, string> _producer;

    public SendController(IProducer<string, string> producer)
    {
        _producer = producer;
    }
    
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(
        [FromQuery] string key,
        [FromQuery] string value,
        CancellationToken stoppingToken)
    {
        var message = new Message<string, string>
        {
            Key = key,
            Value = value
        };

        var result = await _producer.ProduceAsync("messaging", message, stoppingToken);

        return Ok(new { partition = result.Partition.Value, offset = result.Offset.Value });
    }

}