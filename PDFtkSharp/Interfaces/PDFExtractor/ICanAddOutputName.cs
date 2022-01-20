namespace PDFtkSharp.Interfaces.PDFExtractor
{
    public interface ICanAddOutputName
    {
        public ICanAddPagesOrRun WithOutputName(string outputName);
    }
}