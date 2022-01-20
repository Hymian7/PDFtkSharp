using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

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

        public FileInfo OutputFile
        {
            get { return new FileInfo(Path.Combine(OutputPath.FullName, OutputName)); }
            set { 
                    OutputPath = value.Directory;
                    OutputName = value.Name;
                }
        }



        /// <summary>
        /// Use this constructor to use the shipped PDFtk EXE file
        /// </summary>
        public PDFManipulator() 
        {
            // Set executable path based on OS

            // Windows: Check subdirectory in case the binaries are provided
            // Also check Default pdftk installation directory
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (System.IO.File.Exists(".\\assets\\pdftk.exe"))
                {
                    ExecutablePath = new FileInfo(".\\assets\\pdftk.exe");
                }
                else if (System.IO.File.Exists(Environment.ExpandEnvironmentVariables(@"%programfiles(x86)%\PDFtk\bin\pdftk.exe")))
                {
                    ExecutablePath = new FileInfo(Environment.ExpandEnvironmentVariables(@"%programfiles(x86)%\PDFtk\bin\pdftk.exe"));
                }
                else
                {
                    throw new FileNotFoundException("pdftk executable could not be located");
                }
            }
            // Linux: pdftk is normally installed to /bin/pdftk
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                if(File.Exists("/bin/pdftk"))
                {
                     ExecutablePath = new FileInfo("/bin/pdftk");
                }
                else
                {
                    throw new FileNotFoundException("pdftk executable could not be located");
                }
            }
            else
            {
                throw new Exception("OS not supported");
            }
            System.Diagnostics.Debug.WriteLine("PDF Manipulator Executable Path set to: " + ExecutablePath.FullName);

        }

        /// <summary>
        /// Use this constructor to provide an alternative path for the pdftk executable
        /// </summary>
        /// <param name="_exePath">Full Path to PDFtk executable</param>
        public PDFManipulator(FileInfo _exePath)
        {
            //Check if executable path exists. Otherwise throw exception
            if(!_exePath.Exists)
            {
                throw new FileNotFoundException($"The Pdftk executable could not be found under {_exePath}");
            }

            ExecutablePath = _exePath;

            System.Diagnostics.Debug.WriteLine("PDF Manipulator Executable Path set to: " + ExecutablePath.FullName);
        }

    }
}
