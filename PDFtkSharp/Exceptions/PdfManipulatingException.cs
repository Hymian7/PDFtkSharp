using System;
using System.Collections.Generic;
using System.Text;

namespace PDFtkSharp
{
    public class PdfManipulatingException : Exception
    {
        public PdfManipulatingException() : base()
        { }

        public PdfManipulatingException(string message) :base(message)
        {

        }

        public PdfManipulatingException(string message, Exception inner) :base(message, inner)
        {

        }

    }
}
