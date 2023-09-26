using System;
using System.Collections.Generic;

namespace image2LZW.Scanner
{
    public class LuaScanner
    {
        private int _start;
        private int _cur;
        private string _src;
        private int _line;
        private List<LuaToken> _tokens;

        private readonly string[] _keywords = new[]
        {
            "and",
            "break",
            "do",
            "else",
            "elseif",
            "end",
            "false",
            "for",
            "function",
            "if",
            "in",
            "local",
            "nil",
            "not",
            "or",
            "repeat",
            "return",
            "then",
            "true",
            "until",
            "while"
        };

        public ICollection<LuaToken> Scan(string source)
        {
            _tokens = new List<LuaToken>();
            if (string.IsNullOrEmpty(source)) return _tokens;

            _cur = 0;
            _line = 1;
            _src = source;

            while (!IsAtEnd())
            {
                _start = _cur;
                char c = Advance();
                switch (c)
                {
                    case '(': AddToken(LuaToken.TokenType.LEFT_PAREN); break;
                    case ')': AddToken(LuaToken.TokenType.RIGHT_PAREN); break;
                    case '{': AddToken(LuaToken.TokenType.LEFT_BRACE); break;
                    case '}': AddToken(LuaToken.TokenType.RIGHT_BRACE); break;
                    case '[': AddToken(LuaToken.TokenType.LEFT_BRACKET); break;
                    case ']': AddToken(LuaToken.TokenType.RIGHT_BRACKET); break;
                    case '+': AddToken(LuaToken.TokenType.PLUS); break;
                    case '*': AddToken(LuaToken.TokenType.STAR); break;
                    case '/': AddToken(LuaToken.TokenType.SLASH); break;
                    case '#': AddToken(LuaToken.TokenType.HASH); break;
                    case ',': AddToken(LuaToken.TokenType.COMMA); break;
                    case '.': AddToken(LuaToken.TokenType.DOT); break;
                    case ';': AddToken(LuaToken.TokenType.SEMICOLON); break;
                    case ':': AddToken(LuaToken.TokenType.COLON); break;
                    case '^': AddToken(LuaToken.TokenType.CIRCUMFLEX); break;
                    case '%': AddToken(LuaToken.TokenType.PERCENT); break;

                    case '~':
                        AddToken(Match('=') ?
                            LuaToken.TokenType.BANG_EQUAL : LuaToken.TokenType.BANG);
                        break;
                    case '=':
                        AddToken(Match('=') ?
                            LuaToken.TokenType.EQUAL_EQUAL : LuaToken.TokenType.EQUAL);
                        break;
                    case '<':
                        AddToken(Match('=') ?
                            LuaToken.TokenType.LESS_EQUAL : LuaToken.TokenType.LESS);
                        break;
                    case '>':
                        AddToken(Match('=') ?
                            LuaToken.TokenType.GREATER_EQUAL : LuaToken.TokenType.GREATER);
                        break;

                    case '-':
                        if (Match('-')) Comment();
                        else AddToken(LuaToken.TokenType.MINUS);
                        break;

                    case '\r':
                    case '\t':
                    case ' ':
                        // Ignore whitespace
                        break;

                    case '\n':
                        _line++;
                        break;

                    case '"':
                        String();
                        break;

                    case '\'':
                        Char();
                        break;

                    default:
                        if (IsNumber(c)) Number();
                        else if (IsAlpha(c)) Identifier();
                        else Error($"Invalid char {c}");
                        break;
                }
            }
            return _tokens;
        }

        private void Char()
        {
            if (Peek() == '\\') Advance();
            if (PeekNext() == '\'')
            {
                Advance();
                Advance();
                AddToken(LuaToken.TokenType.CHAR);
                return;
            }
            else
            {
                while (Peek() != '\'' && !IsAtEnd())
                {
                    if (Peek() == '\n') _line++;
                    Advance();
                }
                if (IsAtEnd()) Error("Unterminated string");
                Advance();
                AddToken(LuaToken.TokenType.STRING);
            }
        }

        private void String()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n') _line++;
                Advance();
            }
            if (IsAtEnd()) Error("Unterminated string");
            Advance();

            AddToken(LuaToken.TokenType.STRING);
        }

        private void Identifier()
        {
            while (IsAlphaNumeric(Peek())) Advance();

            string literal = _src.Substring(_start, _cur - _start);
            foreach (string keyword in _keywords)
            {
                if (literal.ToLower().Equals(keyword))
                {
                    AddToken(LuaToken.TokenType.KEYWORD);
                    return;
                }
            }

            AddToken(LuaToken.TokenType.IDENTIFIER);
        }

        private void Number()
        {
            while (IsNumber(Peek())) Advance();

            // Look for a fractional part
            if (Peek() == '.' && IsNumber(PeekNext()))
            {
                // Consume the "."
                Advance();

                while (IsNumber(Peek())) Advance();
            }

            AddToken(LuaToken.TokenType.NUMBER);
        }

        private void Comment()
        {
            while (Peek() != '\n' && !IsAtEnd()) Advance();
            AddToken(LuaToken.TokenType.COMMENT);
        }

        private bool IsAlpha(char c) =>
            (c >= 'a' && c <= 'z') ||
            (c >= 'A' && c <= 'Z') ||
             c == '_';

        private bool IsNumber(char c) => c >= '0' && c <= '9';

        private bool IsAlphaNumeric(char c) => IsAlpha(c) || IsNumber(c);

        private char Advance() => _src[_cur++];

        private bool IsAtEnd() => _cur >= _src.Length;

        private bool Match(char c)
        {
            if (IsAtEnd()) return false;
            if (_src[_cur] != c) return false;
            _cur++;
            return true;
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return _src[_cur];
        }

        private char PeekNext()
        {
            if (_cur + 1 >= _src.Length) return '\0';
            return _src[_cur + 1];
        }

        private void AddToken(LuaToken.TokenType type)
        {
            int length = _cur - _start;
            var token = new LuaToken()
            {
                Type = type,
                Literal = _src.Substring(_start, length),
                Start = _start,
                Length = length
            };
            _tokens.Add(token);
        }

        private void Error(string message)
        {
            string msg = $"{message} in line {_line} position {_cur}";
            throw new Exception(msg);
        }
    }
}
