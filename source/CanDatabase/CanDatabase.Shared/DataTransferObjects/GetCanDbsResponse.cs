using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbsResponse
    /// </summary>
    public record GetCanDbsResponse
    {
        #region Properties
        public IEnumerable<CanDbListItem> CanDbs { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbsResponse Constructor
        /// </summary>
        [JsonConstructor]
        public GetCanDbsResponse(IEnumerable<CanDbListItem> canDbs)
        {
            CanDbs = canDbs;
        }
        #endregion Constructors
    }
}
