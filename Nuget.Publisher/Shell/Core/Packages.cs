namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public static class Packages
	{
		public static Package Parse(FileInfo assemblyInfoFile)
		{
			try
			{
				var lines = Storage.ReadAllLines(assemblyInfoFile.FullName);

				var title = AssemblyInfo.GetData(lines, AssemblyInfoData.AssemblyTitle);

				var projectFile = assemblyInfoFile.Directory.Parent.GetFiles(title + "*.csproj").SingleOrDefault();

				if (projectFile == null)
				{
					return null;
				}

				var targetFrameworkElement = XElement.Load(projectFile.FullName)
					.Descendants("{http://schemas.microsoft.com/developer/msbuild/2003}TargetFrameworkVersion")
					.SingleOrDefault();

				var targetFramework = "4.0";

				if (targetFrameworkElement != null)
				{
					targetFramework = targetFrameworkElement.Value.Replace("v", null);
				}

				var version = AssemblyInfo.GetData(lines, AssemblyInfoData.AssemblyVersion);

				var nuspec = projectFile.Directory.GetFiles("Package.nuspec").SingleOrDefault();

				if (nuspec == null)
				{
					return new Package(assemblyInfoFile, lines, title, version, targetFramework);
				}

				return new Package(assemblyInfoFile, lines, title, version, targetFramework, nuspec);
			}
			catch (Exception exception)
			{
				throw new IOException(string.Format("Cannot parse the file {0}.", assemblyInfoFile.FullName), exception);
			}
		}
	}
}