namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.Linq;

	public class HsdkWorkflow
	{
	    public const string SolutionFolderPath = @"D:\Pw.Vs.com\Dc\Es.Hsdk\Main\EyeSoft.Hsdk.sln";

		private static readonly IEnumerable<string> packagesId =
			new[]
				{
					"Accounting",
					"Accounting.Italian",
					"Accounting.Italian.Istat",
					"AutoMapper",
					"Core",
					"Data",
					////"Data.EntityFramework",
					////"Data.EntityFramework.Caching",
					////"Data.EntityFramework.Toolkit",
					////"Data.EntityFramework.Tracing",
					"Data.Nhibernate",
					//"Data.Raven",
					"Data.SqLite",
					"Domain",
					"DynamicProxy",
					"FluentValidation",
					"ServiceLocator",
					"ServiceLocator.Windsor",
					"ServiceModel",
					"ServiceStack.Text",
					"SharpTests.Extensions",
					"Shimmer.Client",
					"Web",
					"Windows",
					"Windows.Installer.InstallMate",
					"Windows.Model"
				}
			.Select(x => string.Concat("EyeSoft.", x))
			.ToArray();

		public RetrievePreviousVersionsStep GenerateBuildAndRevision()
		{
			return new GenerateBuildAndRevisionStep(SolutionFolderPath, packagesId).GenerateBuildAndRevision();
		}
	}
}