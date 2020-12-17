
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CanDatabase.Domain.Models;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDb
{
    /// <summary>
    /// GetCanDbMessage Model
    /// </summary>
    public class GetCanDbMessage
    {
        #region Properties
        public int Id { get; private set; }

        public long CanId { get; private set; }

        public string Name { get; private set; } = "";

        public IEnumerable<GetCanDbSignal> Signals { get; private set; } = new List<GetCanDbSignal>();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbMessage Constructor
        /// </summary>
        private GetCanDbMessage()
        {
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<Message, GetCanDbMessage>> GetProjection()
        {
            var signalProjection = GetCanDbSignal.GetProjection();

            return message => new GetCanDbMessage
            {
                Id = message.Id,
                CanId = message.CanId,
                Name = message.Name,
                Signals = message
                    .Signals
                    .AsQueryable()
                    .Select(signalProjection)
                    .ToList()
            };
        }
        #endregion Methods
    }
}
