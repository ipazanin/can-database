using System;
using System.Linq.Expressions;
using CanDatabase.Domain.Models;

namespace CanDatabase.Application.CanDbs.Queries.GetCanDbs
{
    /// <summary>
    /// GetCanDbsCanDb Model
    /// </summary>
    public class GetCanDbsCanDb
    {
        #region Properties
        public int Id { get; private set; }

        public string OriginalFileName { get; private set; } = string.Empty;

        public DateTimeOffset CreateTimeStamp { get; private set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// GetCanDbsCanDb Constructor
        /// </summary>
        private GetCanDbsCanDb()
        {
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<CanDb, GetCanDbsCanDb>> GetProjection()
        {
            return canDb => new GetCanDbsCanDb
            {
                Id = canDb.Id,
                OriginalFileName = canDb.OriginalFileName,
                CreateTimeStamp = canDb.CreateTimeStamp
            };
        }
        #endregion Methods
    }
}
