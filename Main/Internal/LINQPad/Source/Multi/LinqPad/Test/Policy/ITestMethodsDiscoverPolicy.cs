namespace EyeSoft.LinqPad.Test.Policy;

using System;
using System.Collections.Generic;
using System.Reflection;

public interface ITestMethodsDiscoverPolicy
{
    IEnumerable<MethodBase> GetMethods(Type type);
}