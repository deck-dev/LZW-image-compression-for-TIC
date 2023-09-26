namespace image2LZW.Scanner
{
    public class LuaToken
    {
        public enum TokenType
        {
            // Comment
            COMMENT,

            // Single-character tokens.
            LEFT_PAREN, RIGHT_PAREN,
            LEFT_BRACE, RIGHT_BRACE,
            LEFT_BRACKET, RIGHT_BRACKET,
            COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,
            HASH, CIRCUMFLEX, PERCENT, COLON,

            // One or two character tokens.
            BANG, BANG_EQUAL,
            EQUAL, EQUAL_EQUAL,
            GREATER, GREATER_EQUAL,
            LESS, LESS_EQUAL,

            // Literals
            IDENTIFIER,
            STRING,
            NUMBER,
            CHAR,

            // Keywords
            KEYWORD
        }

        public TokenType Type { get; set; }
        public string Literal { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public override string ToString() => $"[{Type}] {Literal}";
    }
}
