using System.Threading;
using System.Threading.Tasks;
using CanDatabase.Application.CanDbs.Commands.DeleteCanDb;
using CanDatabase.Application.CanDbs.Commands.ParseDbcFile;
using CanDatabase.Application.CanDbs.Queries.GetCanDb;
using CanDatabase.Application.CanDbs.Queries.GetCanDbs;
using CanDatabase.Common.Constants;
using CanDatabase.Shared.DataTransferObjects;
using CanDatabase.WebApi.Configuration;
using CanDatabase.WebApi.DataTransferObjects;
using CanDatabase.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CanDatabase.WebApi.Controllers
{
    /// <summary>
    /// CanDbController
    /// </summary>
    public class CanDbController : ApiController
    {
        #region Constructors
        /// <summary>
        /// CanDbController Constructor
        /// </summary>
        public CanDbController(
            ISender sender,
            IWebApiConfiguration webApiConfiguration
        ) : base(
                sender: sender,
                webApiConfiguration: webApiConfiguration
            )
        {
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Get Can DB's
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/can-db
        /// </remarks>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing Can Db's</returns>
        /// <response code="200">Response object containing Can Db's</response>
        /// <response code="400">If validation fails</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCanDbsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.0")]
        [HttpGet]
        [Route("api/can-db")]
        public async Task<IActionResult> GetCanDbs(
            [FromQuery] int take,
            [FromQuery] int skip,
            CancellationToken cancellationToken
        )
        {
            var response = await Sender.Send(
                request: new GetCanDbsQuery(take: take, skip: skip),
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        /// <summary>
        /// Get Can DB
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Get /api/can-db/1
        /// </remarks>
        /// <param name="id">Can Db ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response object containing Can Db</returns>
        /// <response code="200">Response object containing Can Db</response>
        /// <response code="400">If validation fails</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCanDbResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.0")]
        [HttpGet]
        [Route("api/can-db/{id}")]
        public async Task<IActionResult> GetCanDb(
            [FromRoute] int id,
            CancellationToken cancellationToken
        )
        {
            var response = await Sender.Send(
                request: new GetCanDbQuery(canDbId: id),
                cancellationToken: cancellationToken
            );

            return Ok(response);
        }

        /// <summary>
        /// Parse .dbc file
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/can-db
        /// </remarks>
        /// <param name="file">Uploaded .dbc file</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="201"></response>
        /// <response code="400">If validation fails</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.0")]
        [HttpPost]
        [Route("api/can-db")]
        public async Task<IActionResult> ParseDbc(
            IFormFile file,
            CancellationToken cancellationToken
        )
        {
            var response = await Sender.Send(
                request: new ParseDbcFileCommand(
                    dbcFileStream: file.OpenReadStream(),
                    originalFileName: file.FileName
                ),
                cancellationToken: cancellationToken
            );

            return CreatedAtAction(nameof(ParseDbc), response);
        }

        /// <summary>
        /// Get Can DB
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Delete /api/can-db/1
        /// </remarks>
        /// <param name="id">Can Db ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="400">If validation fails</response>
        /// <response code="500">If unhandled exception occurred</response>
        [Produces(MimeTypeConstants.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCanDbResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
        [ApiVersion("1.0")]
        [HttpDelete]
        [Route("api/can-db/{id}")]
        public async Task<IActionResult> DeleteCanDb(
            [FromRoute] int id,
            CancellationToken cancellationToken
        )
        {
            await Sender.Send(
                request: new DeleteCanDbCommand(canDbId: id),
                cancellationToken: cancellationToken
            );

            return NoContent();
        }
        #endregion Methods
    }
}
