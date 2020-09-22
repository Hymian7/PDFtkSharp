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

        private string _outputName;
        public string OutputName
        {
            get { return _outputName; }
            set { if (value.Substring(value.Length -4)== ".pdf") _outputName = value; else _outputName = value + ".pdf"; }
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
