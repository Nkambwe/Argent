
namespace Argent.Api.Infrastructure.Configuration.Options {
    public class ServiceLoggingOption {
        /// <summary>
        /// AppSettings configuration section
        /// </summary>
        public const string SectionName = "Logging:ServiceLogger";
        /// <summary>
        /// get or set default log file name
        /// </summary>
        public string DefaultLogName { get; set; } = "apilog";
        /// <summary>
        /// Get or Set log folder
        /// </summary>
        public string LogFolder { get; set; } = "Mfilogs";

        /// <summary>
        /// Get or Set rolling log file mximum size. 
        /// </summary>
        /// <remarks>
        /// Default is set to 10 MB
        /// </remarks>
        public long MaxFileSizeInMB { get; set; } = 10;
        /// <summary>
        /// Get or Set maximum rolling file. 
        /// </summary>
        /// <remarks>
        /// Default is set to keep up to 5 files per day
        /// </remarks>
        public int MaxRollingFiles { get; set; } = 5;
        /// <summary>
        /// Get or Set number of days to delete logs older than say 30 days
        /// </summary>
        public int RetentionDays { get; set; } = 30;
    }
}
