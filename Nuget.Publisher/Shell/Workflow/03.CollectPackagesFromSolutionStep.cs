﻿namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Core;

	public class CollectPackagesFromSolutionStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public CollectPackagesFromSolutionStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			IEnumerable<string> packagesId,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
			this.previousVersions = previousVersions;
		}

		public UpdateNuspecAndAssemblyInfoStep CollectPackagesFromSolution()
		{
			return new UpdateNuspecAndAssemblyInfoStep(buildAndRevision, solutionSystemInfo, packagesId, previousVersions);
		}
	}
}