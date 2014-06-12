using System;
using Ninject;
using Ninject.Modules;

namespace PayslipScraper
{
	public class Bindings : NinjectModule
	{
		public Bindings ()
		{
		}

		public override void Load ()
		{
			Bind<ExtractPaySlipInformation>().ToSelf();
			Bind<IExtractInformation>().To<ExtractBankedPay>();
			Bind<IExtractInformation>().To<ExtractPayRate>();
			Bind<IExtractInformation>().To<ExtractPeriodEnd>();
			Bind<IExtractInformation>().To<ExtractSickLeave>();
			Bind<IExtractInformation>().To<ExtractAnnualLeave>();
		}
	}
}

