
using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CanDatabase.Domain.Models;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbNetworkNode Model
    /// </summary>
    public class NetworkNodeListItem
    {
        #region Properties

        #endregion Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = "";
        #region Constructors
        /// <summary>
        /// GetCanDbNetworkNode Constructor
        /// </summary>
        private NetworkNodeListItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        [JsonConstructor]
        public NetworkNodeListItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<NetworkNode, NetworkNodeListItem>> GetProjection()
        {
            return networkNode => new NetworkNodeListItem
            {
                Id = networkNode.Id,
                Name = networkNode.Name
            };
        }
        #endregion Methods
    }
}
