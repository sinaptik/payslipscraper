using System;

namespace PayslipScraper
{
	public interface IExtractInformation
	{
		object Extract(string input);
		string GetName();
	}
}

