using System.IO;

namespace PDFtkSharp.Interfaces.PDFExtractor
{

public interface ICanAddInput
{
    ICanAddOutput WithInputFile(string inputFile);
    ICanAddOutput WithInputFile(FileInfo inputFile);
}

}