using System;

namespace PayslipScraper
{
	public class Payslip
	{
		public double BankedPay { get; set; }
		public double PayRate { get; set; }
		public DateTime PeriodEnd { get; set;}
		public double SickLeave { get; set; }
		public double AnnualLeave { get; set; }
	}
}

