using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CanDatabase.Shared.DataTransferObjects;

namespace CanDatabase.WebApiIntegrationTests.Comparers
{
    /// <summary>
    /// </summary>
    public class MessageListItemComparer : IEqualityComparer<MessageListItem>
    {
        public bool Equals(MessageListItem x, MessageListItem y)
        {
            var signalComparer = new SignalListItemComparer();
            var areEqual = x.Name.Equals(y.Name) &&
                x.Signals
                    .All(
                        xSignal => y
                            .Signals
                            .Any(ySignal => signalComparer.Equals(xSignal, ySignal))
                    );

            return areEqual;
        }

        public int GetHashCode([DisallowNull] MessageListItem obj)
        {
            var signalComparer = new SignalListItemComparer();
            var hashCode = HashCode.Combine(obj.Name);

            foreach (var signal in obj.Signals)
            {
                hashCode = HashCode.Combine(hashCode, signalComparer.GetHashCode(signal));
            }

            return hashCode;
        }
    }
}
