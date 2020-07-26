using System;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Specifics the numeric values of the various opcodes.
    /// </summary>
    public enum OpCode : ushort
    {
    
        /// <summary>
        /// Represents an unknown opcode. Potentially trailing procedure data.
        /// </summary>
        Invalid = 0xFFFF,

        ExitProcHresult = 0x13,

        ExitProc = 0x14,

        LitStr = 0x1B,

        /// <summary>
        /// A Variant variable is declared and initialized with an Integer which is then placed on the stack.
        /// </summary>
        LitVarI2 = 0x28,

        /// <summary>
        /// Perform concatination operation on two strings.
        /// </summary>
        ConcatStr = 0x2A,

        /// <summary>
        /// Define string variant.
        /// </summary>
        LitVarStr = 0x3A,

        SetLastSystemError = 0x3C,

        /// <summary>
        /// Convert to Variant from Currency.
        /// </summary>
        CVarCy = 0x3F,

        /// <summary>
        /// Pops an Integer off the stack and stores it within the specified Integer variable.
        /// </summary>
        FStI2 = 0x70,

        /// <summary>
        /// An Integer is pushed to the stack.
        /// </summary>
        LitI2 = 0xF3,

        /// <summary>
        /// An Integer is pushed to the stack from a provided Byte.
        /// </summary>
        LitI2_Byte = 0xF4,

        /// <summary>
        /// A Long is pushed to the stack.
        /// </summary>
        LitI4 = 0xF5,

        /// <summary>
        /// A Currency is pushed to the stack.
        /// </summary>
        LitCy = 0xF6,

        /// <summary>
        /// A Date variable is declared and initialized with an Integer which is then placed on the stack.
        /// </summary>
        LitDate = 0xFA,

        /// <summary>
        /// Pops an Integer value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1I2 = 0xFC0D,

        /// <summary>
        /// Pops a Long value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1I4 = 0xFC0E,

        /// <summary>
        /// Pops a Single value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1R4 = 0xFC0F,

        /// <summary>
        /// Pops a Double value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1R8 = 0xFC10,

        /// <summary>
        /// Pops a Currency value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1Cy = 0xFC11,

        /// <summary>
        /// Pops a Variant value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1Var = 0xFC12,

        /// <summary>
        /// Pops a String value off the stack and converts it to a Byte value.
        /// </summary>
        CUI1Str = 0xFC13,

        /// <summary>
        /// Pops a Byte value off the stack and converts it to an Integer value.
        /// </summary>
        CI2UI1 = 0xFC14,

        /// <summary>
        /// Pops a Long value off the stack and converts it to an Integer value.
        /// </summary>
        CI2I4 = 0xFC16,

        /// <summary>
        /// Pops a Single value off the stack and converts it to an Integer value.
        /// </summary>
        CI2R4 = 0xFC17,

        /// <summary>
        /// Pops a Double value off the stack and converts it to an Integer value.
        /// </summary>
        CI2R8 = 0xFC18,

        /// <summary>
        /// Pops a Currency value off the stack and converts it to an Integer value.
        /// </summary>
        CI2Cy = 0xFC19,

        /// <summary>
        /// Pops a Variant value off the stack and converts it to an Integer value.
        /// </summary>
        CI2Var = 0xFC1A,

        /// <summary>
        /// Pops a String value off the stack and converts it to an Integer value.
        /// </summary>
        CI2Str = 0xFC1B,

        /// <summary>
        /// Pops a Byte value off the stack and converts it to an Long value.
        /// </summary>
        CI4UI1 = 0xFC1C,

        /// <summary>
        /// Pops a Single value off the stack and converts it to an Long value.
        /// </summary>
        CI4R4 = 0xFC1F,

        /// <summary>
        /// Pops a Double value off the stack and converts it to an Long value.
        /// </summary>
        CI4R8 = 0xFC20,

        /// <summary>
        /// Pops a Currency value off the stack and converts it to an Long value.
        /// </summary>
        CI4Cy = 0xFC21,

        /// <summary>
        /// Pops a Variant value off the stack and converts it to an Long value.
        /// </summary>
        CI4Var = 0xFC22,

        /// <summary>
        /// Pops a String value off the stack and converts it to an Long value.
        /// </summary>
        CI4Str = 0xFC23,

        /// <summary>
        /// Pops a Byte variable off the stack and stores it in the specified variable.
        /// </summary>
        FStUI1 = 0xFCF0,

        /// <summary>
        /// Pops a Variant off the stack and stores it in the specified variable.
        /// </summary>
        FStVar = 0xFCF6,

    }

}
