using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace PDFtkSharp
{
    public class PDFMerger : PDFManipulator
    {
        #region Properties

        public List<FileInfo> InputFiles { get; set; }

        #endregion

        public PDFMerger() : base()
        {
            InputFiles = new List<FileInfo>();
        }

        /// <summary>
        /// Merges two or more PDF files into a single output file.
        /// </summary>
        /// <returns></returns>
        public async Task MergeAsync()
        {
            if(InputFiles.Count < 2) throw new ArgumentOutOfRangeException("The list with input files must at least contain 2 files.");

            try
            {
                StringBuilder inputFilesCombined = new StringBuilder();

                foreach(var file in InputFiles)
                {
                    inputFilesCombined.Append("\"");
                    inputFilesCombined.Append(file.FullName);
                    inputFilesCombined.Append("\" ");
                }

                // pdftk C:\Folder\input1.pdf C:\Folder\input1.pdf cat output C:\Folder\output.pdf verbose
                var command = Cli.Wrap(ExecutablePath.FullName)
                    .WithArguments(args => args
                        .Add($"{inputFilesCombined.ToString().TrimEnd()}", false)
                        .Add("cat")
                        .Add("output")
                        .Add($"\"{Path.Combine(OutputPath.FullName, OutputName)}\"", false)
                        .Add("verbose"))
                    .WithValidation(CommandResultValidation.None);

                Debug.WriteLine(command.Arguments);
                
                var result = await command.ExecuteBufferedAsync();

               if(result.ExitCode != 0)
               {
                   Debug.WriteLine(result.StandardOutput);
                   Debug.WriteLine(result.StandardError);
                   throw new PdfManipulatingException(result.StandardError);
               }
               
               else
               {
                    Debug.WriteLine(result.StandardOutput);
                    Debug.WriteLine("Execution finished at " + result.ExitTime + " after running for " + result.RunTime);
               }
               

            }
            catch (Exception ex)
            {
                throw new PdfManipulatingException("Error while wrapping the command with CliWrap:" + ex.Message, ex);
            }            
           
            
        }

    }
}
