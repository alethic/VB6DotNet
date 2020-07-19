using System.Collections.Generic;
using System.IO;

using Antlr4.Runtime;

namespace VB6DotNet
{

    public class VisualBasic6Parser : IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
    {

        /// <summary>
        /// Visits the ANTLR AST and transforms it to a local AST.
        /// </summary>
        class Visitor : ANTLR.VisualBasic6BaseVisitor<AST.Module>
        {



        }

        readonly Visitor visitor = new Visitor();
        readonly List<AST.Module> modules = new List<AST.Module>();

        /// <summary>
        /// Gets the currently parsed modules.
        /// </summary>
        public IReadOnlyList<AST.Module> Modules => modules;

        /// <summary>
        /// Parses a VB6 input file.
        /// </summary>
        /// <param name="input"></param>
        public void Parse(Stream input)
        {
            Parse(CharStreams.fromStream(input));
        }

        /// <summary>
        /// Parses a VB6 input file.
        /// </summary>
        /// <param name="input"></param>
        public void Parse(TextReader input)
        {
            Parse(CharStreams.fromTextReader(input));
        }

        /// <summary>
        /// Parses a VB6 input file.
        /// </summary>
        /// <param name="input"></param>
        void Parse(ICharStream input)
        {
            // configure lexer
            var l = new ANTLR.VisualBasic6Lexer(input);
            l.RemoveErrorListeners();
            l.AddErrorListener(this);

            // configure parser
            var p = new ANTLR.VisualBasic6Parser(new CommonTokenStream(l));
            p.RemoveErrorListeners();
            p.AddErrorListener(this);

            modules.Add(visitor.Visit(p.startRule()));
        }

        void IAntlrErrorListener<int>.SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw e;
        }

        void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw e;
        }

    }

}
