# PDFtkSharp
C# Wrapper around PDFLabs PDFtk Server CLI

Also check out https://www.pdflabs.com/tools/pdftk-server/
and https://github.com/Tyrrrz/CliWrap

# Usage

## Extract Pages from PDF

### Classic

```
PDFExtractor ext = new PDFExtractor();

ext.InputFile = new System.IO.FileInfo("path/to/input/file.pdf");
ext.OutputName = outputName;
ext.OutputPath = new System.IO.DirectoryInfo("path/to/output");

await ext.ExtractAsync(1,3,4,5);
```

### Fluent

```
await PDFExtractor
      .WithInputFile("path/to/input/file.pdf")
      .WithOutputPath("path/to/output")
      .WithOutputName("outputName")
      .AddRange(Pagemark.Start,2)
      .AddPage(4)
      .AddRange(7,9)
      .AddRange(12,Pagemark.End)
      .ExtractAsync();
```

## Merge PDFs

### Classic

```
PDFMerger merger = new();

merger.InputFiles = new List<FileInfo>()    {
                                              new FileInfo("file1.pdf"),
                                              new FileInfo("file2.pdf")
                                            } ;
merger.OutputName = "outputName";
merger.OutputPath = "path/for/output";
  
await merger.MergeAsync();
``` 

### Fluent
  
No fluent API yet.
