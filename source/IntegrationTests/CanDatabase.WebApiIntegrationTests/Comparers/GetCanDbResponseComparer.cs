using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CanDatabase.Shared.DataTransferObjects;

namespace CanDatabase.WebApiIntegrationTests.Comparers
{
    /// <summary>
    /// </summary>
    public class GetCanDbResponseComparer : IEqualityComparer<GetCanDbResponse>
    {
        public bool Equals(GetCanDbResponse x, GetCanDbResponse y)
        {
            var comparer = new CanDbDetailsComparer();
            var areEqual = comparer.Equals(x.CanDb, y.CanDb);

            return areEqual;
        }

        public int GetHashCode([DisallowNull] GetCanDbResponse obj)
        {
            var comparer = new CanDbDetailsComparer();
            return comparer.GetHashCode(obj.CanDb);
        }
    }
}
