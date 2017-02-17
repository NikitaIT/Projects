/**
 *  @author Nikita Fiodorov
 *  @site   https://github.com/NikitaIT/
 *  @date   01.02.2017
 *  @price  300
 *  @build  g++ -std=c++11 main.cpp 
 *	@run    a.exe "int a; bool qw[12],ff; char c[12];"
 */

#include <iostream>
#include <cctype>
#include <iostream>
#include <sstream>
#include <string>
using namespace std;

int number_value;                 // Хранит целый литерал или литерал с плавающей запятой.
string string_value;            // Хранит имя.
int no_of_errors;                    // Хранит количество встречаемых ошибок.
double expr(std::istream*, bool);    // Обязательное объявление.

/****************************************************************************/

// Функция error() имеет тривиальный характер: инкрементирует счётчик ошибок.
double error(const std::string& error_message) {
    ++no_of_errors;
    std::cerr<<std::endl << "error: " << error_message << std::endl;
    return 1;
}
double info(const std::string& info_message) {
    std::cout<<std::endl << "info: " << info_message << std::endl;
    return 1;
}
/*
 * Проверка на тип
 * 1 - да 0 - нет
 */
bool is_type(string string_value){
    return    string_value =="bool"
            || string_value == "int"
            || string_value == "double"
            || string_value == "char";
}
/*
 * Проверка на число(целое)
 * 0 - да 1 - нет, ошибка
 */
int read_num(std::istream* input){
    char ch;
    if (!input->get(ch)) {
        error("read_num: !input->get(ch)");
        return 1;
    }
    if (isdigit(ch)) {
        input->putback(ch); // Положить назад в поток
        *input >> number_value; // И считать все число
        info("NOMBER "+ to_string(number_value));
        return 0;
    }
    input->putback(ch);
    error("read_num: !isdigit(ch)");
    return 1;
}
/*
 * Проверка на слово начинающееся не с цифры
 * 0 - да 1 - нет, ошибка
 */
int get_word(std::istream* input){
    char ch;
    input->get(ch);
    if (!isalpha(ch)) {
        //input->putback(ch);
        error("get_word: !isalpha(ch)");
        return 1;
    }
    string_value = ch;
    //isalnum - буква или цифра
    while (input->get(ch) && isalnum(ch)) {
        string_value.push_back(ch);
    }
    info("WORD "+string_value);
    input->putback(ch);
    return 0;
}
/*
 * Проверка на любое число пробелов > 1
 * 0 - да 1 - нет, ошибка
 */
int get_space(std::istream* input){
    char ch;
    do {    // Пропустить все пробельные символы кроме '\n'.
        if (!input->get(ch)) {
            error("get_space: !input->get(ch)");
            return 1; // Завершить работу калькулятора.
        }
    } while (ch != '\n' &&isspace(ch));
    info("SPACES");
    input->putback(ch);
    return 0;
}
/*
 * Проверка конструкции 'a*'+'_*'+','+ 'ds*'+ '['+'123*'+']'
 * 0 - да 1 - нет, ошибка
 */
int process_name(std::istream* input){
    char ch;
    //имя перем
    if(get_word(input)){return 1;}
    if (!input->get(ch)) {
        error("process_name: !input->get(ch)");
        return 1; // Завершить работу калькулятора.
    }
    switch(ch){
    case '[': {
        info(" '[' ");
        if(read_num(input)){return 1;}
        if (!input->get(ch)) {
            error("process_name: !input->get(ch)");
            return 1; // Завершить работу калькулятора.
        }
        if(ch!=']'){error("process_name: ch!=']'");return 1;}
        info(" ']' ");
        break;
    }
    default: {input->putback(ch);};
    }
    if(get_space(input)){return 1;}
    if (!input->get(ch)) {
        error("process_name: !input->get(ch)");
        return 1; // Завершить работу калькулятора.
    }
    if(ch==','){
        info(" ',' ");
        if(get_space(input)){return 1;}
        if(process_name(input)){return 1;};
    } else input->putback(ch);
    return 0;
}
/*
 * Проверка типа и вкл process_name
 * 0 - да 1 - нет, ошибка
 */
int process_type(std::istream* input) {

    if(get_word(input)){return 1;}
    if(!is_type(string_value)){
        error("process_type: !is_type(string_value) string_value == "+string_value);
        return 1;
    }
    info("TYPE " + string_value);
    if(get_space(input)){return 1;}
    if(process_name(input)){return 1;}
    char ch;
    if (!input->get(ch)) {
        error("process_type: !input->get(ch)");
        return 1; // Завершить работу калькулятора.
    }
    if(ch==';'){
        return 0;
    }
    error("process_type: ch==';'");
    return 1;
}
/*
 * Поглощение выражений разделённых ";" (верных для process_type) в рекурсии
 * 0 - да 1 - нет, ошибка
 */
int process_start(std::istream* input){
    get_space(input);
    char ch;
    if (!input->get(ch)) {
        error("f: !input->get(ch)");
        return 1; // Завершить работу калькулятора.
    }
    switch (ch) {
    case '0': {return 0;}
    case ';':{ return process_start(input);}
    case '\n': { return process_start(input);}
    default: {
        input->putback(ch);
        if(process_type(input)){
            cout<<"Not add"<<endl;
        }else {cout<<"add"<<endl;}
        return process_start(input);
    }
    }
}
int main(int argc, char* argv[]) {

    std::istream* input = nullptr; // Указатель на поток.
    //если в консоли переданы аргументы, то брать их
    switch (argc) {
    case 1:
        input = &std::cin;
        break;
    case 2:
        input = new std::istringstream(argv[1]);
        break;
    default:
        error("Too many arguments");
        return 1;
    }

    process_start(input);
    if (input != &std::cin) {
        delete input;
    }

    return no_of_errors;
}
