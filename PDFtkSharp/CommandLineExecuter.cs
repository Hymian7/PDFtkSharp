using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PDFtkSharp
{
    static class CommandLineExecuter
    {

        public static void Execute(FileInfo file, string args)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = file.FullName,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    //Muss wieder auf True gesetzt werden
                    CreateNoWindow = false,
                    //TODO: Check
                    WorkingDirectory = @"D:\Desktop\Debug"
                }
            };

            proc.Start();
        }

    }
}
