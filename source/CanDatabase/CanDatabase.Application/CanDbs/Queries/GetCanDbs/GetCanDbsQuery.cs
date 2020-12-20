using CanDatabase.Shared.DataTransferObjects;
using MediatR;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDbs
{
    /// <summary>
    /// GetCanDbsQuery
    /// </summary>
    public record GetCanDbsQuery : IRequest<GetCanDbsResponse>
    {
        #region Properties
        public int Take { get; }

        public int Skip { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbsQuery Constructor
        /// </summary>
        public GetCanDbsQuery(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
        #endregion Constructors
    }
}
