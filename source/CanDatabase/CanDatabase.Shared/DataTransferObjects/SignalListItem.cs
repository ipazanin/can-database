
using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CanDatabase.Domain.Models;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbSignal Model
    /// </summary>
    public class SignalListItem
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = "";

        public int StartBit { get; private set; }

        public int Length { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbSignal Constructor
        /// </summary>
        private SignalListItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="startBit"></param>
        /// <param name="length"></param>
        [JsonConstructor]
        public SignalListItem(
            int id,
            string name,
            int startBit,
            int length
        )
        {
            Id = id;
            Name = name;
            StartBit = startBit;
            Length = length;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<Signal, SignalListItem>> GetProjection()
        {
            return signal => new SignalListItem
            {
                Id = signal.Id,
                Name = signal.Name,
                StartBit = signal.StartBit,
                Length = signal.Length
            };
        }
        #endregion Methods
    }
}
