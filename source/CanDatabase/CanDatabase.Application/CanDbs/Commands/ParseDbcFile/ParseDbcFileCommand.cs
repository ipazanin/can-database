using System.IO;
using MediatR;

namespace CanDatabase.Application.CanDbs.Commands.ParseDbcFile
{
    /// <summary>
    /// ParseDbcFileCommand
    /// </summary>
    public record ParseDbcFileCommand : IRequest<ParseDbcFileResponse>
    {
        #region Properties
        public Stream DbcFileStream { get; }

        public string OriginalFileName { get; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// ParseDbcFileCommand Constructor
        /// </summary>
        public ParseDbcFileCommand(
            Stream dbcFileStream,
            string originalFileName
        )
        {
            DbcFileStream = dbcFileStream;
            OriginalFileName = originalFileName;
        }
        #endregion Constructors
    }
}
