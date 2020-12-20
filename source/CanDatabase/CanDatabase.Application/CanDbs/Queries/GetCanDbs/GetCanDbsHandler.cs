using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CanDatabase.Persistence.Database;
using CanDatabase.Shared.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDbs
{
    /// <summary>
    /// GetCanDbsHandler
    /// </summary>
    public class GetCanDbsHandler : IRequestHandler<GetCanDbsQuery, GetCanDbsResponse>
    {
        #region Fields
        private readonly ICanDatabaseContext _databaseContext;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// GetCanDbsHandler Constructor
        /// </summary>
        public GetCanDbsHandler(
            ICanDatabaseContext databaseContext
        )
        {
            _databaseContext = databaseContext;
        }
        #endregion Constructors

        #region Methods
        public async Task<GetCanDbsResponse> Handle(
            GetCanDbsQuery request,
            CancellationToken cancellationToken
        )
        {
            var canDbs = await _databaseContext
                .CanDbs
                .AsQueryable()
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(CanDbListItem.GetProjection())
                .ToListAsync(cancellationToken);

            var response = new GetCanDbsResponse(
                canDbs: canDbs
            );

            return response;
        }
        #endregion Methods
    }
}
