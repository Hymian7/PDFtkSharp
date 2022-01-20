using System.Threading.Tasks;

namespace PDFtkSharp.Interfaces.PDFExtractor
{
    public interface ICanAddPagesOrRun
    {
        ICanAddPagesOrRun AddPage(int pageNumber);
        ICanAddPagesOrRun AddRange(int startPage, int endPage);
        ICanAddPagesOrRun AddRange(PageMark pageMark, int endPage);
        ICanAddPagesOrRun AddRange(int start, PageMark pageMark);
        void Extract();
        Task ExtractAsync();
    }
}