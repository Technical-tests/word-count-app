using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics.CodeAnalysis;

using word_count_app.common.Helpers;

namespace word_count_app.api.App_Start
{
    [ExcludeFromCodeCoverage]
    internal static class DependencyInjectionConfiguration
    {
        internal static void Register(IServiceCollection services)
        {
            _ = services.AddScoped<IWordCountProcessor, WordCountProcessor>();
        }
    }
}
