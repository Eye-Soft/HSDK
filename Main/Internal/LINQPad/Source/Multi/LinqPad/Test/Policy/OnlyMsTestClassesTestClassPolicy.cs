namespace EyeSoft.LinqPad.Test.Policy;

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class OnlyMsTestClassesTestClassPolicy : ITestClassPolicy
{
    private readonly bool forceIfIgnoreAttribute;

    public OnlyMsTestClassesTestClassPolicy(bool forceIfIgnoreAttribute = false)
    {
        this.forceIfIgnoreAttribute = forceIfIgnoreAttribute;
    }

    public bool IsTestClass(Type type)
    {
        var testClassAttribute = typeof(TestClassAttribute);
        var ignoreAttributeType = typeof(IgnoreAttribute);

        var isTestClass =
            type is { IsClass: true, IsAbstract: false } &&
            type
                .CustomAttributes
                .Any(y => y.AttributeType == testClassAttribute && (forceIfIgnoreAttribute || y.AttributeType == ignoreAttributeType));

        return isTestClass;
    }
}