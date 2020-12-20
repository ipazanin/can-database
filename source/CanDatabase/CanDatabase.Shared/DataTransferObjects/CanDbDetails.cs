using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CanDatabase.Domain.Models;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbCanDb Model
    /// </summary>
    public class CanDbDetails
    {
        #region Constants
        #endregion Constants

        #region Properties
        public int Id { get; private set; }

        public string OriginalFileName { get; private set; } = string.Empty;

        public DateTimeOffset CreateTimeStamp { get; private set; }

        public IEnumerable<MessageListItem> Messages { get; private set; } = new List<MessageListItem>();

        public IEnumerable<NetworkNodeListItem> NetworkNodes { get; private set; } = new List<NetworkNodeListItem>();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbCanDb Constructor
        /// </summary>
        private CanDbDetails()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="originalFileName"></param>
        /// <param name="createTimeStamp"></param>
        /// <param name="messages"></param>
        /// <param name="networkNodes"></param>
        [JsonConstructor]
        public CanDbDetails(int id, string originalFileName, DateTimeOffset createTimeStamp, IEnumerable<MessageListItem> messages, IEnumerable<NetworkNodeListItem> networkNodes)
        {
            Id = id;
            OriginalFileName = originalFileName;
            CreateTimeStamp = createTimeStamp;
            Messages = messages;
            NetworkNodes = networkNodes;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<CanDb, CanDbDetails>> GetProjection()
        {
            var messageProjection = MessageListItem.GetProjection();
            var networkNodeProjection = NetworkNodeListItem.GetProjection();
            return canDb => new CanDbDetails
            {
                Id = canDb.Id,
                OriginalFileName = canDb.OriginalFileName,
                CreateTimeStamp = canDb.CreateTimeStamp,
                Messages = canDb
                    .Messages
                    .AsQueryable()
                    .Select(messageProjection)
                    .ToList(),
                NetworkNodes = canDb
                    .NetworkNodes
                    .AsQueryable()
                    .Select(networkNodeProjection)
                    .ToList()
            };
        }
        #endregion Methods
    }
}
