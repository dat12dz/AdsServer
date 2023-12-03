
namespace AdsServer
{
    internal static class PrintException
    {
        public static void PrintExceptionInfo(this Exception ex)
        {


            Console.WriteLine("Exception Information:");
            Console.WriteLine($"Type: {ex.GetType().FullName}");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"Source: {ex.Source}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");

            // Check if the exception has an inner exception
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception:");
                Console.WriteLine($"Type: {ex.InnerException.GetType().FullName}");
                Console.WriteLine($"Message: {ex.InnerException.Message}");
                Console.WriteLine($"Source: {ex.InnerException.Source}");
                Console.WriteLine($"StackTrace: {ex.InnerException.StackTrace}");
            }
        }
    }
}
