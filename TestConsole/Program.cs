using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PDFtkSharp;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PDFExtractor ext = new PDFExtractor()
            {
                InputFile = new System.IO.FileInfo("D:\\Downloads\\The-Sustainable-Development-Goals-Report-2021.pdf"),
                OutputPath = new System.IO.DirectoryInfo("D:\\Desktop"),
                OutputName = "00000002.pdf"
            };

            int[] pages = new int[68];

            for (int i = 0; i < 68; i++)
            {
                pages[i] = i+1;
            }

            await ext.ExtractAsync(pages);

        }
    }
}
