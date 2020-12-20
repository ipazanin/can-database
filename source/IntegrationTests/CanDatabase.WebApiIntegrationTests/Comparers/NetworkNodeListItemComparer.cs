using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CanDatabase.Shared.DataTransferObjects;

namespace CanDatabase.WebApiIntegrationTests.Comparers
{
    /// <summary>
    /// </summary>
    public class NetworkNodeListItemComparer : IEqualityComparer<NetworkNodeListItem>
    {
        public bool Equals(NetworkNodeListItem x, NetworkNodeListItem y)
        {
            var areEqual = x.Name.Equals(y.Name);
            return areEqual;
        }

        public int GetHashCode([DisallowNull] NetworkNodeListItem obj)
        {
            return HashCode.Combine(obj.Name);
        }
    }
}
