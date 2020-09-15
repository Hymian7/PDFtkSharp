using System;
using PDFtkSharp;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Console.WriteLine();
            Merge();
            

            Console.WriteLine("Ende");

        }

        static void Extract()
        {
            //PDFExtractor extractor = new PDFExtractor(@"D:\Desktop\PDFtk\pdftk.exe");
            PDFExtractor extractor = new PDFExtractor();


            extractor.InputDocument = new System.IO.FileInfo(@"D:\Desktop\Debug\Test.pdf");
            extractor.OutputPath = new System.IO.DirectoryInfo(@"D:\Desktop\Debug\Output");
            extractor.OutputFileName = "out.pdf";

            //extractor.ExtractRange(new int[] { 1, 3, 5, 7, 9 });

            Console.WriteLine("Start Extracting");
            extractor.ExtractRange("1-3 7-end");

        }

        static void Merge()
        {

            PDFMerger merger = new PDFMerger() {

                OutputFileName = "merged.pdf",
                OutputPath= new System.IO.DirectoryInfo($"D:\\Desktop\\Debug\\Output\\")
            };

            merger.InputFiles.Add(new System.IO.FileInfo($"D:\\Desktop\\Debug\\1.pdf"));
            merger.InputFiles.Add(new System.IO.FileInfo($"D:\\Desktop\\Debug\\2.pdf"));
            merger.InputFiles.Add(new System.IO.FileInfo($"D:\\Desktop\\Debug\\3.pdf"));

            Console.WriteLine("Start Merging");

            if (merger.Merge() == true) { Console.WriteLine("Erfolgreich!"); }

        }
    }
}
