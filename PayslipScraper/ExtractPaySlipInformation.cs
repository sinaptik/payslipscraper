using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace PayslipScraper
{
	public class ExtractPaySlipInformation
	{
		static string payslipDirectory = Directory.GetCurrentDirectory() + @"/payslips_pdf";
        static string plaintextPayslipDirectory = Directory.GetCurrentDirectory() + @"/payslips_txt";
		
		private IExtractInformation[] _extractors;
		
		public List<Payslip> Payslips = new List<Payslip>();
		
		public ExtractPaySlipInformation (IExtractInformation[] extractors)
		{
			_extractors = extractors;
		}
		
		public List<Payslip> PerformExtraction(string password)
		{
            var xds = System.Environment.OSVersion;

			//get all pdfs
			var filesToConvert = Directory.GetFiles(payslipDirectory).Where(x => x.EndsWith(".pdf"));
			
			//don't convert files that already have been converted
			var textFiles = Directory.GetFiles(plaintextPayslipDirectory).Where(x => x.EndsWith(".txt"));
			
			filesToConvert = filesToConvert.Where(x => !textFiles.Any(y => y.Contains(x.Split('/').Last().Split('.').First())));
			
			foreach (var file in filesToConvert)
			{
				var fileName = file.Split('/').Last();
				var args = String.Format("-opw {0} {1}", password, fileName);

                //http://www.foolabs.com/xpdf/download.html

                if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                }
                else if (System.Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    Process.Start(new ProcessStartInfo("pdftotext", args) { UseShellExecute = false, WorkingDirectory = payslipDirectory }).WaitForExit();
                }
			}
			
			//get files from folder
			var files = Directory.GetFiles(plaintextPayslipDirectory);
			
			foreach (var file in files)
			{
				if (file.EndsWith(".txt"))
				{
					var input = System.IO.File.ReadAllText(file);

					var payslip = new Payslip();
					
					//run extractors
					foreach (var extractor in _extractors)
					{
						var extractedValue = extractor.Extract(input);
						
						//get the correct property
						PropertyInfo prop = typeof(Payslip).GetProperty(extractor.GetName(), BindingFlags.Public | BindingFlags.Instance);
				        
						//put the extracted value into the property
						if(null != prop && prop.CanWrite)
				        {
				            prop.SetValue(payslip, extractedValue, null);
				        }
					}	
					
					Payslips.Add(payslip);
				}
			}
			
			return Payslips;
		}
	}
}

