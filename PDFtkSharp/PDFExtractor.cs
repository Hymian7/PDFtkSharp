using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using PDFtkSharp.Interfaces.PDFExtractor;

namespace PDFtkSharp
{
    public class PDFExtractor : PDFManipulator, ICanAddInput, ICanAddOutput, ICanAddOutputName, ICanAddPagesOrRun
    {
        #region Properties

        public FileInfo InputFile { get; set; }

        private List<string> _ranges = new List<string>();

        #endregion

        #region Constructors

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

        #endregion

        #region InstanceMethods

        /// <summary>
        /// Extract a single page or a range of pages out of a PDF file. Note: 1-based.
        /// </summary>
        /// <param name="ranges">Ranges can be single pages, e.g. 5, page ranges, e.g. 1-5, or ranges with start or end wildcards, e.g. start-5 7-end</param>
        public Task ExtractAsync(params string[] ranges)
        {
            _ranges = new List<string>(ranges);
            return ExtractAsync();
        }

        /// <summary>
        /// Extracts the given pages into a new PDF file. Note: 1-based
        /// </summary>
        /// <param name="pages">Can be a single page, e.g. 2, multiple pages, e.g. 1 3 5</param>
        public Task ExtractAsync(params int[] pages)
        {
            _ranges = new List<string>();
            foreach(var pagenumber in pages)
            {
                _ranges.Add(pagenumber.ToString());
            }

            return RunCommand();
        }

        private async Task RunCommand()
        {
            try
            {
                // pdftk C:\Folder\input.pdf cat 1 2 3 4 output C:\Folder\output.pdf verbose
                var command = Cli.Wrap(ExecutablePath.FullName)
                    .WithArguments(args => args
                        
                        .Add($"\"{InputFile.FullName}\"", false)
                        .Add("cat")
                        .Add(String.Join(' ', _ranges), false)
                        .Add("output")
                        .Add($"\"{Path.Join(OutputPath.FullName, OutputName)}\"", false)
                        .Add("verbose"))
                    .WithValidation(CommandResultValidation.None);

                Debug.WriteLine($"Running pdftk with arguments: {command.Arguments}");
                    
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

        #endregion

        #region Instantiating Methods
                
            public static ICanAddInput WithExecutable(string pathToExecutable)
            {
                return new PDFExtractor(new FileInfo(pathToExecutable));
            }
            public static ICanAddInput WithExecutable(FileInfo pathToExecutable)
            {
                return new PDFExtractor(pathToExecutable);
            }
            public static ICanAddOutput WithInputFile(string inputFile)
            {
                return new PDFExtractor()
                    {
                        InputFile = new FileInfo(inputFile)
                    };
            }
            public static ICanAddOutput WithInputFile(FileInfo inputFile)
            {
                return new PDFExtractor()
                    {
                        InputFile = inputFile
                    };
            }

        #endregion

        #region Chaining Methods

                public ICanAddOutputName WithOutputPath(DirectoryInfo outputPath)
                {
                    this.OutputPath = outputPath;
                    return this;
                }
        
                public ICanAddOutputName WithOutputPath(string outputPath)
                {
                    this.OutputPath = new DirectoryInfo(outputPath);
                    return this;
                }
        
                public ICanAddPagesOrRun WithOutputFile(FileInfo outputFile)
                {
                    this.OutputFile = outputFile;
                    return this;
                }
        
                public ICanAddPagesOrRun WithOutputFile(string outputFile)
                {
                    this.OutputFile = new FileInfo(outputFile);
                    return this;
                }
        
                public ICanAddPagesOrRun WithOutputName(string outputName)
                {
                    this.OutputName = outputName;
                    return this;
                }
        
                public ICanAddPagesOrRun AddPage(int pageNumber)
                {
                    this._ranges.Add(pageNumber.ToString());
                    return this;
                }
        
                public ICanAddPagesOrRun AddRange(int startPage, int endPage)
                {
                    this._ranges.Add($"{startPage}-{endPage}");
                    return this;
                }
        
                public ICanAddPagesOrRun AddRange(PageMark pageMark, int endPage)
                {
                    string pageMarkString = "";
                    if(pageMark == PageMark.Start) pageMarkString = "start";
                    if(pageMark == PageMark.End) pageMarkString = "end";
                    
                    _ranges.Add($"{pageMarkString}-{endPage}");
                    return this;
                }
        
                public ICanAddPagesOrRun AddRange(int start, PageMark pageMark)
                {
                    string pageMarkString = "";
                    if(pageMark == PageMark.Start) pageMarkString = "start";
                    if(pageMark == PageMark.End) pageMarkString = "end";
                    
                    _ranges.Add($"{start}-{pageMarkString}");
                    return this;
                }

                ICanAddOutput ICanAddInput.WithInputFile(string inputFile)
                {
                    this.InputFile = new FileInfo(inputFile);
                    return this;
                }
                ICanAddOutput ICanAddInput.WithInputFile(FileInfo inputFile)
                {
                    this.InputFile = inputFile;
                    return this;
                }

                public void Extract()
                {
                    Task.Run(() => RunCommand());
                }

                public Task ExtractAsync()
                {
                    return RunCommand();
                }
        #endregion
    }
}
