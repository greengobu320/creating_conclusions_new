﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WpfApp1
{
    internal class splitPdf
    {
        public  void  SplitPdf(string fileName, string OutDirectory)
        {
            foreach (var file in Directory.GetFiles(OutDirectory, "*.pdf"))
            {
               
                File.Delete(file);
            }

            try
            {               
                int pageSize = 2;
                using (PdfReader reader = new PdfReader(fileName))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i += pageSize)
                    {
                        using (Document document = new Document())
                        {
                            string outputPdf = Path.Combine(OutDirectory, $"output_file_{i / pageSize + 1}.pdf");
                            PdfCopy copy = new PdfCopy(document, new FileStream(outputPdf, FileMode.Create));
                            document.Open();

                            for (int j = 0; j < pageSize && (i + j) <= reader.NumberOfPages; j++)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, i + j));
                            }
                            document.Close();
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
