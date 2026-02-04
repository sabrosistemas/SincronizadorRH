using System.Diagnostics;

namespace SabroRH.Sincronizador
{
    public class EventNotifier
    {
        public delegate void StatusNotification(int percentualGlobal, string msgGlobal, int percentualTask, string msgTask, int maxGlobal, int maxTask);
        public static StatusNotification Status;

        public static void NotifyStatus(int percentualGlobal, string msgGlobal, int percentualTask, string msgTask, int maxGlobal, int maxTask)
        {
            Debug.WriteLine($"Global: {percentualGlobal}% - {msgGlobal}");
            Debug.WriteLine($"Task: {percentualTask}% - {msgTask}");
            Status?.Invoke(percentualGlobal, msgGlobal, percentualTask, msgTask, maxGlobal, maxTask);
        }
    }
}
