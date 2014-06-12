using System;
using System.Text.RegularExpressions;

namespace PayslipScraper
{
	public class ExtractPayRate : IExtractInformation
	{
		public ExtractPayRate ()
		{
		}

		#region IExtractInformation implementation
		public object Extract (string input)
		{	
			//extract value
			var match = Regex.Match(input, @"PAY RATE\n([0-9]+\.[0-9]+)", RegexOptions.IgnoreCase);
			
			var match2 = Regex.Match(input, @"PAY RATE\nVALUE\n([0-9]+\.[0-9]+)", RegexOptions.IgnoreCase);
		
			if (match.Success)
			{
				string key = match.Groups[1].Value;
				
				return Convert.ToDouble(key);
			}
			else if (match2.Success)
			{
				string key = match2.Groups[1].Value;
				
				return Convert.ToDouble(key);
			}
			
			return 0.0f;
		}
		
		public string GetName()
		{
			return "PayRate";
		}
		#endregion
	}
}

