<Query Kind="Program">
  <DisableMyExtensions>true</DisableMyExtensions>
</Query>

void Main()
{
	Util.AutoScrollResults = true;
	
	var basePath = @"D:\GitHub\HSDK\Main";

	var newVersion = GetNewVersion().Dump("New version");
	
	ShowAllVersions(basePath);
	return;
	CopyAllPackagesToFolderForPublish(basePath, true);
	
	var newVersionText = "3.0.7736.24658";
	var oldYears = "2020";
	return;
	ReplaceOldVersionWithNewVersion(basePath, "3.0.7714.20388", newVersionText, oldYears);
	ReplaceOldVersionWithNewVersion(basePath, "3.0.7349.34482", newVersionText, oldYears);
	ReplaceOldVersionWithNewVersion(basePath, "3.0.7697.30643", newVersionText, oldYears);

	BuildTheSolution(basePath);
	
	//CopyAllPackagesToFolderForPublish(basePath, true);
	//return;
	//var msbuild = $@"""{msBuildPath}"" ""D:\GitHub\HSDK\Main\Internal\Windows\Source\4.0\Windows.Model\EyeSoft.Windows.Model.csproj"" /p:Configuration=""Release"" /t:Rebuild;Pack /p:IncludeSource=true /p:IncludeSymbols=true /p:GeneratePackageOnBuild=true /p:SymbolPackageFormat=snupkg";

	CopyAllPackagesToFolderForPublish(basePath, false);
}

public void ShowAllVersions(string basePath)
{
	new DirectoryInfo(basePath)
		.GetFiles("*.*", SearchOption.AllDirectories)
		.Where(x => new[] { ".nuspec", ".csproj" }.Contains(x.Extension))
		.Select(x => new { x.FullName, Lines = File.ReadAllLines(x.FullName).Where(y =>
			(y.Contains("<version>") ||
			y.Contains("<dependency ")) &&
			y.Contains("3.")) })
		.Where(x => x.Lines.Any())
		.Dump();
}

void BuildTheSolution(string basePath)
{
	var msBuildPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\msbuild.exe";
	var msbuild = $@"""{msBuildPath}"" ""{basePath}\EyeSoft.Hsdk.sln"" /p:Configuration=""Release"" /t:Rebuild /p:IncludeSource=true /p:IncludeSymbols=true /p:GeneratePackageOnBuild=true /m";
	msbuild.Dump();
	Util.Cmd(msbuild);
}

void CopyAllPackagesToFolderForPublish(string basePath, bool deleteSymbols)
{
	var nuget = new DirectoryInfo(basePath);
	
	var symbolsPackages =
		nuget
			.GetFiles("*.*", SearchOption.AllDirectories)
			.Where(x => x.Name.EndsWith(".snupkg") || x.Name.EndsWith(".symbols.nupkg") || x.Name.EndsWith(".nupkg"));

	if (deleteSymbols)
	{
		symbolsPackages.ToList().ForEach(x => x.Delete());
		return;
	}
	
	var builtSymbols =
		symbolsPackages
			.Where(x => x.Directory.Name == "Release")
			.Select(x => Explorer.CreateLink(x.FullName))
			.Dump();
}

public void ReplaceOldVersionWithNewVersion(string basePath, string currentVersion, string newVersion, params string[] years)
{
	var files =
	new DirectoryInfo(basePath)
		.GetFiles("*.*", SearchOption.AllDirectories)
		.Where(x => new[] { ".csproj" }.Contains(x.Extension) || x.Name == "AssemblyInfo.cs" ||  x.Name == "Package.nuspec")
		.Select(x => new { File = x, Text = File.ReadAllText(x.FullName, Encoding.UTF8) })
		.ToArray();

	if (years == null || !years.Any())
	{
		throw new InvalidOperationException("Years must be not null or empty.");
	}
	
	var currentYear = DateTime.UtcNow.Year;
	var list = new List<string>();
	
	foreach (var file in files)
	{
		var updatedText = file.Text;
		
		switch (file.File.Extension)
		{
			case ".cs":
				updatedText =
					updatedText
						.Replace($"[assembly: AssemblyVersion(\"{currentVersion}\")]", $"[assembly: AssemblyVersion(\"{newVersion}\")]")
						.Replace($"[assembly: AssemblyFileVersion(\"{currentVersion}\")", $"[assembly: AssemblyFileVersion(\"{newVersion}\")");

				foreach (var year in years)
				{
					updatedText = updatedText
						.Replace($"[assembly: AssemblyCopyright(\"Copyright © EyeSoft {year}\")]", $"[assembly: AssemblyCopyright(\"Copyright © EyeSoft {currentYear}\")]");
				}

				break;

			case ".csproj":
				updatedText = updatedText.Replace($"<Version>{currentVersion}</Version>", $"<Version>{newVersion}</Version>");

				foreach (var year in years)
				{
					updatedText = updatedText
						.Replace($"<Copyright>EyeSoft ©{year}</Copyright>", $"<Copyright>EyeSoft ©{currentYear}</Copyright>");
				}
			
				break;
		}
		
		if (file.Text != updatedText)
		{
			list.Add(file.File.FullName);
			File.WriteAllText(file.File.FullName, updatedText, Encoding.UTF8);
		}
	}
	
	list.Dump();
}

private string GetNewVersion()
{
	var dateTime = DateTime.UtcNow;
	var version = new Version(3, 0).Increment(dateTime);

	return $"{version}";
}

public static class VersionHelper
{
	private static readonly DateTime referenceDate = new DateTime(2000, 1, 1);

	public static Version Increment(this Version version, DateTime? dateTime = null)
	{
		return Increment(version.Major, version.Minor, dateTime);
	}

	public static Version Increment(this Version version, BuildAndRevision buildAndRevision)
	{
		return Increment(version.Major, version.Minor, buildAndRevision);
	}

	public static Version Increment(int major, int minor, DateTime? dateTime)
	{
		var buildAndRevision = GenerateBuildAndRevision(dateTime);

		return Increment(major, minor, buildAndRevision);
	}

	public static Version Increment(int major, int minor, BuildAndRevision buildAndRevision)
	{
		return new Version(major, minor, (int)buildAndRevision.Build, (int)buildAndRevision.Revision);
	}

	public static BuildAndRevision GenerateBuildAndRevision(DateTime? dateTime = null)
	{
		var localDateTime = dateTime ?? DateTime.Now;

		var build = (localDateTime.Date - referenceDate).TotalDays;
		var revision = (localDateTime - localDateTime.Date).TotalSeconds / 2;

		return new BuildAndRevision((int)build, (int)revision);
	}

	public static DateTime GetDateTime(this Version version)
	{
		var date = referenceDate.AddDays(version.Build).AddSeconds(version.Revision * 2);
		return date;
	}
}

public class BuildAndRevision
{
	public BuildAndRevision(int build, int revision)
	{
		Build = build;
		Revision = revision;

		try
		{
			Date = new DateTime(2000, 1, 1).AddDays(build).AddSeconds(revision * 2);
		}
		catch
		{
		}
	}

	public int Build { get; }

	public int Revision { get; }

	public DateTime? Date { get; }

	public override string ToString()
	{
		return Date.HasValue ? $"{Build} {Revision} {Date.Value}" : $"{Build} {Revision}";
	}
}

public static class Explorer
{
	public static Hyperlinq CreateLink(string path, string text = null)
	{
		var hyperlinq = new Hyperlinq(() => OpenFileInExplorer(path), text?? path);
		
		return hyperlinq;
	}
	
	public static void OpenFileInExplorer(string path)
	{
		Process.Start("explorer", string.Format("/select, \"{0}\"", path));
	}
}