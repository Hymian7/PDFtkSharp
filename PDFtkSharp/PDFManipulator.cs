using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PDFtkSharp
{
    /// <summary>
    /// Base Class for all PDF Manipulators. Provices basic Properties.
    /// </summary>
    public abstract class PDFManipulator
    {
        protected FileInfo ExecutablePath { get; set; }
        public DirectoryInfo OutputPath { get; set; }
        public string OutputName
        {
            get { return OutputName; }
            set { if (value.EndsWith(".pdf")) OutputName = value; else OutputName = value + ".pdf"; }
        }



        /// <summary>
        /// Use this constructor to use the shipped PDFtk EXE file
        /// </summary>
        public PDFManipulator()
        {
            ExecutablePath = new FileInfo(".\\assets\\pdftk.exe");
        }

        /// <summary>
        /// Use this constructor to provide an alternative .exe file
        /// </summary>
        /// <param name="_exePath">Full Path to PDFtk Server EXE</param>
        public PDFManipulator(FileInfo _exePath)
        {
            ExecutablePath = _exePath;
        }

    }
}
