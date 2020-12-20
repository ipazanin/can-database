using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using CanDatabase.Domain.Models;

namespace CanDatabase.Shared.DataTransferObjects
{
    /// <summary>
    /// GetCanDbsCanDb Model
    /// </summary>
    public class CanDbListItem
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
        private CanDbListItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="originalFileName"></param>
        /// <param name="createTimeStamp"></param>
        [JsonConstructor]
        public CanDbListItem(
            int id,
            string originalFileName,
            DateTimeOffset createTimeStamp
        )
        {
            Id = id;
            OriginalFileName = originalFileName;
            CreateTimeStamp = createTimeStamp;
        }
        #endregion Constructors

        #region Methods
        public static Expression<Func<CanDb, CanDbListItem>> GetProjection()
        {
            return canDb => new CanDbListItem
            {
                Id = canDb.Id,
                OriginalFileName = canDb.OriginalFileName,
                CreateTimeStamp = canDb.CreateTimeStamp
            };
        }
        #endregion Methods
    }
}
