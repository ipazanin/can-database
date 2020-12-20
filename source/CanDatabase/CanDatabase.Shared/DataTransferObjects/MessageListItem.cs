
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CanDatabase.Domain.Models;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbMessage Model
    /// </summary>
    public class MessageListItem
    {
        #region Properties
        public int Id { get; private set; }

        public long CanId { get; private set; }

        public string Name { get; private set; } = "";

        public IEnumerable<SignalListItem> Signals { get; private set; } = new List<SignalListItem>();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbMessage Constructor
        /// </summary>
        private MessageListItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="canId"></param>
        /// <param name="name"></param>
        /// <param name="signals"></param>
        [JsonConstructor]
        public MessageListItem(
            int id,
            long canId,
            string name,
            IEnumerable<SignalListItem> signals
        )
        {
            Id = id;
            CanId = canId;
            Name = name;
            Signals = signals;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<Message, MessageListItem>> GetProjection()
        {
            var signalProjection = SignalListItem.GetProjection();

            return message => new MessageListItem
            {
                Id = message.Id,
                CanId = message.CanId,
                Name = message.Name,
                Signals = message
                    .Signals
                    .AsQueryable()
                    .Select(signalProjection)
                    .ToList()
            };
        }
        #endregion Methods
    }
}
