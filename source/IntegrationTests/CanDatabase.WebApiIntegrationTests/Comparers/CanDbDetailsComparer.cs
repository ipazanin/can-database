using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CanDatabase.Shared.DataTransferObjects;

namespace CanDatabase.WebApiIntegrationTests.Comparers
{
    /// <summary>
    /// </summary>
    public class CanDbDetailsComparer : IEqualityComparer<CanDbDetails>
    {
        public bool Equals(CanDbDetails x, CanDbDetails y)
        {
            var networkNodeComparer = new NetworkNodeListItemComparer();
            var messageComparer = new MessageListItemComparer();

            var areEqual = x.OriginalFileName.Equals(y.OriginalFileName) &&
                x.Messages
                    .All(
                        xMessage => y
                            .Messages
                            .Any(yMessage => messageComparer.Equals(xMessage, yMessage))
                    ) &&
                x.NetworkNodes
                    .All(
                        xNetworkNode => y
                            .NetworkNodes
                            .Any(yNetworkNode => networkNodeComparer.Equals(xNetworkNode, yNetworkNode))
                    );

            return areEqual;
        }

        public int GetHashCode([DisallowNull] CanDbDetails obj)
        {
            var networkNodeComparer = new NetworkNodeListItemComparer();
            var messageComparer = new MessageListItemComparer();

            // CreateTimeStamp is not included as comparison argument
            var hashCode = HashCode.Combine(
                obj.OriginalFileName
            );

            foreach (var networkNode in obj.NetworkNodes)
            {
                hashCode = HashCode.Combine(hashCode, networkNodeComparer.GetHashCode(networkNode));
            }

            foreach (var message in obj.Messages)
            {
                hashCode = HashCode.Combine(hashCode, messageComparer.GetHashCode(message));
            }

            return hashCode;
        }
    }
}
