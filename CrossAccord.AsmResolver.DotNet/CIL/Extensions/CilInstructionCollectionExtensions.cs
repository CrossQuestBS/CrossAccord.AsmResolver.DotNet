using System;
using System.Collections.Generic;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;

namespace CrossAccord.AsmResolver.DotNet.CIL.Extensions;

public static class CilInstructionCollectionExtensions
{
    public static void InsertBefore(this CilInstructionCollection instructionCollection, CilInstruction target,
        CilInstruction instruction)
    {
        var index = instructionCollection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        instructionCollection.Insert(index, instruction);
    }

    public static void InsertAfter(this CilInstructionCollection instructionCollection, CilInstruction target,
        CilInstruction instruction)
    {
        var index = instructionCollection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        instructionCollection.Insert(index + 1, instruction);
    }

    public static void InsertRangeAfter(this CilInstructionCollection instructionCollection, CilInstruction target,
        IEnumerable<CilInstruction> instructions)
    {
        var index = instructionCollection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        instructionCollection.InsertRange(index + 1, instructions);
    }

    public static void InsertRangeBefore(this CilInstructionCollection instructionCollection, CilInstruction target,
        IEnumerable<CilInstruction> instructions)
    {
        var index = instructionCollection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        instructionCollection.InsertRange(index, instructions);
    }
}