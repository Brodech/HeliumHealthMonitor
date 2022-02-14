namespace HeliumHealthMonitor.Data.Bootstrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1 || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Please provide the connection string as the only argument. Press enter to quit");
                Console.ReadKey();
                return;
            }
            var connectionString = args[0];
            Console.WriteLine($"Initializing Database!  {connectionString}");
            try
            {
                var dataAccess = new DataAccess(connectionString);
                await dataAccess.DropAll();
                await dataAccess.Initialize();
                Console.WriteLine($"Database Initialized!  {connectionString}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}