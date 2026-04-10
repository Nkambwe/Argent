namespace Argent.Api.Infrastructure.Logging {
    public interface IServiceLogger {
        string Id { set; get; }
        string Channel { set; get; }
        void Log(string message, string type = "MSG");
    }
}
