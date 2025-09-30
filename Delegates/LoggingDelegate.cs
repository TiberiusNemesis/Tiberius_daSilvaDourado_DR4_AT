namespace TravelAgency.Delegates
{
    public class Logger
    {
        private static List<string> memoryLogs = new List<string>();

        public static void LogToConsole(string message)
        {
            Console.WriteLine($"[CONSOLE] {DateTime.Now}: {message}");
        }

        public static void LogToFile(string message)
        {
            var logMessage = $"[FILE] {DateTime.Now}: {message}";
            var logPath = Path.Combine("wwwroot", "logs", "application.log");

            Directory.CreateDirectory(Path.GetDirectoryName(logPath));
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }

        public static void LogToMemory(string message)
        {
            var logMessage = $"[MEMORY] {DateTime.Now}: {message}";
            memoryLogs.Add(logMessage);
        }

        public static List<string> GetMemoryLogs()
        {
            return new List<string>(memoryLogs);
        }
    }
}