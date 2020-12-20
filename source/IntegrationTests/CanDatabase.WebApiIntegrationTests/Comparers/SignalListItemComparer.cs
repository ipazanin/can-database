using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CanDatabase.Shared.DataTransferObjects;

namespace CanDatabase.WebApiIntegrationTests.Comparers
{
    /// <summary>
    /// </summary>
    public class SignalListItemComparer : IEqualityComparer<SignalListItem>
    {
        public bool Equals(SignalListItem x, SignalListItem y)
        {
            var areEqual = x.Name.Equals(y.Name) &&
                x.StartBit.Equals(y.StartBit) &&
                x.Length.Equals(y.Length);
            return areEqual;
        }

        public int GetHashCode([DisallowNull] SignalListItem obj)
        {
            return HashCode.Combine(obj.Name, obj.StartBit, obj.Length);
        }
    }
}
