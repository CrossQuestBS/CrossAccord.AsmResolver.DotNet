using System;

namespace CrossAccord.AsmResolver.DotNet.TestCases.CIL;

public class CILModificationTestCase
{
    public void SimpleFunction()
    {
        var loadStr = "Hello!";
        var call = String.Copy(loadStr);
        Console.WriteLine(call);
    }
}