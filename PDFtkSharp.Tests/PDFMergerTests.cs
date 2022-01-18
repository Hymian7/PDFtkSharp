using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFtkSharp;
using System.IO;
using System.Collections.Generic;

namespace PDFtkSharp.Tests;

[TestClass]
public class PDFMergerTests
{

    private static DirectoryInfo outputPath = new("../../../assets/output");
    private static string outputName = "merged.pdf";
    private static List<FileInfo> inputFiles = new()    {
                                                            new FileInfo("../../../assets/test.pdf"),
                                                            new FileInfo("../../../assets/test2.pdf")
                                                        } ;

    [ClassInitialize]
    public static void CreateOutputDirectory(TestContext context)
    {
        if(!outputPath.Exists) outputPath.Create();
    }

    
    [TestMethod]
    public void MergeTwoFiles()
    {
        if(System.IO.File.Exists(Path.Combine(outputPath.FullName, outputName)))
        {
            Debug.WriteLine("Deleting file " + System.IO.Path.Combine(outputPath.FullName, outputName) );
            System.IO.File.Delete(System.IO.Path.Combine(outputPath.FullName, outputName));
        }

        PDFMerger merger = new();
        merger.InputFiles = inputFiles;
        merger.OutputName = outputName;
        merger.OutputPath = outputPath;

        //Act
        Debug.WriteLine($"Merging Input Files {Environment.NewLine + String.Join(Environment.NewLine, inputFiles) + Environment.NewLine} to {System.IO.Path.Combine(merger.OutputPath.FullName, merger.OutputName)}");
        Task.WaitAll(merger.MergeAsync());

         
        //Assert
        Assert.IsTrue(File.Exists(Path.Combine(merger.OutputPath.FullName, merger.OutputName)));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public async Task Merge_LessThanTwoPages_Throws_ArgumentOutOfRangeException()
    {

        PDFMerger merger = new();
        merger.InputFiles = new List<FileInfo>();
        merger.OutputName = outputName;
        merger.OutputPath = outputPath;

        //Act
              Debug.WriteLine($"Merging Input Files {Environment.NewLine + String.Join(Environment.NewLine, inputFiles) + Environment.NewLine} to {System.IO.Path.Combine(merger.OutputPath.FullName, merger.OutputName)}");
        await merger.MergeAsync();

         
        //Assert
    }

    [ClassCleanup]
    public static void DeleteAllOutputFiles()
    {
        var outputFile = new FileInfo(Path.Combine(outputPath.FullName, outputName));
        if(outputFile.Exists) outputFile.Delete();
    }
}