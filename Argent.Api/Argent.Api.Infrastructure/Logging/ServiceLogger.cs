using Argent.Api.Infrastructure.Configuration.Options;
using Argent.Api.Infrastructure.Configuration.Providers;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Argent.Api.Infrastructure.Logging {
    public class ServiceLogger : IServiceLogger {
        private readonly ILoggerConfigurationProvider _environment;
        private readonly ServiceLoggingOption _options;
        private readonly string _fileName;

        // One semaphore per file path — prevents concurrent write corruption
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _fileLocks = new();

        public string Id { get; set; } = "ARGENT";
        public string Channel { get; set; } = "API";

        public ServiceLogger(
            ILoggerConfigurationProvider environment,
            IOptions<ServiceLoggingOption> loggingOptions,
            string logName) {
            _environment = environment;
            _options = loggingOptions.Value;
            _fileName = string.IsNullOrWhiteSpace(logName)
                ? _options.DefaultLogName
                : logName;
        }

        public void Log(string message, string type = "MSG") {
            var date = DateTime.Now;
            var shortDate = date.ToString("yyyy-MM-dd");

            // Cross-platform: root comes from IEnvironmentProvider (configured per environment)
            var basePath = Path.Combine(_environment.RootLogPath, "activity_Logs");
            Directory.CreateDirectory(basePath);

            CleanupOldLogs(basePath);

            var filepath = GetRollingFilePath(basePath, shortDate);

            try {
                // East Africa Standard Time — graceful fallback if TZ not found on this OS
                string tzAbbrev;
                try {
                    var tz = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
                    tzAbbrev = string.Concat(tz.StandardName.Split(' ').Select(w => w[0]));
                }
                catch {
                    tzAbbrev = "EAT";
                }

                var timeIn = $"{date:yyyy.MM.dd HH:mm:ss} {tzAbbrev}";
                var channelPart = !string.IsNullOrEmpty(Channel) ? $"CHANNEL: {Channel}\t" : "";
                var idPart = !string.IsNullOrEmpty(Id) ? $"{Id}\t" : "";

                var line = $"[{timeIn}]: [{type}]\t{channelPart}{idPart}{message}";

                WriteToFile(filepath, line);
            }
            catch (Exception ex) {
                Console.Error.WriteLine($"[ServiceLogger] Failed to write log: {ex.Message}");
            }
        }

        private string GetRollingFilePath(string basePath, string shortDate) {
            var maxSizeBytes = _options.MaxFileSizeInMB * 1024L * 1024L;
            var baseFileName = $"log_{_fileName}_{shortDate}";

            for (int i = 0; i < _options.MaxRollingFiles; i++) {
                var filePath = Path.Combine(basePath, $"{baseFileName}_{i}.txt");

                if (!File.Exists(filePath))
                    return filePath;

                if (new FileInfo(filePath).Length < maxSizeBytes)
                    return filePath;
            }

            // All rolling slots full — wrap to first (oldest)
            return Path.Combine(basePath, $"{baseFileName}_0.txt");
        }

        private static void WriteToFile(string filepath, string message) {
            var fileLock = _fileLocks.GetOrAdd(filepath, _ => new SemaphoreSlim(1, 1));
            fileLock.Wait();
            try {
                File.AppendAllText(filepath, message + Environment.NewLine);
            }
            finally {
                fileLock.Release();
            }
        }

        private void CleanupOldLogs(string basePath) {
            try {
                var cutoff = DateTime.Now.AddDays(-_options.RetentionDays);
                foreach (var file in Directory.GetFiles(basePath, "log_*", SearchOption.TopDirectoryOnly)) {
                    if (new FileInfo(file).CreationTime < cutoff)
                        File.Delete(file);
                }
            }
            catch (Exception ex) {
                Console.Error.WriteLine($"[ServiceLogger] Log cleanup failed: {ex.Message}");
            }
        }
    }

}
