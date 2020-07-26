using System;

namespace VB6DotNet.PCode
{

    public partial class OpDescriptor
    {

        public static OpDescriptor ExitProcHresult { get; } = new OpDescriptor(
            OpCode.ExitProcHresult);

        public static OpDescriptor ExitProc { get; } = new OpDescriptor(
            OpCode.ExitProc);

        public static OpDescriptor LitStr { get; } = new OpDescriptor(
            OpCode.LitStr, 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.String));

        /// <summary>
        /// A Variant variable is declared and initialized with an Integer which is then placed on the stack.
        /// </summary>
        public static OpDescriptor LitVarI2 { get; } = new OpDescriptor(
            OpCode.LitVarI2, 
            new OpDescriptorArg(OpArgType.Variable, OpArgValueType.Variant), 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.Integer));

        /// <summary>
        /// Perform concatination operation on two strings.
        /// </summary>
        public static OpDescriptor ConcatStr { get; } = new OpDescriptor(
            OpCode.ConcatStr);

        /// <summary>
        /// Define string variant.
        /// </summary>
        public static OpDescriptor LitVarStr { get; } = new OpDescriptor(
            OpCode.LitVarStr, 
            new OpDescriptorArg(OpArgType.Variable, OpArgValueType.String), 
            new OpDescriptorArg(OpArgType.Constant, OpArgValueType.String));

        public static OpDescriptor SetLastSystemError { get; } = new OpDescriptor(
            OpCode.SetLastSystemError);

        /// <summary>
        /// Convert to Variant from Currency.
        /// </summary>
        public static OpDescriptor CVarCy { get; } = new OpDescriptor(
            OpCode.CVarCy);

        /// <summary>
        /// Pops an Integer off the stack and stores it within the specified Integer variable.
        /// </summary>
        public static OpDescriptor FStI2 { get; } = new OpDescriptor(
            OpCode.FStI2, 
            new OpDescriptorArg(OpArgType.Variable, OpArgValueType.Integer));

        /// <summary>
        /// An Integer is pushed to the stack.
        /// </summary>
        public static OpDescriptor LitI2 { get; } = new OpDescriptor(
            OpCode.LitI2, 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.Integer));

        /// <summary>
        /// An Integer is pushed to the stack from a provided Byte.
        /// </summary>
        public static OpDescriptor LitI2_Byte { get; } = new OpDescriptor(
            OpCode.LitI2_Byte, 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.Byte));

        /// <summary>
        /// A Long is pushed to the stack.
        /// </summary>
        public static OpDescriptor LitI4 { get; } = new OpDescriptor(
            OpCode.LitI4, 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.Long));

        /// <summary>
        /// A Currency is pushed to the stack.
        /// </summary>
        public static OpDescriptor LitCy { get; } = new OpDescriptor(
            OpCode.LitCy, 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.Currency));

        /// <summary>
        /// A Date variable is declared and initialized with an Integer which is then placed on the stack.
        /// </summary>
        public static OpDescriptor LitDate { get; } = new OpDescriptor(
            OpCode.LitDate, 
            new OpDescriptorArg(OpArgType.Inline, OpArgValueType.Date));

        /// <summary>
        /// Pops an Integer value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1I2 { get; } = new OpDescriptor(
            OpCode.CUI1I2);

        /// <summary>
        /// Pops a Long value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1I4 { get; } = new OpDescriptor(
            OpCode.CUI1I4);

        /// <summary>
        /// Pops a Single value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1R4 { get; } = new OpDescriptor(
            OpCode.CUI1R4);

        /// <summary>
        /// Pops a Double value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1R8 { get; } = new OpDescriptor(
            OpCode.CUI1R8);

        /// <summary>
        /// Pops a Currency value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1Cy { get; } = new OpDescriptor(
            OpCode.CUI1Cy);

        /// <summary>
        /// Pops a Variant value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1Var { get; } = new OpDescriptor(
            OpCode.CUI1Var);

        /// <summary>
        /// Pops a String value off the stack and converts it to a Byte value.
        /// </summary>
        public static OpDescriptor CUI1Str { get; } = new OpDescriptor(
            OpCode.CUI1Str);

        /// <summary>
        /// Pops a Byte value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2UI1 { get; } = new OpDescriptor(
            OpCode.CI2UI1);

        /// <summary>
        /// Pops a Long value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2I4 { get; } = new OpDescriptor(
            OpCode.CI2I4);

        /// <summary>
        /// Pops a Single value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2R4 { get; } = new OpDescriptor(
            OpCode.CI2R4);

        /// <summary>
        /// Pops a Double value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2R8 { get; } = new OpDescriptor(
            OpCode.CI2R8);

        /// <summary>
        /// Pops a Currency value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2Cy { get; } = new OpDescriptor(
            OpCode.CI2Cy);

        /// <summary>
        /// Pops a Variant value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2Var { get; } = new OpDescriptor(
            OpCode.CI2Var);

        /// <summary>
        /// Pops a String value off the stack and converts it to an Integer value.
        /// </summary>
        public static OpDescriptor CI2Str { get; } = new OpDescriptor(
            OpCode.CI2Str);

        /// <summary>
        /// Pops a Byte value off the stack and converts it to an Long value.
        /// </summary>
        public static OpDescriptor CI4UI1 { get; } = new OpDescriptor(
            OpCode.CI4UI1);

        /// <summary>
        /// Pops a Single value off the stack and converts it to an Long value.
        /// </summary>
        public static OpDescriptor CI4R4 { get; } = new OpDescriptor(
            OpCode.CI4R4);

        /// <summary>
        /// Pops a Double value off the stack and converts it to an Long value.
        /// </summary>
        public static OpDescriptor CI4R8 { get; } = new OpDescriptor(
            OpCode.CI4R8);

        /// <summary>
        /// Pops a Currency value off the stack and converts it to an Long value.
        /// </summary>
        public static OpDescriptor CI4Cy { get; } = new OpDescriptor(
            OpCode.CI4Cy);

        /// <summary>
        /// Pops a Variant value off the stack and converts it to an Long value.
        /// </summary>
        public static OpDescriptor CI4Var { get; } = new OpDescriptor(
            OpCode.CI4Var);

        /// <summary>
        /// Pops a String value off the stack and converts it to an Long value.
        /// </summary>
        public static OpDescriptor CI4Str { get; } = new OpDescriptor(
            OpCode.CI4Str);

        /// <summary>
        /// Pops a Byte variable off the stack and stores it in the specified variable.
        /// </summary>
        public static OpDescriptor FStUI1 { get; } = new OpDescriptor(
            OpCode.FStUI1, 
            new OpDescriptorArg(OpArgType.Variable, OpArgValueType.Byte));

        /// <summary>
        /// Pops a Variant off the stack and stores it in the specified variable.
        /// </summary>
        public static OpDescriptor FStVar { get; } = new OpDescriptor(
            OpCode.FStVar, 
            new OpDescriptorArg(OpArgType.Variable, OpArgValueType.Variant));


        static OpDescriptor FromIdImpl(OpCode id)
        {
            return id switch
            {
                OpCode.ExitProcHresult => ExitProcHresult,
                OpCode.ExitProc => ExitProc,
                OpCode.LitStr => LitStr,
                OpCode.LitVarI2 => LitVarI2,
                OpCode.ConcatStr => ConcatStr,
                OpCode.LitVarStr => LitVarStr,
                OpCode.SetLastSystemError => SetLastSystemError,
                OpCode.CVarCy => CVarCy,
                OpCode.FStI2 => FStI2,
                OpCode.LitI2 => LitI2,
                OpCode.LitI2_Byte => LitI2_Byte,
                OpCode.LitI4 => LitI4,
                OpCode.LitCy => LitCy,
                OpCode.LitDate => LitDate,
                OpCode.CUI1I2 => CUI1I2,
                OpCode.CUI1I4 => CUI1I4,
                OpCode.CUI1R4 => CUI1R4,
                OpCode.CUI1R8 => CUI1R8,
                OpCode.CUI1Cy => CUI1Cy,
                OpCode.CUI1Var => CUI1Var,
                OpCode.CUI1Str => CUI1Str,
                OpCode.CI2UI1 => CI2UI1,
                OpCode.CI2I4 => CI2I4,
                OpCode.CI2R4 => CI2R4,
                OpCode.CI2R8 => CI2R8,
                OpCode.CI2Cy => CI2Cy,
                OpCode.CI2Var => CI2Var,
                OpCode.CI2Str => CI2Str,
                OpCode.CI4UI1 => CI4UI1,
                OpCode.CI4R4 => CI4R4,
                OpCode.CI4R8 => CI4R8,
                OpCode.CI4Cy => CI4Cy,
                OpCode.CI4Var => CI4Var,
                OpCode.CI4Str => CI4Str,
                OpCode.FStUI1 => FStUI1,
                OpCode.FStVar => FStVar,
                _ => Invalid,
            };
        }

    }

}
