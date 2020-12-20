using System.Threading.Tasks;
using Xunit;
using CanDatabase.WebApi;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using CanDatabase.WebApiIntegrationTests.Infrastructure;
using System.Net.Http.Json;
using CanDatabase.Shared.DataTransferObjects;
using CanDatabase.WebApiIntegrationTests.Comparers;
using System.Collections.Generic;

namespace CanDatabase.WebApiIntegrationTests.ControllersTests
{
    /// <summary>
    /// CanDbControllerTest
    /// </summary>
    public class CanDbControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        #region Fields
        private readonly CustomWebApplicationFactory<Startup> _factory;
        #endregion Fields

        #region Controllers
        public CanDbControllerTest(
            CustomWebApplicationFactory<Startup> factory
        )
        {
            _factory = factory;
        }
        #endregion Controllers

        #region ParseDbc
        [Fact]
        public async Task ParseDbc_ParsesContentsOfDbcFileCorrectly_WhenDbcIsInCorrectFormat()
        {
            #region Arrange
            var fileName = "nodes.dbc";
            var client = _factory.CreateClient();
            var dbcFile = File.OpenRead($"TestData/{fileName}");
            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            content.Add(
                content: new StreamContent(
                    content: dbcFile,
                    bufferSize: Convert.ToInt32(dbcFile.Length)
                ),
                name: "file",
                fileName: fileName
            );
            var canDbId = 1;
            var expectedGetCanDbResponse = new GetCanDbResponse(
                canDb: new CanDbDetails(
                    id: 1,
                    originalFileName: fileName,
                    createTimeStamp: DateTimeOffset.UtcNow,
                    messages: new List<MessageListItem>
                    {
                        new MessageListItem(
                            id: 1,
                            canId: 257,
                            name: "SendStoreData",
                            signals: new List<SignalListItem>
                            {
                                new SignalListItem(
                                    id: 1,
                                    name: "SawTooth",
                                    startBit: 32,
                                    length: 32
                                ),
                                new SignalListItem(
                                    id: 2,
                                    name: "Sinus",
                                    startBit: 0,
                                    length: 32
                                ),
                            }
                        )
                    },
                    networkNodes: new List<NetworkNodeListItem>
                    {
                        new NetworkNodeListItem(
                            id: 1,
                            name: "ECUVariantIdent"
                        ),
                        new NetworkNodeListItem(
                            id: 2,
                            name: "GW"
                        ),
                        new NetworkNodeListItem(
                            id: 3,
                            name: "Tester"
                        ),
                        new NetworkNodeListItem(
                            id: 4,
                            name: "ECU"
                        ),
                    }
                )
            );
            #endregion Arrange

            #region Act
            var parseDbcResponse = await client.PostAsync(
                    requestUri: "api/can-db",
                    content: content
                );

            var actualGetCanDbResponse = await client.GetFromJsonAsync<GetCanDbResponse>(
                requestUri: $"api/can-db/{canDbId}"
            );
            #endregion Act

            #region Assert
            parseDbcResponse.EnsureSuccessStatusCode();
            Assert.Equal(
                expected: expectedGetCanDbResponse,
                actual: actualGetCanDbResponse,
                comparer: new GetCanDbResponseComparer()
            );
            #endregion Assert
        }
        #endregion ParseDbc
    }
}
