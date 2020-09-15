using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PDFtkSharp
{
    public abstract class PDFManipulator
    {
        protected FileInfo ExecutablePath { get; set; }
        public DirectoryInfo OutputPath { get; set; }
        public string OutputFileName { get; set; }

        public PDFManipulator()
        {
            ExecutablePath = new FileInfo(".\\assets\\pdftk.exe");
        }

        public PDFManipulator(FileInfo _exePath)
        {
            ExecutablePath = _exePath;
        }

    }
}
