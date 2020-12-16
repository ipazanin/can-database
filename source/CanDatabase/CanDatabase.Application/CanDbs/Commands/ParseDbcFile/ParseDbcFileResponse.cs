namespace CanDatabase.Application.CanDbs.Commands.ParseDbcFile
{
    /// <summary>
    /// ParseDbcFileResponse
    /// </summary>
    public record ParseDbcFileResponse
    {
        #region Properties
        public int MessagesCount { get; }
        public int SignalsCount { get; }
        public int NetworkNodesCount { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ParseDbcFileResponse Constructor
        /// </summary>
        public ParseDbcFileResponse(
            int messagesCount,
            int signalsCount,
            int networkNodesCount
        )
        {
            MessagesCount = messagesCount;
            SignalsCount = signalsCount;
            NetworkNodesCount = networkNodesCount;
        }
        #endregion Constructors
    }
}
