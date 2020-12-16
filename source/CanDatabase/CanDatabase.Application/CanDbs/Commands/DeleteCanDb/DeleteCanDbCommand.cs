using MediatR;

namespace CanDatabase.Application.CanDbs.Commands.DeleteCanDb
{
    /// <summary>
    /// DeleteCanDbCommand
    /// </summary>
    public record DeleteCanDbCommand : IRequest
    {
        #region Properties
        public int CanDbId { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// DeleteCanDbCommand Constructor
        /// </summary>
        public DeleteCanDbCommand(int canDbId)
        {
            CanDbId = canDbId;
        }
        #endregion Constructors
    }
}
