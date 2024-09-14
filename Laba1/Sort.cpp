#include <iostream>
#include <string>
#include <ctime>
#include <vector>
#include <chrono>
#include <random>
#include <utility>  
#include <fstream>
using namespace std;
const int VALUES_COUNT = 8;
int TESTS_LIMIT = 20;
const int TEST_VALUES[VALUES_COUNT]{ 1000, 2000, 4000, 8000, 16000, 32000,64000,128000 };
/*
 * Сортировка массива методом вставок
 * @param vector_values вектор 
*/
void insertionSort(vector<double>& vector_values);
/*
 * Программа, тестирующая сортировку вставками
*/
int main()
{
    setlocale(LC_ALL, "Russian");
    mt19937 engine(time(0));
    uniform_real_distribution<double> gen(-1.0, 1.0);
    std::ofstream out;
    out.open("TestingLog.txt");
    if (out.is_open())
    {
        for (int i = 0; i < VALUES_COUNT; i++) {
            out << "N = " << TEST_VALUES[i] << endl;
            for (int j = 0; j < TESTS_LIMIT; j++) {
                vector<double> vector_values(TEST_VALUES[i]);
                /*cout << "\nДо Сортировки: " << endl;*/
                for (int k = 0; k < TEST_VALUES[i]; k++) {
                    vector_values[k] = gen(engine);
                }
               /*cout << "\nПосле Сортировки: " << endl;*/
               chrono::high_resolution_clock::time_point start = chrono::high_resolution_clock::now();
               insertionSort(vector_values);
               chrono::high_resolution_clock::time_point end = chrono::high_resolution_clock::now();
               chrono::duration<double> sec_diff = end - start;
               out << sec_diff.count()<< endl;
            }
        }
    }
    out.close();
}

void insertionSort(vector<double>& vector_values) {
    for (int i = 1; i < vector_values.size(); i++) {
        for(int j = i; j > 0 && vector_values[j - 1] > vector_values[j]; j--) {
            std::swap(vector_values[j-1], vector_values[j]);
        }
    }  
}
