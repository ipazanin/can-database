using System.Collections.Generic;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDbs
{
    /// <summary>
    /// GetCanDbsResponse
    /// </summary>
    public record GetCanDbsResponse
    {
        #region Properties
        public IEnumerable<GetCanDbsCanDb> CanDbs { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbsResponse Constructor
        /// </summary>
        public GetCanDbsResponse(IEnumerable<GetCanDbsCanDb> canDbs)
        {
            CanDbs = canDbs;
        }
        #endregion Constructors
    }
}
