namespace EyeSoft.LinqPad.Test.Policy;

using System;

public class PublicClassTestClassPolicy : ITestClassPolicy
{
    public bool IsTestClass(Type type)
    {
        var isTestClass =
            type is { IsClass: true, IsAbstract: false } &&
            type.Name.EndsWith("Test");

        return isTestClass;
    }
}