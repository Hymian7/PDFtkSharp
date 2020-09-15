using System;
using System.Diagnostics;
using System.IO;

namespace PDFtkSharp
{
    public class PDFExtractor : PDFManipulator
    {
        #region Properties

        public FileInfo InputDocument { get; set; }

        #endregion

        public PDFExtractor(FileInfo _exePath) : base(_exePath) { }

        public PDFExtractor(string _exePath) : base(new FileInfo(_exePath)) { }

        public PDFExtractor() : base() { }

        public void ExtractRange(string range)
        {
            //runCMD($"-splitByPage '{InputDocument}'");
            CommandLineExecuter.Execute(ExecutablePath ,$"{InputDocument} cat {range} output {OutputPath}\\{OutputFileName}");
        }

        public void ExtractRange(int[] pages)
        {
            CommandLineExecuter.Execute(ExecutablePath, $"{InputDocument} cat {String.Join(" ", pages)} output {OutputPath}\\{OutputFileName}");
        }

    }
}
