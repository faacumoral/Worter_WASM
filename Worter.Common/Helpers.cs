using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using Worter.Common.Constants;

namespace Worter.Common
{
    public static class Helpers
    {
        public static Environment GetEnvironment()
        {
            var value = System.Environment.GetEnvironmentVariable(CONSTANTS.Keys.ENVIRONMENT);

            // default value
            if (string.IsNullOrEmpty(value)) return CONSTANTS.Keys.DEFAULT_ENVIRONMENT;

            value = value.ToUpper();

            switch (value)
            {
                case CONSTANTS.Keys.DEVELOPMENT:
                    return Environment.DEVELOPMENT;
                case CONSTANTS.Keys.PRODUCTION:
                    return Environment.PRODUCTION;
            }

            throw new ArgumentException($"'{value}' environment is not a valid one. Check Worter.Common.Environment enum.");
        }
    }
}
