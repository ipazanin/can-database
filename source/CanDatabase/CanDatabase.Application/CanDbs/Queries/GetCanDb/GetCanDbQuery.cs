using MediatR;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbQuery
    /// </summary>
    public record GetCanDbQuery : IRequest<GetCanDbResponse>
    {
        #region Properties
        public int CanDbId { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbQuery Constructor
        /// </summary>
        public GetCanDbQuery(int canDbId)
        {
            CanDbId = canDbId;
        }
        #endregion Constructors
    }
}
