#include <iostream>
#include <locale> // Ïîäêëþ÷åíèå ðóññêîãî ÿçûêà
#include <cmath> // Äëÿ sqrt
 
using namespace std; 
 
int main()
{
   setlocale (LC_ALL, "russian"); // Ðóññêèé ÿçûê
 
   float radius, l, s;
   const float pi = 3.14;
 
   cin >> radius;
 
   l = 2 * pi * radius; // Äëèíà
   cout << l << endl;
 
   s = pi * radius * radius; // Ïëîùàäü
   cout << s << endl;
 
   return 0;
}