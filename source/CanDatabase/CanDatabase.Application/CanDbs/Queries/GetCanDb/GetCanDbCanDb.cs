using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CanDatabase.Domain.Models;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbCanDb Model
    /// </summary>
    public class GetCanDbCanDb
    {
        #region Constants
        #endregion Constants

        #region Properties
        public int Id { get; private set; }

        public string OriginalFileName { get; private set; } = string.Empty;

        public DateTimeOffset CreateTimeStamp { get; private set; }

        public IEnumerable<GetCanDbMessage> Messages { get; private set; } = new List<GetCanDbMessage>();

        public IEnumerable<GetCanDbNetworkNode> NetworkNodes { get; private set; } = new List<GetCanDbNetworkNode>();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbCanDb Constructor
        /// </summary>
        private GetCanDbCanDb()
        {
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<CanDb, GetCanDbCanDb>> GetProjection()
        {
            var messageProjection = GetCanDbMessage.GetProjection();
            var networkNodeProjection = GetCanDbNetworkNode.GetProjection();
            return canDb => new GetCanDbCanDb
            {
                Id = canDb.Id,
                OriginalFileName = canDb.OriginalFileName,
                CreateTimeStamp = canDb.CreateTimeStamp,
                Messages = canDb
                    .Messages
                    .AsQueryable()
                    .Select(messageProjection),
                NetworkNodes = canDb
                    .NetworkNodes
                    .AsQueryable()
                    .Select(networkNodeProjection)
            };
        }
        #endregion Methods
    }
}
