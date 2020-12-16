namespace CanDatabase.Domain.Models
{
    /// <summary>
    /// Signal Model
    /// </summary>
    /// <remarks>
    ///     <para>Keyword: SG_</para>
    ///     <para>
    ///         Description: 
    ///             SG_ {SignalName} [M|m{MultiplexerIdentifier}] : 
    ///             {StartBit}|{Length}@{Endianness}{Signed} 
    ///             ({Factor},{Offset}) [{Min}|{Max}] "[Unit]" [ReceivingNodes]
    ///     </para>
    ///     <para>Example: SG_ SawTooth : 32|32@1- (1,0) [0|0] "" Vector__XXX</para>
    /// </remarks>
    public class Signal
    {
        #region Constants
        public const int NameMaxLength = 128;

        public const string Keyword = "SG_";
        #endregion Constants

        #region Properties
        /// <summary>
        /// Application Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        public int StartBit { get; private set; }

        /// <summary>
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// </summary>
        public int MessageId { get; private set; }

        /// <summary>
        /// </summary>
        public Message? Message { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private Signal()
        {
            Name = null!;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="startBit"></param>
        /// <param name="length"></param>
        /// <param name="messageId"></param>
        /// <param name="message"></param>
        public Signal(
            int id,
            string name,
            int startBit,
            int length,
            int messageId,
            Message? message
        )
        {
            Id = id;
            Name = name;
            StartBit = startBit;
            Length = length;
            MessageId = messageId;
            Message = message;
        }
        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
