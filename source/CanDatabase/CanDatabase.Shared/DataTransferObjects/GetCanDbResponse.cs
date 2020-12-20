using System.Text.Json.Serialization;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbResponse
    /// </summary>
    public record GetCanDbResponse
    {
        #region Properties
        public CanDbDetails CanDb { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbResponse Constructor
        /// </summary>
        [JsonConstructor]
        public GetCanDbResponse(CanDbDetails canDb)
        {
            CanDb = canDb;
        }
        #endregion Constructors
    }
}
