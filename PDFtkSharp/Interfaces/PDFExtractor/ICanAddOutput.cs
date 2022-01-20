using System.IO;

namespace PDFtkSharp.Interfaces.PDFExtractor;

public interface ICanAddOutput
{
    public ICanAddOutputName WithOutputPath(DirectoryInfo outputPath);
    public ICanAddOutputName WithOutputPath(string outputPath);
    public ICanAddPagesOrRun WithOutputFile(FileInfo outputFile);
    public ICanAddPagesOrRun WithOutputFile(string outputFile);
    
}