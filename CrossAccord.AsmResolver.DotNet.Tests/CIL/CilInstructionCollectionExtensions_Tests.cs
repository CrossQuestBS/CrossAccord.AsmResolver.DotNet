using AsmResolver;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;
using CrossAccord.AsmResolver.DotNet.CIL.Extensions;
using CrossAccord.AsmResolver.DotNet.TestCases.CIL;

namespace CrossAccord.AsmResolver.DotNet.Tests.CIL;

public class CilInstructionCollectionExtensions_Tests
{
    private CilMethodBody _methodBody_1;

    [SetUp]
    public void Setup()
    {
        var exampleCaseDefinition =
            ModuleDefinition.FromFile(typeof(CILModificationTestCase).Assembly.Location,
                new(ThrowErrorListener.Instance));

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
    public class InsertAfter : CilInstructionCollectionExtensions_Tests
    {
        [Test]
        public void ShouldInsertAfterFirstInstruction()
        {
            var firstInstruction = _methodBody_1.Instructions[0];
            var instruction = new CilInstruction(CilOpCodes.Nop);

            _methodBody_1.Instructions.InsertAfter(firstInstruction, instruction);

            Assert.That(_methodBody_1.Instructions[1], Is.EqualTo(instruction));
        }
    }


    [TestFixture]
    public class InsertRangeAfter : CilInstructionCollectionExtensions_Tests
    {
        [Test]
        public void ShouldInsertAfterFirstInstruction()
        {
            var idx = 0;
            var target = _methodBody_1.Instructions[idx];

            var instructionsToInsert = new CilInstruction[]
            {
                new(CilOpCodes.Nop),
                new(CilOpCodes.Ldstr, "Hello!")
            };


            _methodBody_1.Instructions.InsertRangeAfter(target, instructionsToInsert);

            Assert.That(_methodBody_1.Instructions[idx], Is.EqualTo(target));

            idx++;
            foreach (var instruction in instructionsToInsert)
            {
                Assert.That(_methodBody_1.Instructions[idx], Is.EqualTo(instruction));
                idx++;
            }
        }
    }

    [TestFixture]
    public class InsertRangeBefore : CilInstructionCollectionExtensions_Tests
    {
        [Test]
        public void ShouldInsertAfterFirstInstruction()
        {
            var idx = 0;
            var target = _methodBody_1.Instructions[idx];

            var instructionsToInsert = new CilInstruction[]
            {
                new(CilOpCodes.Nop),
                new(CilOpCodes.Ldstr, "Hello!")
            };


            _methodBody_1.Instructions.InsertRangeBefore(target, instructionsToInsert);

            // idx: 0
            //  instruction == (Nop)
            // idx: 1
            //  instruction == (Ldstr, "Hello!")
            foreach (var instruction in instructionsToInsert)
            {
                Assert.That(_methodBody_1.Instructions[idx], Is.EqualTo(instruction));
                idx++;
            }

            // idx: 2
            //  instruction == target
            Assert.That(_methodBody_1.Instructions[idx], Is.EqualTo(target));
        }
    }

    [TestFixture]
    public class InsertBefore : CilInstructionCollectionExtensions_Tests
    {
        [Test]
        public void ShouldInsertBeforeFirstInstruction()
        {
            var firstInstruction = _methodBody_1.Instructions[0];
            var instruction = new CilInstruction(CilOpCodes.Nop);

            _methodBody_1.Instructions.InsertBefore(firstInstruction, instruction);

            Assert.That(_methodBody_1.Instructions[0], Is.EqualTo(instruction));
            Assert.That(_methodBody_1.Instructions[1], Is.EqualTo(firstInstruction));
        }
    }
}