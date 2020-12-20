using CanDatabase.Shared.DataTransferObjects;
using CanDatabase.Shared.PaginationModels;
using MediatR;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDbs
{
    /// <summary>
    /// GetCanDbsQuery
    /// </summary>
    public record GetCanDbsQuery : IRequest<GetCanDbsResponse>
    {
        #region Properties
        public PaginationParameters PaginationParameters { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbsQuery Constructor
        /// </summary>
        public GetCanDbsQuery(PaginationParameters paginationParameters)
        {
            PaginationParameters = paginationParameters;
        }
        #endregion Constructors
    }
}
