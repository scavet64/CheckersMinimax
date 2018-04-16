using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax
{
    public static class FileNameHelper
    {
        /// <summary>
        /// Gets the executing directory.
        /// </summary>
        /// <returns>The executing directory path</returns>
        public static string GetExecutingDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return new FileInfo(location).Directory.FullName + @"\";
        }
    }
}
