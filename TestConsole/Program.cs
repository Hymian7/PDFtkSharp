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
            Console.WriteLine("Start");
            Console.WriteLine();
            //Merge();
            //Extract();

            await RotatePages();
            

            Console.WriteLine("Ende");

        }

        static async Task Extract()
        {
            //PDFExtractor extractor = new PDFExtractor(@"D:\Desktop\PDFtk\pdftk.exe");
            PDFExtractor extractor = new PDFExtractor();


            extractor.InputDocument = new System.IO.FileInfo(@"D:\Desktop\Debug\Test.pdf");
            extractor.OutputPath = new System.IO.DirectoryInfo(@"D:\Desktop\Debug\Output");
            extractor.OutputName = "extracted.pdf";

            //extractor.ExtractRange(new int[] { 1, 3, 5, 7, 9 });

            Console.WriteLine("Start Extracting");
            
            await extractor.ExtractPagesAsync(new int[] { 99 });
            //extractor.ExtractRange("1-3 7-end");

            Console.WriteLine("Extracting Finished");


        }

        static async Task Merge()
        {

            PDFMerger merger = new PDFMerger() {

                OutputName = "merged.pdf",
                OutputPath= new System.IO.DirectoryInfo($"D:\\Desktop\\Debug\\Output\\")
            };

            merger.InputFiles.Add(new System.IO.FileInfo($"D:\\Desktop\\Debug\\1.pdf"));
            merger.InputFiles.Add(new System.IO.FileInfo($"D:\\Desktop\\Debug\\2.pdf"));
            merger.InputFiles.Add(new System.IO.FileInfo($"D:\\Desktop\\Debug\\3.pdf"));

            Console.WriteLine("Start Merging");

            await merger.MergeAsync();

            Console.WriteLine("Merging Finished");

        }

        static async Task RotatePages()
        {
            PDFRotator rot = new PDFRotator();
            rot.InputDocument = new System.IO.FileInfo($"D:\\Desktop\\Debug\\Rotation.pdf");

            Console.WriteLine($"Start rotation at {DateTime.Now.ToLongTimeString()}");

            await rot.RotatePagesAsync(new List<int>() { 1, 3 }, PDFRotator.Rotation.Left);

            Console.WriteLine($"End rotation at {DateTime.Now.ToLongTimeString()}");
        }
    }
}
