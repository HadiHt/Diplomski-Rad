using BusinessAction.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessAction.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessActionController : ControllerBase
    {

        private readonly ILogger<BusinessActionController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BusinessActionController(ILogger<BusinessActionController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpPost("Execute-Action")]
        public async Task<IActionResult> ExecuteAction([FromBody] CamundaRequestModel request)
        {
            _logger.LogInformation($"Executing method: {nameof(ExecuteAction)}");
            string className = $"BusinessAction.API.Services.Actions.{request.processDefinitionKey.Split("_")[0]}Actions";
            Type type = Type.GetType(className);
            if (type == null)
            {
                _logger.LogError($"Class {className} not found.");
                return BadRequest($"Class {className} not found.");
            }
            var instance = _serviceProvider.GetService(type);
            MethodInfo method = type.GetMethod(request.TaskId);
            if (method == null)
            {
                _logger.LogError($"Method 'Execute' not found in class {className}.");
                return BadRequest($"Method 'Execute' not found in class {className}.");
            }
            try
            {
                var result = (Task<GenericResponseModel>)method.Invoke(instance, new object[] { request });
                return Ok(await result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the action.");
                return StatusCode(500, "An error occurred while executing the action.");
            }

        }
    }
}
