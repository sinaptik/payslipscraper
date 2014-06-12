using System;
using System.Text.RegularExpressions;

namespace PayslipScraper
{
	public class ExtractPeriodEnd : IExtractInformation
	{
		public ExtractPeriodEnd ()
		{
		}

		#region IExtractInformation implementation
		public object Extract (string input)
		{
			//extract value
			var match = Regex.Match(input, @"PERIOD END\n([0-9]+/[0-9]+/[0-9]+)", RegexOptions.IgnoreCase);
		
			if (match.Success)
			{
				string key = match.Groups[1].Value;
				
				return Convert.ToDateTime(key);
			}
			
			throw new Exception();
		}
		
		public string GetName()
		{
			return "PeriodEnd";
		}
		#endregion
	}
}

