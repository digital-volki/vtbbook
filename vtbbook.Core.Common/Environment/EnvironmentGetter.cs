using System.Collections.Generic;

namespace vtbbook.Core.Common.Environment
{
    public static class EnvironmentGetter
    {
        private const string DefaultEnvironmentName = "ASPNETCORE_ENVIRONMENT";

        private static readonly Dictionary<Environment, string> EnvValue = new()
        {
            { Environment.Development, "dev" }
        };

        public static string Get(Environment defaultValue = Environment.Development)
        {
            return System.Environment.GetEnvironmentVariable(DefaultEnvironmentName) ?? EnvValue[defaultValue];
        }
    }
}
