using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CanDatabase.Application.Exceptions;
using CanDatabase.Domain.Models;
using CanDatabase.Persistence.Database;
using CanDatabase.Shared.DataTransferObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbHandler
    /// </summary>
    public class GetCanDbHandler : IRequestHandler<GetCanDbQuery, GetCanDbResponse>
    {
        #region Fields
        private readonly ICanDatabaseContext _databaseContext;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// GetCanDbHandler Constructor
        /// </summary>
        public GetCanDbHandler(
            ICanDatabaseContext databaseContext
        )
        {
            _databaseContext = databaseContext;
        }
        #endregion Constructors

        #region Methods
        public async Task<GetCanDbResponse> Handle(
            GetCanDbQuery request,
            CancellationToken cancellationToken
        )
        {
            var canDb = await _databaseContext
                .CanDbs
                .AsQueryable()
                .Select(CanDbDetails.GetProjection())
                .FirstOrDefaultAsync(
                    predicate: canDb => canDb.Id == request.CanDbId,
                    cancellationToken: cancellationToken
                );

            if (canDb is null)
            {
                throw new NotFoundException(nameof(CanDb), request.CanDbId);
            }

            var response = new GetCanDbResponse(canDb: canDb);

            return response;
        }
        #endregion Methods
    }
}
