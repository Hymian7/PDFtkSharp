using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public bool Merge()
        {
            string args = $"{String.Join(" ", InputFiles)} cat output {OutputPath}\\{OutputFileName}";            

            CommandLineExecuter.Execute(ExecutablePath, args);

            return true;
        }

    }
}
