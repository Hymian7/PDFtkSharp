using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PDFtkSharp
{
    static class CommandLineExecuter
    {

        public static bool Execute(FileInfo file, string args)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = file.FullName,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    //Muss wieder auf True gesetzt werden
                    CreateNoWindow = false,
                    //TODO: Check
                    WorkingDirectory = @"D:\Desktop\Debug"
                    };
            

                proc.Start();

                var errorreader = proc.StandardError;
                var error = errorreader.ReadToEnd();

                proc.WaitForExit();

                if (proc.ExitCode > 0) throw new Exception("Fehler beim Manipulieren der PDF-Datei\n" + error);

                return true;

            }
        }

    }
}
