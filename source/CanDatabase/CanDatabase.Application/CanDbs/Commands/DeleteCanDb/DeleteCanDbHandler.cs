using System.Threading;
using System.Threading.Tasks;
using CanDatabase.Application.Exceptions;
using CanDatabase.Domain.Models;
using CanDatabase.Persistence.Database;
using MediatR;

namespace CanDatabase.Application.CanDbs.Commands.DeleteCanDb
{
    /// <summary>
    /// DeleteCanDbHandler
    /// </summary>
    public class DeleteCanDbHandler : IRequestHandler<DeleteCanDbCommand>
    {
        #region Fields
        private readonly ICanDatabaseContext _databaseContext;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// DeleteCanDbHandler Constructor
        /// </summary>
        public DeleteCanDbHandler(
            ICanDatabaseContext databaseContext
        )
        {
            _databaseContext = databaseContext;
        }
        #endregion Constructors

        #region Methods
        public async Task<Unit> Handle(
            DeleteCanDbCommand request,
            CancellationToken cancellationToken
        )
        {
            var canDb = await _databaseContext.CanDbs.FindAsync(
                keyValues: new object[] { request.CanDbId },
                cancellationToken: cancellationToken
            );

            if (canDb is null)
            {
                throw new NotFoundException(
                    typeName: nameof(CanDb),
                    id: request.CanDbId
                );
            }

            _databaseContext.CanDbs.Remove(canDb);

            await _databaseContext.SaveChangesAsync(cancellationToken: cancellationToken);

            return Unit.Value;
        }
        #endregion Methods
    }
}
