using System;
using System.Collections.Generic;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;

namespace CrossAccord.AsmResolver.DotNet.CIL.Extensions;

public static class CilInstructionCollectionExtensions
{
    public static void InsertBefore(this CilInstructionCollection collection, CilInstruction target,
        CilInstruction instruction)
    {
        var index = collection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        collection.Insert(index, instruction);
    }

    public static void InsertAfter(this CilInstructionCollection collection, CilInstruction target,
        CilInstruction instruction)
    {
        var index = collection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        collection.Insert(index + 1, instruction);
    }

    public static void InsertRangeAfter(this CilInstructionCollection collection, CilInstruction target,
        IEnumerable<CilInstruction> instructions)
    {
        var index = collection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        collection.InsertRange(index + 1, instructions);
    }

    public static void InsertRangeBefore(this CilInstructionCollection collection, CilInstruction target,
        IEnumerable<CilInstruction> instructions)
    {
        var index = collection.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        collection.InsertRange(index, instructions);
    }
}