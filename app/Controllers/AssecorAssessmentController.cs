using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.SampleData.Common;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssecorAssessmentController : ControllerBase
    {
        private readonly ISampleDataManager manager;
        private readonly ILogger<AssecorAssessmentController> logger;

        public AssecorAssessmentController(
            ISampleDataManager manager,
            ILogger<AssecorAssessmentController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        [HttpGet("persons/")]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetPersonsAsync()
        {
            try
            {
                var personsResponse = await manager.GetPersonsResponseAsync();
                return Ok(personsResponse);
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Problem(exception.Message + " " + exception.InnerException.ToString(), "", 
                    (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("persons/{id}")]
        public async Task<ActionResult<PersonResponse>> GetPersonsByIdAsync(int id)
        {
            try
            {
                var personResponse = await manager.GetPersonResponseByIdAsync(id);
                return Ok(personResponse);
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Problem(exception.Message + " " + exception.InnerException.ToString(), "", 
                    (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("persons/color/{color}")]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetPersonsByColorAsync(Color color)
        {
            try
            {
                var personsResponse = await manager.GetPersonsResponseByColorAsync(color);
                return Ok(personsResponse);
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Problem(exception.Message + " " + exception.InnerException.ToString(), "", 
                    (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("persons/")]
        public async Task<ActionResult> SetPersonAsync(PersonResponse personResponse)
        {
            try
            {
                await manager.SetPersonResponseAsync(personResponse);
                return Ok();
            }
            catch (Exception exception)
            {
                LogException(exception);
                return Problem(exception.Message + " " + exception.InnerException.ToString(), "", 
                    (int)HttpStatusCode.InternalServerError);
            }
        }

        private void LogException(Exception exception)
        {
            logger.LogError(
                exception,
                "Error executing {controllerName}",
                nameof(AssecorAssessmentController));
        }
    }
}
