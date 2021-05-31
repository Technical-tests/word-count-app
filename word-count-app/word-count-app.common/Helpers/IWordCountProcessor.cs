
using System.Collections.Generic;
using System.Threading.Tasks;

using word_count_app.common.Models;

namespace word_count_app.common.Helpers
{
    public interface IWordCountProcessor
    {
        Task<Dictionary<string, ushort>> GetAmountAsync(RequestWords request);
    }
}
