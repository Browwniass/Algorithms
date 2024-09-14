#include <iostream>
#include <string>
#include <ctime>
#include <vector>
#include <chrono>
#include <random>
#include <utility>  
#include <fstream>
#include <unordered_map>
#include <map>
using namespace std;
const int VALUES_COUNT = 8;
int TESTS_LIMIT = 20;
const int TEST_VALUES[VALUES_COUNT]{ 1000, 2000, 4000, 8000, 16000, 32000,64000,128000 };

vector<double> arrayWithMaxmMiddleSelection(int i);
vector<double> arrayWithMaxDeterministicSelection(int i, int& recursion_stack, int& recursion_stack_height, int& recursion_stack_height_max);
int partition(vector<double>& A, int low, int high);
void quickSort(vector<double>& vector_values, int low, int high, int& recursion_stack, int& recursion_stack_height, int& recursion_stack_height_max);

/// <summary>
/// �������� ��� ������������ ������� ����������
/// </summary>
/// <returns></returns>
int main()
{
    setlocale(LC_ALL, "Russian");
    mt19937 engine(time(0));
    uniform_real_distribution<double> gen(-1.0, 1.0);
    std::ofstream out;
    std::ofstream outr;
    out.open("TestingLog.txt");
    outr.open("Testing2Log.txt");
    if (out.is_open() && outr.is_open())
    {
        for (int array_i = 0; array_i < 5; array_i++) {
            out << "K-" << array_i << endl;
            //cout << "K-" << array_i << endl;
            outr << "K-" << array_i << endl;
            for (int i = 0; i < VALUES_COUNT; i++) {
                /*cout << "N = " << TEST_VALUES[i] << endl;*/
                out << "N = " << TEST_VALUES[i]<<endl;
                outr << "N = " << TEST_VALUES[i] << endl;
                for (int j = 0; j < TESTS_LIMIT; j++) {
                    vector<double> vector_values(TEST_VALUES[i]);
                    int recursion_stack_height = 0;
                    int recursion_stack = 0;
                    int recursion_stack_height_max=0;
                    /*cout << "\n�� ����������: " << endl;*/
                    if (array_i == 0 || array_i == 1) {
                        //��������� ������� � ���������� ���������� �� -1 �� 1
                        for (int k = 0; k < TEST_VALUES[i]; k++) {
                            vector_values[k] = gen(engine);
                        }
                        //��������������� ������
                        if (array_i == 1) {  
                            std::sort(vector_values.begin(), vector_values.end());
                        }
                    }
                    //������ � ����������� ����������
                    else if (array_i == 2) {
                        double rand = gen(engine);
                        for (int k = 0; k < TEST_VALUES[i]; k++) {
                            vector_values[k] = rand;
                        }
                    }
                    //������ � ������������ ����������� ��������� ��� ������ �������� �������� � �������� ��������
                    else if (array_i == 3 && j < 1 ) {
                        vector_values = arrayWithMaxmMiddleSelection(i);
                    }
                    //������ � ������������ ����������� ��������� ��� ����������������� ������ �������� ��������
                    else if (array_i == 4 && j<1) {
                        vector_values = arrayWithMaxDeterministicSelection(i, recursion_stack,  recursion_stack_height, recursion_stack_height_max);
                    }
                    //for (int k = 0; k < TEST_VALUES[i]; k++) {
                    //    /*vector_values[k] = gen(engine);*/
                    //    cout << vector_values[k] << ",";
                    //}
                    //������� �������� ����� �������� ������� � ������������ ���-��� ��������� ��� ����������������� ������ �������� ��������
                    recursion_stack_height = 0;
                    recursion_stack = 0;
                    recursion_stack_height_max = 0;
                    /*cout << endl;*/
                    /*cout << "\n����� ����������: " << endl;*/
                    //������� ������� � ����������
                    chrono::high_resolution_clock::time_point start = chrono::high_resolution_clock::now();                    
                    quickSort(vector_values, 0, vector_values.size() - 1, recursion_stack, recursion_stack_height, recursion_stack_height_max);
                    chrono::high_resolution_clock::time_point end = chrono::high_resolution_clock::now();
                    chrono::duration<double> milli_diff = end - start;
                    //out << /*sec_diff.count()*/ "recursion_stack: "<< recursion_stack /*<< endl*/;
                    out << /*sec_diff.count()*/ /*" ;recursion_stack_height: " <<*/ recursion_stack_height_max << endl;
                    outr << milli_diff.count() << endl;
                    /*out <<sec_diff.count() << endl;*/
                 /*   for (int k = 0; k < TEST_VALUES[i]; k++) {
                        cout << vector_values[k] << ",";
                    }
                    cout << endl;*/
                }
            }
        }
    }
    out.close();
    outr.close();
}
/// <summary>
/// �������� ������� � ������������ ����������� ��������� ��� ������ �������� �������� � �������� ��������
/// </summary>
/// <param name="i">����� �����</param>
/// <returns>������ ��� ������</returns>
vector<double> arrayWithMaxmMiddleSelection(int i) {
    vector<double> final_vector(TEST_VALUES[i]);
    //���������� ������� a ����� n ���������� �� 1 �� n,
    for (int k = 0; k < TEST_VALUES[i]; k++) {
        final_vector[k] = k + 1;
    }
    for (int z = 0; z < TEST_VALUES[i]; z++) {
        std::swap(final_vector[z], final_vector[z / 2]);
    }
    return final_vector;
}
/// <summary>
/// �������� ������� � ������������ ����������� ��������� ��� ����������������� ������ �������� ��������
/// </summary>
/// <param name="i">����� �����</param>
/// <param name="recursion_stack">������� ���� ������� ��������</param>
/// <param name="recursion_stack_height">������� ������ ������������ �����</param>
/// <param name="recursion_stack_height_max">������������ ������ ������������ �����</param>
/// <returns>������ ��� ������</returns>
vector<double> arrayWithMaxDeterministicSelection(int i, int& recursion_stack, int& recursion_stack_height, int& recursion_stack_height_max) {
    vector<double> final_vector(TEST_VALUES[i]);
    //���������� ������ ��������������� ������� �������
    int p = (0 + TEST_VALUES[i] - 1) / 2;
    //������ ��� �������� ��������
    vector<double> val(TEST_VALUES[i]);
    //������ ��� �������� ������
    vector<double> key(TEST_VALUES[i]);
    //������������� ���� ����)
    for (int k = 0; k < TEST_VALUES[i]; k++) {
        key[k] = k + 1;
        val[k] = 0;
    }

    for (int k = 0; k < TEST_VALUES[i]; k++) {
        //����� ���������� ����������������� ������� ������� �� �����
        p = (TEST_VALUES[i] - 1 - k) / 2;
        //��������� �������� ����������� �������� �������� � ��� ������� �������
        double val_temp = TEST_VALUES[i] - (k + 1) + 1;
        val[p] = val_temp;
        //��������� ������ �� ��������� val
        quickSort(val, 0, val.size() - 1, recursion_stack, recursion_stack_height, recursion_stack_height_max);
        auto it = std::find(val.begin(), val.end(), val_temp);
        int temp_index = it - val.begin();
        //������ ��������� � key �� �������� �������������� val
        std::swap(key[p], key[temp_index]);
        //cout << "------Key----------" << endl;
        //for (int z = 0; z < TEST_VALUES[i]; z++) {

        //    cout << key[z] << ",";
        //}
        //cout << "\n--------Value----------" << endl;
        //for (int z = 0; z < TEST_VALUES[i]; z++) {

        //    cout << val[z] << ",";
        //}
        //cout << endl;
    }
    //��������� �������� �������� ������� ������
    vector<double> temp_vector = key;
    /*cout << "------------final------------" << endl;*/
    quickSort(key, 0, val.size() - 1, recursion_stack, recursion_stack_height, recursion_stack_height_max);
    //������������ �������� � val �� �������������� ������� � key
    for (int z = 0; z < TEST_VALUES[i]; z++) {
        auto it = std::find(temp_vector.begin(), temp_vector.end(), key[z]);
        int index = it - temp_vector.begin();
        final_vector[z] = val[index];
        /*cout << final_vector[z] << ",";*/
    }
   /* cout << endl;
    cout << endl;*/
    return final_vector;
}
/// <summary>
/// ��������� ������� �� ����� ������� ����������
/// </summary>
/// <param name="A">����������� ������</param>
/// <param name="low">����� �������</param>
/// <param name="high">������ �������</param>
/// <returns>����� ������ �������</returns>
int partition(vector<double>& A, int low, int high) {
    double v = A[(low + high) / 2];
    int i = low;
    int j = high;
    while (i <= j) {
        while (A[i] < v) {
            i++;
        }
        while (A[j] > v) {
            j--;
        }
        if (i >= j) {
            break;
        }
        std::swap(A[i++], A[j--]);    
    }
    return j;
}
/// <summary>
/// ������� ����������
/// </summary>
/// <param name="A">����������� ������</param>
/// <param name="low">����� �������</param>
/// <param name="high">������ �������</param>
/// <param name="recursion_stack">������� ���� ������� ��������</param>
/// <param name="recursion_stack_height">������� ������ ������������ �����</param>
/// <param name="recursion_stack_height_max">������������ ������ ������������ �����</param>
void quickSort(vector<double>& A, int low, int high, int& recursion_stack, int& recursion_stack_height, int& recursion_stack_height_max) {
    recursion_stack++;
    /*recursion_stack_height++;*/
   
    if (low < high) {
        recursion_stack_height++;
        if (recursion_stack_height_max < recursion_stack_height) recursion_stack_height_max = recursion_stack_height;
        int p = partition(A, low, high);
        quickSort(A, low, p,recursion_stack, recursion_stack_height, recursion_stack_height_max);
        quickSort(A, p+1, high, recursion_stack, recursion_stack_height, recursion_stack_height_max);
        recursion_stack_height--;
    }
    
    
}
