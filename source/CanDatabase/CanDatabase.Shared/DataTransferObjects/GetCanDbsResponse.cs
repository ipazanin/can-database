using System.Text.Json.Serialization;
using CanDatabase.Shared.PaginationModels;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbsResponse
    /// </summary>
    public record GetCanDbsResponse
    {
        #region Properties
        public PagedList<CanDbListItem> PagedCanDbs { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbsResponse Constructor
        /// </summary>
        [JsonConstructor]
        public GetCanDbsResponse(PagedList<CanDbListItem> pagedCanDbs)
        {
            PagedCanDbs = pagedCanDbs;
        }
        #endregion Constructors
    }
}
