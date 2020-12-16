namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbResponse
    /// </summary>
    public record GetCanDbResponse
    {
        #region Properties
        public GetCanDbCanDb CanDb { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbResponse Constructor
        /// </summary>
        public GetCanDbResponse(GetCanDbCanDb canDb)
        {
            CanDb = canDb;
        }
        #endregion Constructors
    }
}
