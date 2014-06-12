using System;
using System.Text.RegularExpressions;

namespace PayslipScraper
{
	public class ExtractBankedPay : IExtractInformation
	{
		public ExtractBankedPay ()
		{
		}

		#region IExtractInformation implementation
		public object Extract (string input)
		{
			//extract value
			var match = Regex.Match(input, @"NET PAY:\n([0-9]+\.[0-9]+)", RegexOptions.IgnoreCase);
		
			if (match.Success)
			{
				string key = match.Groups[1].Value;
				
				return Convert.ToDouble(key);
			}
			
			throw new Exception();
		}
		
		public string GetName()
		{
			return "BankedPay";
		}
		#endregion
	}
}

