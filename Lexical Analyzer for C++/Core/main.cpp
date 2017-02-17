/**
 *  @author Nikita Fiodorov
 *  @site https://github.com/NikitaIT/
 *  @date 17.02.2017
 *  @price 800
 *  @build g++ -std=c++11 main.cpp
 */

#include <conio.h>
#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <map>
using namespace std;
/**
 *  10 – ключевое слово;
 *  20 – разделитель; знак
 *  30 – идентификатор; имя
 *  40 – константа. цифра
 */
enum{KEY_WORD=10,SEPARATOR=20,IDENTIFIER=30,VALUE_CONSTANT=40};
/**
 *  Вспомогательные константы
 *  0  - ошибки
 *  -1 - конец файла
 *  -2 - перенос строки
 *  -3 - неопределен(дерективы)
 */
enum{ERROR=0,END_OF_FILE=-1,CR=-2,NON=-3};

static map<string,int> key_word;
static map<string,int> separator;
static map<string,int> identifier;
static map<string,int> value_constant;
void init(){
    //без нуля
    key_word["int"] = 1;
    key_word["float"] = 2;
    key_word["bool"] = 3;
    key_word["void"] = 4;
    key_word["main"] = 5;
    key_word["using"] = 6;
    key_word["namespace"] = 7;
    key_word["return"] = 8;
    key_word["if"] = 9;
    //key_word["std"] = ;
    key_word["cin"] = 12;
    key_word["cout"] = 13;
    key_word["endl"] = 14;
    key_word["cin"] = 15;
    key_word["const"] = 11;
    key_word["for"] = 10;
    //высер
    key_word["include"] = 1;
    key_word["define"] = 2;

    separator["{"]=1;
    separator["}"]=2;
    separator["("]=3;
    separator[")"]=4;
    separator[";"]=5;
    separator["<"]=6;
    separator[">"]=7;
    separator["="]=8;
    separator["=="]=9;
    separator["!="]=10;

    separator["*"]=11;
    separator["/"]=12;
    separator["+"]=13;
    separator["-"]=14;
    separator[">>"]=15;
    separator["<<"]=16;
    separator[","]=17;
}

//  с++11 to_string(T)
template <typename T> std::string toString(T val)
{
    std::ostringstream oss;
    oss<< val;
    return oss.str();
}

//  Для временных имён переменных
char buffer[1024];
/**
 *  Проверка одной лексемы
 *  @return (<индекс_таблицы>,<значение_в_таблице>)
 */
pair<int,int> get_lexem(FILE* stream, char* buffer, size_t buffer_length)
{
  char ch = getc(stream);
  size_t i;

  /* пропускаем все пробелы, табуляции и символы новой строки */
  while(ch != '\n' &&isspace(ch))
    ch = getc(stream);

  /* пропускаем все комментарии */
  if(ch == '/'){
      ch = getc(stream);
      if(ch=='/') while(ch != '\n')ch = getc(stream);
      ungetc(ch, stream);
  }
  /**/
  if(ch == '#'){
      i = 0;
      while((ch = getc(stream)) != EOF && (isalpha(ch) || isdigit(ch) || ch == '_')){
          buffer[i++] = ch;
      }

      if((ch != EOF)||(ch=='\n'))
        ungetc(ch, stream);
      buffer[i] = '\0';
      if(key_word[buffer]){return std::make_pair(NON, NON);}
  }
  /* отрисовка переносов*/
  if(ch=='\n')return std::make_pair(CR, CR);

  /* конец файла */
  if(ch == EOF)
    return std::make_pair(END_OF_FILE, END_OF_FILE);

  /* сложный случаай 1: < или << */
  if(ch == '<') {
    ch = getc(stream);
    if(ch == '<') return std::make_pair(SEPARATOR, separator["<<"]);
    ungetc(ch, stream);
    return std::make_pair(SEPARATOR, separator["<"]);
  }
  /* сложный случаай 2: > или >> */
  if(ch == '>') {
    ch = getc(stream);
    if(ch == '>') return std::make_pair(SEPARATOR, separator[">>"]);
    ungetc(ch, stream);
    return std::make_pair(SEPARATOR, separator[">"]);
  }
  /* сложный случаай 3: = или == */
  if(ch == '=') {
    ch = getc(stream);
    if(ch == '=') return std::make_pair(SEPARATOR, separator["=="]);
    ungetc(ch, stream);
    return std::make_pair(SEPARATOR, separator["="]);
  }
  /* простые односимвольные лексемы */
  if(separator[toString(ch)])
    return std::make_pair(SEPARATOR, separator[toString(ch)]);

  /* сложный случай 4: != */
  if(ch == '!') {
    ch = getc(stream);
    if(ch == '=') return std::make_pair(SEPARATOR, separator["!="]);
    /* по грамматике сразу после ! обязан идти символ = */
    /* если это не так, в лексеме ошибка */
    return std::make_pair(ERROR, ERROR);
  }

  /* сложный случай 5: идентификатор или ключевое слово */
  if(isalpha(ch) || ch == '_') {
    /* по правилам языка С, переменные именуются с буквы или нижнего слеша */
    i = 0;
    do {
      buffer[i++] = ch;
    } while((ch = getc(stream)) != EOF && (isalpha(ch) || isdigit(ch) || ch == '_'));

    if(ch != EOF)
      ungetc(ch, stream);

    buffer[i] = '\0';

    if(key_word[buffer]) return std::make_pair(KEY_WORD, key_word[buffer]);

    return std::make_pair(IDENTIFIER, identifier[buffer]);
  }

  /* сложный случай 6: число */
  if(isdigit(ch)) {
      i = 0;
      do {
        buffer[i++] = ch;
      } while((ch = getc(stream)) != EOF && (isdigit(ch)||ch=='.'));

      if(ch != EOF)
        ungetc(ch, stream);

      buffer[i] = '\0';
    value_constant[buffer] = value_constant.size();
    return std::make_pair(VALUE_CONSTANT, value_constant[buffer]);
  }
  /* сложный случай 7: строка */
  if(ch=='"') {
    i = 0;

    while((ch = getc(stream)) != EOF && (ch!='"')&& (ch!='\n')){
        buffer[i++] = ch;
    }

    if((ch != EOF)||(ch=='\n'))
      ungetc(ch, stream);
    buffer[i] = '\0';

    value_constant[buffer] = value_constant.size();
    return std::make_pair(VALUE_CONSTANT, value_constant[buffer]);
  }
    return std::make_pair(ERROR, ERROR);
}


int main()
{
    init();
    FILE * pFile = fopen ("sample.cpp","r");
    if (pFile==NULL) {
        cout<<pFile<<" ooo";
        getch();
        return 0;
    }

    int a;
    cout<<"print in file(1) or console(2)"<<endl;
    cin>>a;
    ofstream fout("decode.txt");
    cout<<endl;
    pair<int,int> lex(-1,-1);
    for(;;)
    {
        lex = get_lexem(pFile,buffer,1000);
        if(lex.first==ERROR) {
            cout<<"error code: "<<lex.first;
            break;
        }
        if(lex.first==END_OF_FILE)break;

        if(a==1){
            if(lex.first==CR){fout <<"\n";continue;}
            fout <<"("<< lex.first<<";"<<lex.second<<")";
        }
        if(a!=1){
            if(lex.first==CR){cout <<endl;continue;}
            cout <<"("<< lex.first<<";"<<lex.second<<")";
        }
    }
    fclose (pFile);
    fout.close();
    return 0;
}
