using System.IO;
using System.Threading.Tasks;
using CanDatabase.Domain.Models;

namespace CanDatabase.Domain.IServices
{
    /// <summary>
    /// IParseDbcFileService
    /// </summary>
    public interface IParseDbcFileService
    {
        public Task<CanDb> ParseDbcFile(Stream dbcFileStream);
    }
}
