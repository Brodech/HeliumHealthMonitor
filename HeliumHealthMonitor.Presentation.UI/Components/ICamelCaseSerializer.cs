namespace HeliumHealthMonitor.Presentation.UI.Components
{
    public interface ISerializer
    {
        string ContentType { get; set; }

        string Serialize(object obj);
    }
}