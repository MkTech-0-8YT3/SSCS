using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonTests
{
    public static class CommonMethods
    {
        private static readonly string RESOURCES_DIRECTORY = "resources";
        public static string GetFilePathFromTestResources(string file)
        {
            string workingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
            return workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + file;
        }
    }
}
