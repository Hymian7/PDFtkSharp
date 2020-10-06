using CliWrap;
using CliWrap.Buffered;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtkSharp
{
    public class PDFRotator : PDFManipulator
    {
        

        public enum Rotation
        {
            Right = 1,
            Left,
            Down
        }


        public FileInfo InputDocument { get; set; }

        private bool _overrideFile = true;

        /// <summary>
        /// Defines, of the output file should be the same as the input file. Default is true.
        /// </summary>
        public bool OverrideFile { get => _overrideFile; set => _overrideFile = value; }

        public new DirectoryInfo OutputPath
        {
            get
            {
                if (OverrideFile)
                {
                    return InputDocument.Directory;
                }
                return base.OutputPath;
            }
            set { base.OutputPath = value; }
        }

        public new String OutputName
        {
            get
            {
                if (OverrideFile)
                {
                    return InputDocument.Name;
                }
                return base.OutputName;
            }
            set { base.OutputName = value; }
        }

        const String appendix = "_rot";

        /// <summary>
        /// Use this constructor to provide an alternative .exe file
        /// </summary>
        /// <param name="_exePath">Full Path to PDFtk Server EXE</param>
        public PDFRotator(FileInfo _exePath) : base(_exePath) { }

        /// <summary>
        /// Use this constructor to provide an alternative .exe file
        /// </summary>
        /// <param name="_exePath">Full Path to PDFtk Server EXE</param>
        public PDFRotator(string _exePath) : base(new FileInfo(_exePath)) { }

        /// <summary>
        /// Class for rotation of single or multiple pages of PDF file
        /// </summary>
        public PDFRotator() : base() { }

        public async Task RotatePagesAsync(IEnumerable<int> pages, Rotation rotation)
        {
            String rotationStatements = BuildRotationOptions(pages, rotation);

            try
            {
                if (OverrideFile)
                {
                await Cli.Wrap(ExecutablePath.FullName).WithArguments($"\"{InputDocument.FullName}\" rotate {rotationStatements}output \"{OutputPath}\\{OutputName}{appendix}").ExecuteBufferedAsync();
                CleanUp();
                }
                else
                {
                await Cli.Wrap(ExecutablePath.FullName).WithArguments($"\"{InputDocument.FullName}\" rotate {rotationStatements}output \"{OutputPath}\\{OutputName}").ExecuteBufferedAsync();
                }
                
            }

            catch (Exception ex)
            {
                throw new PdfManipulatingException(ex.Message);
            }


        }

        private String BuildRotationOptions(IEnumerable<int> pages, Rotation rotation)
        {
            StringBuilder rotationStatements = new StringBuilder();

            String rotationKeyWord;

            // Translate the Rotation Enum to PDFtk Arguments
            switch (rotation)
            {
                case Rotation.Right:
                    rotationKeyWord = "right";
                    break;
                case Rotation.Left:
                    rotationKeyWord = "left";
                    break;
                case Rotation.Down:
                    rotationKeyWord = "down";
                    break;
                default:
                    rotationKeyWord = "north";
                    break;
            }

            foreach (var page in pages)
            {
                rotationStatements.Append(page);
                rotationStatements.Append(rotationKeyWord);
                rotationStatements.Append(' ');
            }

            return rotationStatements.ToString();
        }

        /// <summary>
        /// Cleanup for deleting the original file and renaming the new rotated file to the name of the original file
        /// (Workaround for overriding original file)
        /// </summary>
        private void CleanUp()
        {
            try
            {
                File.Move(InputDocument.FullName, InputDocument.FullName + ".old");
                File.Move(InputDocument.FullName + appendix, InputDocument.FullName);
                File.Delete(InputDocument.FullName + ".old");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                if (File.Exists(InputDocument.FullName + ".old"))
                {
                    File.Move(InputDocument.FullName + ".old", InputDocument.FullName, true);
                }
                if (File.Exists(InputDocument.FullName+appendix))
                {
                    File.Delete(InputDocument.FullName + appendix);
                }
            }

        }
    }
}
