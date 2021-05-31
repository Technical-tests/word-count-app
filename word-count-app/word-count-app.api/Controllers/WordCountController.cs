using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using word_count_app.common.Helpers;
using word_count_app.common.Models;

namespace word_count_app.api.Controllers
{
    /// <summary>
    /// Controlador para contar las palabras de un texto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WordCountController : ControllerBase
    {
        private readonly IWordCountProcessor _wordCountProcessor;

        public WordCountController(IWordCountProcessor wordCountProcessor)
        {
            _wordCountProcessor = wordCountProcessor ?? throw new ArgumentNullException(nameof(wordCountProcessor));
        }

        /// <summary>
        /// Contamos las palabras repetidas de un texto. Si viene una palabra como filtro, contamos cuantas veces se repite esta en el texto, de lo contrario hacemos un conteo de todas
        /// </summary>
        /// <param name="request">Texto original y la palabra de filtro</param>
        /// <returns>Resultados</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Count([FromBody] RequestWords request)
        {
            return string.IsNullOrWhiteSpace(request.OriginalText) ? BadRequest(request) : (ActionResult)Ok(await _wordCountProcessor.GetAmountAsync(request));
        }
    }
}
