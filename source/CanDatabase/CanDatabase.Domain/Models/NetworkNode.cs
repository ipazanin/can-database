namespace CanDatabase.Domain.Models
{
    /// <summary>
    /// NetworkNode Model
    /// </summary>
    /// <remarks>
    ///     <para>Keyword: BU_</para>
    ///     <para>Description: List of all CAN-Nodes, seperated by whitespaces.</para>
    ///     <para>Example: BU_: ECUVariantIdent GW Tester ECU</para>
    /// </remarks>
    public class NetworkNode
    {
        #region Constants
        public const int NameMaxLength = 128;
        #endregion Constants

        #region Properties
        /// <summary>
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        public int CanDbId { get; private set; }

        /// <summary>
        /// </summary>
        public CanDb? CanDb { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ORM Constructor
        /// </summary>
        private NetworkNode()
        {
            Name = null!;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="canDbId"></param>
        /// <param name="canDb"></param>
        public NetworkNode(
            int id,
            string name,
            int canDbId,
            CanDb? canDb
        )
        {
            Id = id;
            Name = name;
            CanDbId = canDbId;
            CanDb = canDb;
        }
        #endregion Constructors

        #region Methods
        #endregion Methods
    }
}
