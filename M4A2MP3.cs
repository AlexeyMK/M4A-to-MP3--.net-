using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;

namespace fjEnterprises
{
    public static class M4A2MP3
    {
        /// <summary>
        /// converts an M4A file to an MP3 file.  Note:
        /// make sure that lame.exe and faad.exe are in the same directory
        /// as this dll.
        /// Inspired by (and using code from)
        /// http://yakkowarner.blogspot.com/2008/06/my-m4a2mp3-script.html
        /// </summary>
        /// <param name="fromPath">The m4a file (use full path)</param>
        /// <param name="toPath">the mp3 file (use full path)</param>
        public static void Convert(string fromPath, string toPath)
        {
            int lastIndex = fromPath.Contains("\\") ? fromPath.LastIndexOf('\\') : 0;
            string tempFileName = Environment.CurrentDirectory + "\\temp\\" + fromPath.Substring(lastIndex);
            
            if(File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
            if(!Directory.GetParent(tempFileName).Exists)
            {
                Directory.GetParent(tempFileName).Create();
            }

            Interaction.Shell(String.Format("faad.exe -o {0} {1}",
                    InQuotes(tempFileName),
                    InQuotes(fromPath)), 
                AppWinStyle.Hide, true, -1);
            Interaction.Shell(String.Format("lame.exe --preset standard {0} {1}",
                    InQuotes(tempFileName),
                    InQuotes(toPath)),
                AppWinStyle.Hide, true, -1);

            File.Delete(tempFileName);
        }

        private static string InQuotes(string withoutQuotes)
        {
 	        return "\"" + withoutQuotes + "\"";
        }
    }
}
