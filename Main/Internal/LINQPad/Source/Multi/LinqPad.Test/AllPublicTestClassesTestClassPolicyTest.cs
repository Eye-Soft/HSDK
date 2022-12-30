namespace EyeSoft.LinqPad.Test;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Policy;

[TestClass]
public class AllPublicTestClassesTestClassPolicyTest
{
	[TestMethod]
	public void VerifyPublicClassIsTestClass()
	{
		new PublicClassTestClassPolicy().IsTestClass(typeof(SampleTest)).Should().BeTrue();
	}

	public class SampleTest
	{
		// ReSharper disable once UnusedMember.Global
		public void Verify1()
		{
		}
	}
}