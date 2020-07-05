using System.IO;
using System.Text;

namespace Exa.Debugging.Commands.Parser
{
    /// <summary>
    /// Breaks the user input into chunks that can be processed by other objects
    /// Get type of value with .Token
    /// Get value with .Type
    /// Go to the next chunk with NextToken()
    /// </summary>
    public sealed class Tokenizer
    {
        private int iterator;
        private readonly TextReader reader;
        private char currentChar;

        public Tokenizer(TextReader reader)
        {
            this.reader = reader;
            NextChar();
            NextToken();
        }

        public Token Token { get; private set; }
        public string Value { get; private set; }

        private void NextChar()
        {
            int ch = reader.Read();
            currentChar = ch < 0 ? '\0' : (char)ch;
            iterator++;
        }

        public void NextToken()
        {
            // Skip whitespace
            while (char.IsWhiteSpace(currentChar))
                NextChar();

            // Catch EOF
            if (currentChar == '\0')
            {
                Token = Token.EOF;
                Value = "";
                return;
            }

            var sb = new StringBuilder();

            // Catch literal
            if (char.IsLetter(currentChar))
            {
                while (char.IsLetter(currentChar))
                {
                    sb.Append(currentChar);
                    NextChar();
                }

                Token = Token.Literal;
                Value = sb.ToString();
                return;
            }

            // Catch number
            if (char.IsDigit(currentChar) || currentChar == '.')
            {
                // Capture digits/decimal point
                bool haveDecimalPoint = false;
                while (char.IsDigit(currentChar) || (!haveDecimalPoint && currentChar == '.'))
                {
                    sb.Append(currentChar);
                    haveDecimalPoint = currentChar == '.';
                    NextChar();
                }

                Token = Token.Number;
                Value = sb.ToString();
                return;
            }

            // Catch key and bool
            if (currentChar == '-')
            {
                NextChar();

                // Catch key
                if (currentChar == '-')
                {
                    Token = Token.Key;
                    NextChar();
                }
                // Catch bool
                else
                {
                    Token = Token.Flag;
                }

                while (char.IsLetter(currentChar) || currentChar == '-')
                {
                    sb.Append(currentChar);
                    NextChar();
                }

                Value = sb.ToString();
                return;
            }

            // Catch string
            if (currentChar == '"')
            {
                NextChar();

                var hasClosedParenthesis = false;
                while (currentChar != '\0')
                {
                    if (currentChar == '"')
                    {
                        hasClosedParenthesis = true;
                        NextChar();
                        break;
                    }

                    sb.Append(currentChar);
                    NextChar();
                }

                if (!hasClosedParenthesis)
                {
                    throw new IncorrectCommandFormatException("String not closed");
                }

                Token = Token.String;
                Value = sb.ToString();
                return;
            }

            throw new IncorrectCommandFormatException($"Incorrect format at position {iterator}");
        }
    }
}