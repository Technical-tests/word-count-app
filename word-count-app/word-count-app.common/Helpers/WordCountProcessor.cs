using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using word_count_app.common.Models;

namespace word_count_app.common.Helpers
{
    public class WordCountProcessor : IWordCountProcessor
    {
        public Task<Dictionary<string, ushort>> GetAmountAsync(RequestWords request)
        {
            Dictionary<string, ushort> response = new Dictionary<string, ushort>();
            string[] listWords = request.OriginalText.Replace(".", string.Empty).Replace(",", string.Empty).Split(' ');

            if (string.IsNullOrWhiteSpace(request.Filter))
            {
                foreach (string word in listWords)
                {
                    if (response.ContainsKey(word))
                    {
                        response[word] = ++response[word];
                    }
                    else
                    {
                        response.Add(word, 1);
                    }
                }
            }
            else
            {
                response.Add(request.Filter, 0);
                foreach (string word in listWords)
                {
                    if (string.Equals(word, request.Filter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        response[request.Filter] = ++response[request.Filter];
                    }
                }
            }

            return Task.FromResult(response);
        }
    }
}
