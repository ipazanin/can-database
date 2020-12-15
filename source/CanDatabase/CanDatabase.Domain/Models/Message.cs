using System.Collections.Generic;

namespace CanDatabase.Domain.Models
{
    /// <summary>
    /// Message Model
    /// </summary>
    /// <remarks>
    ///     <para>Keyword: BO_</para>
    ///     <para>Description: BO_ {CAN-ID} {MessageName}: {MessageLength} {SendingNode}</para>
    ///     <para>Example: BO_ 257 SendStoreData: 8 Tester</para>
    /// </remarks>
    public class Message
    {
        #region Constants
        public const int NameMaxLength = 128;
        #endregion Constants

        #region Properties
        /// <summary>
        /// Application Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// </summary>
        public int CanId { get; private set; }

        /// <summary>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        public int CanDbId { get; private set; }

        /// <summary>
        /// </summary>
        public CanDb? CanDb { get; private set; }

        /// <summary>
        /// </summary>
        public IEnumerable<Signal> Signals { get; private set; } = new List<Signal>();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private Message()
        {
            Name = null!;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="canId"></param>
        /// <param name="name"></param>
        /// <param name="canDbId"></param>
        /// <param name="canDb"></param>
        /// <param name="signals"></param>
        public Message(
            int id,
            int canId,
            string name,
            int canDbId,
            CanDb? canDb,
            IEnumerable<Signal> signals
        )
        {
            Id = id;
            CanId = canId;
            Name = name;
            CanDbId = canDbId;
            CanDb = canDb;
            Signals = signals;
        }
        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
