using System;
using System.Reflection;
using System.Linq;
using Ninject;

namespace PayslipScraper
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
			
			var extractor = kernel.Get<ExtractPaySlipInformation>();
			
			var payslips = extractor.PerformExtraction(args[0]);
			
			var sorted = payslips.OrderBy(x => x.PeriodEnd).ToList();
			
			//foreach (var slip in sorted)
			//	Console.WriteLine(string.Format("{0}, {1}", slip.PeriodEnd.ToShortDateString(), slip.AnnualLeave));
			
			var makeHtml = new GenerateGraphHtml(sorted);
			Console.WriteLine(makeHtml.GenerateJavascript());

            Console.ReadLine();
		}
	}
}
