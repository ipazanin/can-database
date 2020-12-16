using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CanDatabase.Domain.IServices;
using CanDatabase.Persistence.Database;
using MediatR;

namespace CanDatabase.Application.CanDbs.Commands.ParseDbcFile
{
    /// <summary>
    /// ParseDbcFileHandler
    /// </summary>
    public class ParseDbcFileHandler : IRequestHandler<ParseDbcFileCommand, ParseDbcFileResponse>
    {
        #region Fields
        private readonly ICanDatabaseContext _databaseContext;
        private readonly IParseDbcFileService _parseDbcFileService;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// ParseDbcFileHandler Constructor
        /// </summary>
        public ParseDbcFileHandler(
            ICanDatabaseContext databaseContext,
            IParseDbcFileService parseDbcFileService
        )
        {
            _databaseContext = databaseContext;
            _parseDbcFileService = parseDbcFileService;
        }
        #endregion Constructors

        #region Methods
        public async Task<ParseDbcFileResponse> Handle(
            ParseDbcFileCommand request,
            CancellationToken cancellationToken
        )
        {
            var canDb = await _parseDbcFileService.ParseDbcFile(
                dbcFileStream: request.DbcFileStream,
                originalFileName: request.OriginalFileName
            );

            _databaseContext.CanDbs.Add(canDb);

            await _databaseContext.SaveChangesAsync(cancellationToken: cancellationToken);

            var response = new ParseDbcFileResponse(
                messagesCount: canDb.Messages.Count(),
                signalsCount: canDb.Messages
                    .Aggregate(
                        seed: 0,
                        func: (signalsCount, message) => signalsCount + message.Signals.Count()
                    ),
                networkNodesCount: canDb.NetworkNodes.Count()
            );

            return response;
        }
        #endregion Methods
    }
}
