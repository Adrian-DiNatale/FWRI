using FWRI.KeyCardReport.Library.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FWRI.KeyCardReport.Library.Utilities
{
    /// <summary>
    /// Service to inject that can read a value from the AppSettings file.
    /// </summary>
    /// <seealso cref="FWRI.KeyCardReport.Library.Interfaces.IAppSettings" />
    public class ConfigurationUtility : IAppSettings
    {
        private static IConfiguration _configuration = null!;

        public ConfigurationUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Helpers        
        public static int DefaultPageSize
        {
            get
            {
                return GetIntFromAppSettings("AppSettings:DefaultPageSize") ?? 5;
            }
        }
        #endregion

        #region Config Accessors
        private static int? GetIntFromAppSettings(string key)
        {
            string? value = _configuration[key];
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            return null;
        }
        #endregion

    }
}
