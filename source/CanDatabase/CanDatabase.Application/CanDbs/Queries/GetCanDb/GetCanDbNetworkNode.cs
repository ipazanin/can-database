
using System;
using System.Linq.Expressions;
using CanDatabase.Domain.Models;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbNetworkNode Model
    /// </summary>
    public class GetCanDbNetworkNode
    {
        #region Properties

        #endregion Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = "";
        #region Constructors
        /// <summary>
        /// GetCanDbNetworkNode Constructor
        /// </summary>
        private GetCanDbNetworkNode()
        {
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<NetworkNode, GetCanDbNetworkNode>> GetProjection()
        {
            return networkNode => new GetCanDbNetworkNode
            {
                Id = networkNode.Id,
                Name = networkNode.Name
            };
        }
        #endregion Methods
    }
}
