
using System;
using System.Linq.Expressions;
using CanDatabase.Domain.Models;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbSignal Model
    /// </summary>
    public class GetCanDbSignal
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = "";

        public int StartBit { get; private set; }

        public int Length { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbSignal Constructor
        /// </summary>
        private GetCanDbSignal()
        {
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<Signal, GetCanDbSignal>> GetProjection()
        {
            return signal => new GetCanDbSignal
            {
                Id = signal.Id,
                Name = signal.Name,
                StartBit = signal.StartBit,
                Length = signal.Length
            };
        }
        #endregion Methods
    }
}
