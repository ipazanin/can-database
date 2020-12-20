using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanDatabase.Application.CanDbs.Queries.GetCanDbs;
using CanDatabase.Domain.Models;
using CanDatabase.Persistence.Database;
using Moq;
using Xunit;
using MockQueryable.Moq;

namespace CanDatabase.ApplicationTests.CanDbsTests.QueriesTests.GetCanDbsTests
{
    /// <summary>
    /// GetCanDbsHandlerTest
    /// </summary>
    public class GetCanDbsHandlerTest
    {
        [Fact]
        public async Task Handle_ReturnsGetCanDbsResponseWithOneCanDb_WhenThereisOneCanDbInDatabaseAndTakeIsGreaterThanZeroAndSkipIsZero()
        {
            #region Arrange
            var canDbs = new List<CanDb>
            {
                new CanDb(
                    id: 1,
                    originalFileName: "nodes.dbc",
                    createTimeStamp: DateTimeOffset.UtcNow,
                    messages: new List<Message>(),
                    networkNodes: new List<NetworkNode>()
                )
            };

            var canDbsDbSetMock = canDbs.AsQueryable().BuildMockDbSet();

            var databaseContextMock = new Mock<ICanDatabaseContext>(MockBehavior.Strict);
            databaseContextMock.SetupGet(databaseContext => databaseContext.CanDbs)
                .Returns(canDbsDbSetMock.Object);

            var handler = new GetCanDbsHandler(
                databaseContext: databaseContextMock.Object
            );

            var expectedNumberOfCanDbs = 1;
            #endregion Arrange

            #region Act
            var response = await handler.Handle(
                request: new GetCanDbsQuery(
                    take: 20,
                    skip: 0
                ),
                cancellationToken: default
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedNumberOfCanDbs,
                actual: response.CanDbs.Count()
            );
            #endregion Assert
        }

        [Fact]
        public async Task Handle_ReturnsGetCanDbsResponseWithEmptyCanDbs_WhenThereIsNoneCanDbInDatabase()
        {
            #region Arrange
            var canDbs = new List<CanDb>();

            var canDbsDbSetMock = canDbs.AsQueryable().BuildMockDbSet();

            var databaseContextMock = new Mock<ICanDatabaseContext>(MockBehavior.Strict);
            databaseContextMock.SetupGet(databaseContext => databaseContext.CanDbs)
                .Returns(canDbsDbSetMock.Object);

            var handler = new GetCanDbsHandler(
                databaseContext: databaseContextMock.Object
            );

            var expectedNumberOfCanDbs = 0;
            #endregion Arrange

            #region Act
            var response = await handler.Handle(
                request: new GetCanDbsQuery(
                    take: 20,
                    skip: 0
                ),
                cancellationToken: default
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedNumberOfCanDbs,
                actual: response.CanDbs.Count()
            );
            #endregion Assert
        }
    }
}
