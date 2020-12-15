using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CanDatabase.Domain.IServices;
using CanDatabase.Domain.Models;

namespace CanDatabase.Domain.Services
{
    /// <summary>
    /// ParseDbcFileService
    /// </summary>
    public class ParseDbcFileService : IParseDbcFileService
    {
        #region Constants
        private const char DbcDelimiter = ' ';

        private const int KeywordIndex = 0;

        private const int MinimumNumberOfNetworkNodeProperties = 2;
        private const int MinimumNumberOfMessageProperties = 5;
        private const int MinimumNumberOfSignalProperties = 8;

        private const int MinimumNumberOfSignalStartBitAndLengthSplittedItems = 2;

        private const int MessageCanIdPropertyIndex = 1;
        private const int MessageNamePropertyIndex = 2;

        private const int SignalNamePropertyIndex = 1;
        private const int SignalStartBitAndLengthPropertyIndex = 3;
        private const int SignalStartBitSplittedIntegersIndex = 0;
        private const int SignalLengthSplittedIntegersIndex = 1;

        private const string IntegersRegexPattern = "[0-9]+";
        #endregion Constants

        #region Fields
        #endregion Fields

        #region Constructors
        /// <summary>
        /// ParseDbcFileService Constructor
        /// </summary>
        public ParseDbcFileService()
        {
        }
        #endregion Constructors

        #region Methods
        public async Task<CanDb> ParseDbcFile(Stream dbcFileStream)
        {
            var messages = new List<Message>();
            var networkNodes = new List<NetworkNode>();

            using var streamReader = new StreamReader(
                stream: dbcFileStream
            );

            await ParseLines(
                streamReader: streamReader,
                messages: messages,
                networkNodes: networkNodes
            );

            var canDb = new CanDb(
                id: default,
                messages: messages,
                networkNodes: networkNodes
            );

            return canDb;
        }

        private static async Task ParseLines(
            StreamReader streamReader,
            List<Message> messages,
            List<NetworkNode> networkNodes
        )
        {
            string? line;
            Message? currentMessage = null;

            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                var properties = line
                    .Trim()
                    .Split(
                        separator: DbcDelimiter,
                        options: StringSplitOptions.RemoveEmptyEntries
                    );

                if (!properties.Any())
                {
                    continue;
                }

                var keyword = properties.ElementAt(KeywordIndex);
                switch (keyword)
                {
                    case "BU_":
                        var currentNetworkNodes = ParseNetworkNodesFromProperties(properties: properties);
                        networkNodes.AddRange(currentNetworkNodes);
                        currentMessage = null;
                        break;
                    case "BO_":
                        var message = ParseMessageFromProperties(properties: properties);
                        if (message is not null)
                        {
                            messages.Add(message);
                        }
                        currentMessage = message;
                        break;
                    case "SG_":
                        if (currentMessage is not null)
                        {
                            var signal = ParseSignalFromProperties(properties: properties);
                            if (signal is not null)
                            {
                                currentMessage.AddSignal(signal: signal);
                            }
                        }
                        break;
                    default:
                        currentMessage = null;
                        break;
                }
            }
        }

        private static IEnumerable<NetworkNode> ParseNetworkNodesFromProperties(IEnumerable<string> properties)
        {
            if (properties.Count() < MinimumNumberOfNetworkNodeProperties)
            {
                return Array.Empty<NetworkNode>();
            }

            var networkNodes = properties
                .Skip(1)
                .Select(networkNodeName => new NetworkNode(
                    id: default,
                    name: networkNodeName,
                    canDbId: default,
                    canDb: default
                ));

            return networkNodes;
        }

        private static Message? ParseMessageFromProperties(IEnumerable<string> properties)
        {
            if (properties.Count() < MinimumNumberOfMessageProperties)
            {
                return null;
            }

            var canIdString = properties.ElementAt(MessageCanIdPropertyIndex);

            if (!int.TryParse(
                s: canIdString,
                style: NumberStyles.Integer,
                provider: NumberFormatInfo.InvariantInfo,
                result: out var canId
            ))
            {
                return null;
            }

            var name = properties.ElementAt(MessageNamePropertyIndex);

            var message = new Message(
                id: default,
                canId: canId,
                name: name,
                canDbId: default,
                canDb: default,
                signals: new List<Signal>()
            );

            return message;
        }

        private static Signal? ParseSignalFromProperties(IEnumerable<string> properties)
        {
            if (properties.Count() < MinimumNumberOfSignalProperties)
            {
                return null;
            }

            var startBitAndLength = properties.ElementAt(SignalStartBitAndLengthPropertyIndex);

            var splittedStartBitAndLengthItems = Regex
                .Matches(
                    input: startBitAndLength,
                    pattern: IntegersRegexPattern
                )
                .Select(match => match.Value);

            if (splittedStartBitAndLengthItems.Count() < MinimumNumberOfSignalStartBitAndLengthSplittedItems)
            {
                return null;
            }

            var startBitString = splittedStartBitAndLengthItems
                .ElementAt(SignalStartBitSplittedIntegersIndex);

            var lengthString = splittedStartBitAndLengthItems
                .ElementAt(SignalLengthSplittedIntegersIndex);

            if (!int.TryParse(
                s: startBitString,
                style: NumberStyles.Integer,
                provider: NumberFormatInfo.InvariantInfo,
                result: out var startBit
            ))
            {
                return null;
            }

            if (!int.TryParse(
                s: lengthString,
                style: NumberStyles.Integer,
                provider: NumberFormatInfo.InvariantInfo,
                result: out var length
            ))
            {
                return null;
            }

            var name = properties.ElementAt(SignalNamePropertyIndex);

            var signal = new Signal(
                id: default,
                name: name,
                startBit: startBit,
                length: length,
                messageId: default,
                message: default
            );

            return signal;
        }
        #endregion Methods
    }
}
