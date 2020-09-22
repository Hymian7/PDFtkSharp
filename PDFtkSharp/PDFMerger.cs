using System;
using System.Collections.Generic;
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

        public async Task MergeAsync()
        {
            string args = $"{String.Join(" ", InputFiles)} cat output {OutputPath}\\{OutputName}";


            //CommandLineExecuter.Execute(ExecutablePath, args);

            try
            {
                await Cli.Wrap(ExecutablePath.FullName).WithArguments(args).ExecuteBufferedAsync();
            }
            catch (Exception ex)
            {

                throw new PdfManipulatingException(ex.Message, ex);
            }
           
            
        }

    }
}
