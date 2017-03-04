#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include <list>
#include <algorithm>

/**
 *  @author Nikita Fiodorov
 *  @site https://github.com/NikitaIT/
 *  @date 04.03.2017
 *  @price 125
 *  @build g++ -std=c++11 main.cpp
 */

/*!
 * \a Student _____ Group №_____
 * \date 04.03.2017
 */

/*!
 * \brief endOfProposals    Проверка на конец предложения
 * \param ch                Проверяемый смвол
 * \return 1                если символ конца предложения, 0 в пративном случае
 */
bool endOfProposals(char ch);

/*!
 * \brief extractProposals  Извлечь предложение
 * \param docin             Файл содержащий текст
 * \return                  Предложение
 */
std::string extractProposals(std::ifstream &docin);

/*!
 * \brief openDoc           Открыть файл исходного текста
 * \return                  Указатель на этот файл или nullptr если файл не найден
 */
std::ifstream * openDoc();

/*!
 * \brief buildProposalsList    Построение обращенного списка
 * \param docin                 Файл исходного текста
 * \param count                 Число считываемых строк
 * \return                      Список строк наоборот
 */
std::list<std::string> buildProposalsList(std::ifstream *docin, size_t count);

int main()
{
    //открываем файл
    auto docin = openDoc();
    if(docin==nullptr) return 0;
    //задаем колличество строк
    size_t count = 3;
    //строим список строк
    std::list<std::string> reverseProposalsList = buildProposalsList(docin, count);
    //выводим список в консоль
    std::for_each(reverseProposalsList.begin(),
                  reverseProposalsList.end(),
                  [](std::string p){ std::cout<< p; });
    getchar();
    return 0;
}

bool endOfProposals(char ch)
{
    return (ch == '.' || ch == '!' || ch == '?');
}

std::string extractProposals(std::ifstream &docin)
{
    char ch=' ';
    std::stringstream proposal;
    //пока не конец файла или предложения забераем символ
    while (!docin.eof()&&!endOfProposals(ch)) {
        docin.get(ch);
        proposal.put(ch);
    }
    return proposal.str();
}

std::ifstream * openDoc()
{
    std::string filepath = "C:/test.txt";
    std::cout<<"Enter the file name: ";
    std::cin>>filepath;
    std::ifstream *docin = new std::ifstream(filepath);
    if(!docin->is_open())
    {
        std::cout << "Error opening the file." << std::endl;
        getchar();
        docin = nullptr;
    }

    return docin;
}

std::list<std::string> buildProposalsList(std::ifstream *docin, size_t count)
{
    std::list<std::string> proposals;
    for(size_t i(0);i<count;i++){
        proposals.push_front(extractProposals(*docin));
    }
    return proposals;
}
