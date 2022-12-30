namespace EyeSoft.LinqPad.Test;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Policy;

[TestClass]
public class OnlyMsTestClassesTestClassPolicyTest
{
	[TestMethod]
	public void VerifyNotMsTestClassIsNotTestType()
	{
		new OnlyMsTestClassesTestClassPolicy().IsTestClass(typeof(SampleTest)).Should().BeFalse();
	}

	private class SampleTest
	{
		// ReSharper disable once UnusedMember.Local
		public void Verify1()
		{
		}
	}
}