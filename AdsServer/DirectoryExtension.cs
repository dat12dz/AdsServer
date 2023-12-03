using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsServer
{
    internal static class DirectoryExtension
    {
       public static int CountFilesInFolder( string folderPath)
        {
            try
            {
                // Check if the directory exists
                if (Directory.Exists(folderPath))
                {
                    // Get an array of file names in the specified folder
                    string[] files = Directory.GetFiles(folderPath);

                    // Display each file name (optional)
                    Console.WriteLine("Files in the folder:");
                    foreach (string file in files)
                    {
                        Console.WriteLine(file);
                    }

                    // Return the number of files
                    return files.Length;
                }
                else
                {
                    Console.WriteLine($"Folder not found: {folderPath}");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }
    }
}
