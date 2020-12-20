using System.Text.Json.Serialization;

namespace CanDatabase.Shared.PaginationModels
{
    /// <summary>
    /// Paginationparameters Model
    /// </summary>
    public record PaginationParameters
    {
        #region Properties
        public int CurrentPage { get; init; }
        #endregion Properties

        #region Properties
        public int PageSize { get; init; }
        #endregion

        #region Methods
        public int Take() => PageSize;
        public int Skip() => (CurrentPage - 1) * PageSize;
        #endregion
    }
}
