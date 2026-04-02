using AsmResolver;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;
using CrossAccord.AsmResolver.DotNet.CIL;
using CrossAccord.AsmResolver.DotNet.TestCases.CIL;

namespace CrossAccord.AsmResolver.DotNet.Tests.CIL;

public class CILProcessor_Tests
{
    private CilMethodBody _methodBody_1;
    
    [SetUp]
    public void Setup()
    {
        var exampleCaseDefinition =
            ModuleDefinition.FromFile(typeof(CILModificationTestCase).Assembly.Location, new(ThrowErrorListener.Instance));

        var method = (MethodDefinition)exampleCaseDefinition
            .LookupMember(typeof(CILModificationTestCase)
                .GetMethod(nameof(CILModificationTestCase.SimpleFunction))!.MetadataToken);

        if (method.CilMethodBody is null)
        {
            Assert.Fail("CILModificationTestCase.SimpleFunction method body is null");
            return;
        }

        _methodBody_1 = method.CilMethodBody;

    }

    [TestFixture]
    public class InsertAfter : CILProcessor_Tests
    {
        [Test]
        public void ShouldInsertAfterFirstInstruction()
        {
            var cilProcessor = new CILProcessor(_methodBody_1);
            var firstInstruction = _methodBody_1.Instructions[0];
            var instruction = new CilInstruction(CilOpCodes.Nop);

            cilProcessor.InsertAfter(firstInstruction, instruction);
            
            Assert.That(_methodBody_1.Instructions[1], Is.EqualTo(instruction));
        }
        
    }
}