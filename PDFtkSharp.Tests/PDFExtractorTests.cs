using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFtkSharp;
using Tesseract;

namespace PDFtkSharp.Tests;

[TestClass]
public class PDFExtractorTests
{

    private static string inputFile = @"..\..\..\assets\test.pdf";
    private static string outputPath = @"..\..\..\assets\output\";

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(5)]
    [DataRow(6)]
    [DataRow(7)]
    [DataRow(8)]
    [DataRow(9)]
    [DataRow(10)]
    public void Extract_Single_Page(int value)
    {
        //Arrange
        string outputName = $"extracted-page-{value}.pdf";
        if(System.IO.File.Exists(System.IO.Path.Combine(outputPath, outputName)))
        {
            Debug.WriteLine("Deleting file " +System.IO.Path.Combine(outputPath, outputName) );
            System.IO.File.Delete(System.IO.Path.Combine(outputPath, outputName));
        }

        PDFExtractor ext = new();
        ext.InputDocument = new System.IO.FileInfo(inputFile);
        ext.OutputName = outputName;
        ext.OutputPath = new System.IO.DirectoryInfo(outputPath);

        //Act
        Debug.WriteLine($"Extracting page {value} from {inputFile} to {System.IO.Path.Combine(ext.OutputPath.FullName, ext.OutputName)}");
        Task.WaitAll(ext.ExtractPagesAsync(value));

         
        //Assert
        Assert.IsTrue(System.IO.File.Exists(System.IO.Path.Combine(ext.OutputPath.FullName, ext.OutputName)));
    }

    [TestMethod]
    //Page 0 means all pages
    //[DataRow(0)]
    [DataRow(-1)]
    [DataRow(11)]
    [ExpectedException(typeof(PdfManipulatingException))]
    public async Task Extract_Invalid_Page_Throws_PdfManipulationException(int value)
    {
        //Arrange
        string outputName = $"extracted-page-{value}.pdf";


        PDFExtractor ext = new();
        ext.InputDocument = new System.IO.FileInfo(inputFile);
        ext.OutputName = outputName;
        ext.OutputPath = new System.IO.DirectoryInfo(outputPath);

        //Act
        Debug.WriteLine($"Trying to extract page {value} from {inputFile} to {System.IO.Path.Combine(ext.OutputPath.FullName, ext.OutputName)}");
        await ext.ExtractPagesAsync(value);

         
        //Assert
    }

    [ClassCleanup]
    public static void DeleteAllOutputFiles()
    {
        foreach(var file in System.IO.Directory.GetFiles(outputPath))
        {
            var fi = new System.IO.FileInfo(file);
            fi.Delete();
        }
    }
}