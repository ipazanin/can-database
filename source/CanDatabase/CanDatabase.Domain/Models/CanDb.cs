using System.Collections.Generic;

namespace CanDatabase.Domain.Models
{
    /// <summary>
    /// Can Database, Represents root .dbc file data
    /// </summary>
    public class CanDb
    {
        #region Constants
        #endregion Constants

        #region Properties
        /// <summary>
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// </summary>
        public IEnumerable<Message> Messages { get; private set; } = new List<Message>();

        /// <summary>
        /// </summary>
        public IEnumerable<NetworkNode> NetworkNodes { get; private set; } = new List<NetworkNode>();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private CanDb()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="messages"></param>
        /// <param name="networkNodes"></param>
        public CanDb(
            int id,
            IEnumerable<Message> messages,
            IEnumerable<NetworkNode> networkNodes
        )
        {
            Id = id;
            Messages = messages;
            NetworkNodes = networkNodes;
        }
        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}