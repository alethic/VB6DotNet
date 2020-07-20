using VBDotNet6.PCode;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Provides read-only access to a PCodeInstructionValue.
    /// </summary>
    public abstract class PCodeInstruction
    {

        public abstract PCodeOp Op { get; }

    }

}
