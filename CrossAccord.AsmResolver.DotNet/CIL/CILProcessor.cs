using System;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;

namespace CrossAccord.AsmResolver.DotNet.CIL;

public class CILProcessor
{
    readonly CilMethodBody body;
    readonly CilInstructionCollection instructions;

    public CilMethodBody Body => body;

    public CILProcessor(CilMethodBody cilMethodBody)
    {
        body = cilMethodBody;
        instructions = cilMethodBody.Instructions;
    }

    public void InsertAfter(CilInstruction target, CilInstruction instruction)
    {
        var index = instructions.IndexOf(target);

        if (index == -1)
            throw new ArgumentOutOfRangeException(nameof(target));

        instructions.Insert(index + 1, instruction);
    }
}