#include <string>
#include <list>
using namespace std;
namespace ScriptEngine {
namespace Compiler
{
// CurrentSymbol{get} -> GetCurrentSymbol()
//CurrentLine{get} ->GetCurrentLine()
class ParseIterator
{
private:
     int _index;
     int _startPosition;
     char _currentSymbol;
     string _code;
     int _lineCounter = 1;
     list<int> _lineBounds;
public:
     ParseIterator(string code)
    {
        _code = code;
        _index = 0;
        _startPosition = 0;
        //_lineBounds = new list<int>();
        _lineBounds.push_back(0);// first line

        if (_code.length() > 0)
        {
            _currentSymbol = _code[0];
        }
        else
            _currentSymbol = '\0';
    }
    // CurrentSymbol{get} -> GetCurrentSymbol()
     char GetCurrentSymbol()
    {

            return _currentSymbol;

    }

     Word GetContents()
    {
        return GetContents(0, 0);
    }
    //CurrentLine{get} ->GetCurrentLine()
     int GetCurrentLine()
    {

            return _lineCounter;

    }

     string GetCodeLine(int index)
    {
        auto idx = GetCodeIndexer();
        return idx.GetCodeLine(index);
    }

     SourceCodeIndexer GetCodeIndexer()
    {
        return new SourceCodeIndexer(_code, _lineBounds);
    }

     CodePositionInfo GetPositionInfo(int lineNumber)
    {
        auto cp = new CodePositionInfo();
        cp.LineNumber = lineNumber;
        cp.Code = GetCodeLine(lineNumber);
        return cp;
    }

     Word GetContents(int padLeft, int padRight)
    {
        int len;

        if (_startPosition == _index && _startPosition < _code.length())
        {
            len = 1;
        }
        else if (_startPosition < _index)
        {
            len = _index - _startPosition;
        }
        else
        {
            return new Word() { start = -1 };
        }
        //_code.Substring();
        auto contents = _code.substr(_startPosition + padLeft, len - padRight);
        auto word = new Word() { start = _startPosition, content = contents };

        _startPosition = _index + 1;

        return word;
    }

     bool MoveNext()
    {
        _index++;
        if (_index < _code.Length)
        {
            _currentSymbol = _code[_index];
            if (_currentSymbol == '\n')
            {
                _lineCounter++;
                _lineBounds.push_back(_index + 1);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

     bool MoveBack()
    {
        _index--;
        if (_index >= 0)
        {
            _currentSymbol = _code[_index];
            if (_currentSymbol == '\n')
            {
                _lineCounter--;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

     bool MoveToContent()
    {
        if (SkipSpaces())
        {
            _startPosition = _index;
            return true;
        }
        else
        {
            return false;
        }
    }

     bool SkipSpaces()
    {
        while (isspace(_currentSymbol))
        {
            if (!MoveNext())
            {
                return false;
            }
        }

        if (_index >= _code.length())
        {
            return false;
        }

        return true;
    }
}
}
}
