using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace word_count_app.api.Services
{
    [ExcludeFromCodeCoverage]
    internal static class ValidateJwt
    {
        internal static async Task<bool> TokenValidate(TokenValidatedContext context)
        {
            return await Task.Run(() => true);
        }
    }
}
