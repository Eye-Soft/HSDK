namespace EyeSoft.LinqPad.Test.Policy;

using System.Reflection;

public interface ITestMethodPolicy
{
	bool IsTestMethod(MethodBase method);
}