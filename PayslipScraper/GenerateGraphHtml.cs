using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipScraper
{
	public class GenerateGraphHtml
	{
		private List<Payslip> _payslips;
		
		public GenerateGraphHtml (List<Payslip> payslips)
		{
			_payslips = payslips;
		}
		
		public string GenerateJavascript()
		{
			var builder = new StringBuilder();
				//['x', '2013-01-01', '2013-01-02', '2013-01-03', '2013-01-04', '2013-01-05', '2013-01-06'],
				//['data1', 30, 200, 100, 400, 150, 250],
				//['data2', 130, 340, 200, 500, 250, 350]
			
			builder.Append("['x', ");
			
			for(var i = 0; i < _payslips.Count; i++)
			{
				builder.Append("'");
				builder.Append(String.Format("{0:yyyy/MM/dd}", _payslips[i].PeriodEnd));
				
				if (i < _payslips.Count - 1)
					builder.Append("', ");
				else
					builder.Append("' ");
			}
					
			builder.Append("],\n");
			
			///
			
			builder.Append("['Annual leave', ");
			
			for(var i = 0; i < _payslips.Count; i++)
			{
				builder.Append(_payslips[i].AnnualLeave);
				
				if (i < _payslips.Count - 1)
					builder.Append(", ");
			}
			
			builder.Append("],\n");
			
			///
			
			builder.Append("['Sick leave', ");
			
			for(var i = 0; i < _payslips.Count; i++)
			{
				builder.Append(_payslips[i].SickLeave);
				
				if (i < _payslips.Count - 1)
					builder.Append(", ");
			}
			
			builder.Append("]\n");
			
			return builder.ToString();
		}
	}
}

