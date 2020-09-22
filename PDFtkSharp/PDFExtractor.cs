using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace PDFtkSharp
{
    public class PDFExtractor : PDFManipulator
    {
        #region Properties

        public FileInfo InputDocument { get; set; }

        #endregion

        /// <summary>
        /// Use this constructor to provide an alternative .exe file
        /// </summary>
        /// <param name="_exePath">Full Path to PDFtk Server EXE</param>
        public PDFExtractor(FileInfo _exePath) : base(_exePath) { }

        /// <summary>
        /// Use this constructor to provide an alternative .exe file
        /// </summary>
        /// <param name="_exePath">Full Path to PDFtk Server EXE</param>
        public PDFExtractor(string _exePath) : base(new FileInfo(_exePath)) { }

        /// <summary>
        /// Class for extracting pages out of a PDF-File
        /// </summary>
        public PDFExtractor() : base() { }

        /// <summary>
        /// Extract a single page or a range of pages out of a PDF file. Note: 1-based.
        /// </summary>
        /// <param name="range">Range can be one range, e.g. 1-5, or multiple ranges with start or end wildcards, e.g. start-5 7-end</param>
        public async Task ExtractRangeAsync(string range)
        {

            //CommandLineExecuter.Execute(ExecutablePath ,$"{InputDocument} cat {range} output {OutputPath}\\{OutputName}");
            try
            { 
            var result = await Cli.Wrap(ExecutablePath.FullName).WithArguments($"{InputDocument} cat {range} output {OutputPath}\\{OutputName}").ExecuteBufferedAsync();
            }

            catch(Exception ex)
            {
                throw new PdfManipulatingException(ex.Message);
            }


        }

        /// <summary>
        /// Extracts the given pages into a new PDF file. Note: 1-based
        /// </summary>
        /// <param name="pages">Can be a single page, e.g. 2, multiple pages, e.g. 1 3 5</param>
        public async Task ExtractPagesAsync(params int[] pages)
        {
            //CommandLineExecuter.Execute(ExecutablePath, $"{InputDocument} cat {String.Join(" ", pages)} output {OutputPath}\\{OutputName}");

            try
            {
               await Cli.Wrap(ExecutablePath.FullName).WithArguments($"{InputDocument} cat {String.Join(" ", pages)} output {OutputPath}\\{OutputName}").ExecuteBufferedAsync();
            }
            catch (Exception ex)
            {

                throw new PdfManipulatingException(ex.Message);
            }            

        }

    }
}
