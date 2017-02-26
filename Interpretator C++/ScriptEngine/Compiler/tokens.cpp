#include <map>
using namespace std;
namespace ScriptEngine {
namespace Compiler
{

    enum Token
{
    NotAToken=0,

    // structure
    VarDef,
    Procedure,
    EndProcedure,
    Function,
    EndFunction,
    If,
    Then,
    Else,
    ElseIf,
    EndIf,
    For,
    While,
    Each,
    To,
    In,
    Loop,
    EndLoop,
    Break,
    Continue,
    Return,
    Try,
    Exception,
    RaiseException,
    EndTry,
    EndOfText,
    Export,

    // operators
    Plus,
    Minus,
    UnaryMinus,
    Multiply,
    Division,
    Modulo,
    Equal,
    MoreThan,
    LessThan,
    MoreOrEqual,
    LessOrEqual,
    NotEqual,
    And,
    Or,
    Not,
    Dot,
    OpenPar,
    ClosePar,
    OpenBracket,
    CloseBracket,
    NewObject,

    // special chars
    Comma,
    StringQuote,
    DateQuote,
    Semicolon,
    Question,

    // modifiers
    ByValParam,

    // built-in functions
    // must be declared last
    Bool,
    Number,
    Str,
    Date,
    Type,
    ValType,
    StrLen,
    TrimL,
    TrimR,
    TrimLR,
    Left,
    Right,
    Mid,
    StrPos,
    UCase,
    LCase,
    TCase,
    Chr,
    ChrCode,
    EmptyStr,
    StrReplace,
    StrGetLine,
    StrLineCount,
    StrEntryCount,
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second,
    BegOfYear,
    BegOfMonth,
    BegOfDay,
    BegOfHour,
    BegOfMinute,
    BegOfQuarter,
    EndOfYear,
    EndOfMonth,
    EndOfDay,
    EndOfHour,
    EndOfMinute,
    EndOfQuarter,
    WeekOfYear,
    DayOfYear,
    DayOfWeek,
    AddMonth,
    CurrentDate,
    Integer,
    Round,
    Log,
    Log10,
    Sin,
    Cos,
    Tan,
    ASin,
    ACos,
    ATan,
    Exp,
    Pow,
    Sqrt,
    Min,
    Max,
    Format,

    ExceptionInfo,
    ExceptionDescr,
    ModuleInfo,
    End
};

    class LanguageDef
    {

        /************************************************************************/
        /* Comparator for case-insensitive comparison in STL assos. containers  */
        /************************************************************************/
        struct ci_less : std::binary_function<std::string, std::string, bool>
        {
            // case-independent (ci) compare_less binary function
            struct nocase_compare : public std::binary_function<unsigned char,unsigned char,bool>
            {
              bool operator() (const unsigned char& c1, const unsigned char& c2) const {
                  return tolower (c1) < tolower (c2);
              }
            };
            bool operator() (const std::string & s1, const std::string & s2) const {
              return std::lexicographical_compare
                (s1.begin (), s1.end (),   // source range
                s2.begin (), s2.end (),   // dest range
                nocase_compare ());  // comparison
            }
          };//end of ci_less
    private:
        static map<Token, int> _priority;//= new map<Token, int>();
        static map<string, Token,ci_less> _stringToToken; //= new map<string, Token,ci_less>();
        static const int BUILTINS_INDEX = (int)Token::ByValParam;
        static const int END_INDEX = (int)Token::End;

        // structure
        LanguageDef()
        {
            _priority[Token::Plus]=5;
            _priority[Token::Minus]=5;
            _priority[Token::UnaryMinus]=5;
            _priority[Token::Multiply]=6;
            _priority[Token::Division]=6;
            _priority[Token::Modulo]=6;

            _priority[Token::Or]=1;
            _priority[Token::And]=2;
            _priority[Token::Not]=3;

            _priority[Token::Equal]=4;
            _priority[Token::MoreThan]=4;
            _priority[Token::LessThan]=4;
            _priority[Token::MoreOrEqual]=4;
            _priority[Token::LessOrEqual]=4;
            _priority[Token::NotEqual]=4;

            // tokens

            // Ключевые слова

            AddToken(Token::If, "если", "if");
            AddToken(Token::Then, "тогда", "then");
            AddToken(Token::Else, "иначе", "else");
            AddToken(Token::ElseIf, "иначеесли", "elseif");
            AddToken(Token::EndIf, "конецесли", "endif");
            AddToken(Token::VarDef, "перем", "var");
            AddToken(Token::ByValParam, "знач", "val");
            AddToken(Token::Procedure, "процедура", "procedure");
            AddToken(Token::EndProcedure, "конецпроцедуры", "endprocedure");
            AddToken(Token::Function, "функция", "function");
            AddToken(Token::EndFunction, "конецфункции", "endfunction");
            AddToken(Token::For, "для", "for");
            AddToken(Token::Each, "каждого", "each");
            AddToken(Token::In, "из", "in");
            AddToken(Token::To, "по", "to");
            AddToken(Token::While, "пока", "while");
            AddToken(Token::Loop, "цикл", "do");
            AddToken(Token::EndLoop, "конеццикла", "enddo");
            AddToken(Token::Return, "возврат", "return");
            AddToken(Token::Continue, "продолжить", "contınue");
            AddToken(Token::Break, "прервать", "break");
            AddToken(Token::Try, "попытка", "try");
            AddToken(Token::Exception, "исключение", "except");
            // обратная совместимость с beta 1.0
            AddToken(Token::Exception, "exception");
            AddToken(Token::RaiseException, "вызватьисключение", "raise");
            AddToken(Token::EndTry, "конецпопытки", "endtry");
            AddToken(Token::NewObject, "новый", "new");
            AddToken(Token::Export, "экспорт", "export");
            AddToken(Token::And, "и", "and");
            AddToken(Token::Or, "или", "or");
            AddToken(Token::Not, "не", "not");

            // Операторы

            AddToken(Token::Plus, "+");
            AddToken(Token::Minus, "-");
            AddToken(Token::Multiply, "*");
            AddToken(Token::Division, "/");
            AddToken(Token::LessThan, "<");
            AddToken(Token::LessOrEqual, "<=");
            AddToken(Token::MoreThan, ">");
            AddToken(Token::MoreOrEqual, ">=");
            AddToken(Token::NotEqual, "<>");
            AddToken(Token::Modulo, "%");
            AddToken(Token::OpenPar, "(");
            AddToken(Token::ClosePar, ")");
            AddToken(Token::OpenBracket, "[");
            AddToken(Token::CloseBracket, "]");
            AddToken(Token::Dot, ".");
            AddToken(Token::Comma, ",");
            AddToken(Token::Equal, "=");
            AddToken(Token::Semicolon, ";");
            AddToken(Token::Question, "?");

            // Функции работы с типами

            AddToken(Token::Bool, "булево", "boolean");
            AddToken(Token::Number, "число", "number");
            AddToken(Token::Str, "строка", "string");
            AddToken(Token::Date, "дата", "date");
            AddToken(Token::Type, "тип", "type");
            AddToken(Token::ValType, "типзнч", "typeof");

            //Встроенные функции

            AddToken(Token::StrLen, "стрдлина", "strlen");
            AddToken(Token::TrimL, "сокрл", "triml");
            AddToken(Token::TrimR, "сокрп", "trimr");
            AddToken(Token::TrimLR, "сокрлп", "trimall");
            AddToken(Token::Left, "лев", "left");
            AddToken(Token::Right, "прав", "right");
            AddToken(Token::Mid, "сред", "mid");
            AddToken(Token::StrPos, "найти", "find");
            AddToken(Token::UCase, "врег", "upper");
            AddToken(Token::LCase, "нрег", "lower");
            AddToken(Token::TCase, "трег", "title");
            AddToken(Token::Chr, "символ", "char");
            AddToken(Token::ChrCode, "кодсимвола", "charcode");
            AddToken(Token::EmptyStr, "пустаястрока", "isblankstring");
            AddToken(Token::StrReplace, "стрзаменить", "strreplace");
            AddToken(Token::StrGetLine, "стрполучитьстроку", "strgetline");
            AddToken(Token::StrLineCount, "стрчислострок", "strlinecount");
            AddToken(Token::StrEntryCount, "стрчисловхождений", "stroccurrencecount");
            AddToken(Token::Year, "год", "year");
            AddToken(Token::Month, "месяц", "month");
            AddToken(Token::Day, "день", "day");
            AddToken(Token::Hour, "час", "hour");
            AddToken(Token::Minute, "минута", "minute");
            AddToken(Token::Second, "секунда", "second");
            AddToken(Token::BegOfYear, "началогода", "begofyear");
            AddToken(Token::BegOfMonth, "началомесяца", "begofmonth");
            AddToken(Token::BegOfDay, "началодня", "begofday");
            AddToken(Token::BegOfHour, "началочаса", "begofhour");
            AddToken(Token::BegOfMinute, "началоминуты", "begofminute");
            AddToken(Token::BegOfQuarter, "началоквартала", "begofquarter");
            AddToken(Token::EndOfYear, "конецгода", "endofyear");
            AddToken(Token::EndOfMonth, "конецмесяца", "endofmonth");
            AddToken(Token::EndOfDay, "конецдня", "endofday");
            AddToken(Token::EndOfHour, "конецчаса", "endofhour");
            AddToken(Token::EndOfMinute, "конецминуты", "endofminute");
            AddToken(Token::EndOfQuarter, "конецквартала", "endofquarter");
            AddToken(Token::WeekOfYear, "неделягода", "weekofyear");
            AddToken(Token::DayOfYear, "деньгода", "dayofyear");
            AddToken(Token::DayOfWeek, "деньнедели", "dayofweek");
            AddToken(Token::AddMonth, "добавитьмесяц", "addmonth");
            AddToken(Token::CurrentDate, "текущаядата", "currentdate");
            AddToken(Token::Integer, "цел", "int");
            AddToken(Token::Round, "окр", "round");
            AddToken(Token::Log, "log");
            AddToken(Token::Log10, "log10");
            AddToken(Token::Sin, "sin");
            AddToken(Token::Cos, "cos");
            AddToken(Token::Tan, "tan");
            AddToken(Token::ASin, "asin");
            AddToken(Token::ACos, "acos");
            AddToken(Token::ATan, "atan");
            AddToken(Token::Exp, "exp");
            AddToken(Token::Pow, "pow");
            AddToken(Token::Sqrt, "sqrt");
            AddToken(Token::Min, "мин", "min");
            AddToken(Token::Max, "макс", "max");
            AddToken(Token::Format, "формат", "format");
            AddToken(Token::ExceptionInfo, "информацияобошибке", "errorinfo");
            AddToken(Token::ExceptionDescr, "описаниеошибки", "errordescription");
            AddToken(Token::ModuleInfo, "текущийсценарий", "currentscript");
        }
    private:
        static void AddToken(Token token, string name)
        {
            _stringToToken[name]= token;
        }

        static void AddToken(Token token, string name, string alias)
        {
            _stringToToken[name]= token;
            _stringToToken[alias]= token;
        }

    public:
        static Token GetToken(string tokText)
        {
            Token result;
            if(result = _stringToToken[tokText])
            {
                return result;
            }
            else
            {
                return Token::NotAToken;
            }
        }

        static int GetPriority(Token op)
        {
            return _priority[op];
        }

        static bool IsBuiltInFunction(Token token)
        {
            return (int)token > BUILTINS_INDEX;
        }

        static bool IsBinaryOperator(Token token)
        {
            return token == Token::Plus
                || token == Token::Minus
                || token == Token::Multiply
                || token == Token::Division
                || token == Token::Modulo
                || token == Token::And
                || token == Token::Or
                || token == Token::Not
                || token == Token::LessThan
                || token == Token::LessOrEqual
                || token == Token::MoreThan
                || token == Token::MoreOrEqual
                || token == Token::Equal
                || token == Token::NotEqual;
        }

        static bool IsLogicalOperator(Token token)
        {
            return token == Token::And || token == Token::Or;
        }

//        static bool IsLiteral(Lexem &lex)
//        {
//            return lex.Type == LexemType.StringLiteral
//                || lex.Type == LexemType.NumberLiteral
//                || lex.Type == LexemType.BooleanLiteral
//                || lex.Type == LexemType.DateLiteral
//                || lex.Type == LexemType.UndefinedLiteral
//                || lex.Type == LexemType.NullLiteral;
//        }

//        static bool IsUserSymbol(Lexem &lex)
//        {
//            return lex.Type == LexemType.Identifier && lex.Token == Token::NotAToken;
//        }

//        static bool IsIdentifier(Lexem &lex)
//        {
//            return lex.Type == LexemType.Identifier;
//        }

        //возвращает все что после BUILTINS_INDEX
        static Token* BuiltInFunctions()
        {
            Token *result = new Token[END_INDEX-BUILTINS_INDEX];
            for (int i = BUILTINS_INDEX + 1, j = 0; i < END_INDEX; i++, j++)
            {
                result[j] = (Token)(BUILTINS_INDEX+i);
            }

            return result;
        }

    };

    class SpecialChars
    {
    public:
         static const char StringQuote = '"';
         static const char DateQuote = '\'';
         static const char EndOperator = ';';
         static const char Directive = '#';

         static bool IsOperatorChar(char symbol)
        {
            switch (symbol)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '<':
                case '>':
                case '=':
                case '%':
                case '(':
                case ')':
                case '.':
                case ',':
                case '[':
                case ']':
                    return true;
                default:
                    return false;

            }
        }

         static bool IsDelimiter(char symbol)
        {
            return !((isdigit(symbol)||isalpha(symbol)) || symbol == '_');
        }

    };


}
}
