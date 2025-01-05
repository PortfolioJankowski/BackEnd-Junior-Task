namespace Task.Services.Static
{
    public static class CommandProvider
    {
        public enum Command
        {
            Count,
            MaxAge
        }

        private static readonly Dictionary<Command, string> CommandMappings = new()
        {
            { Command.Count, "count" },
            { Command.MaxAge, "max-age" }
         };

        public static IEnumerable<string> GetCommands()
        {
            return CommandMappings.Values;
        }

        public static string GetCommand(Command command)
        {
            return CommandMappings[command];
        }
    }
}
