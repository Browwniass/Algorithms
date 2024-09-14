#include <iostream>
#include <algorithm>
#include <queue>
#include <cstdlib>
#include <ctime>
#include <string>
#include <vector>
#include <numeric>
#include <fstream>
using namespace std;
//�������
template<typename T>
class Queue {

private:

    // ���� �������
    class Node {
    public:
        T* value = nullptr;  // ��������
        Node* next = nullptr;  // ��������� �� ����. �������       
        Node(T value) {
            this->value = new T(value);
        }
    };
    Node* first = nullptr;  // ������ �������
    Node* last = nullptr;  // ��������� �������
    int elems = 0;
public:
    // �������� �������
    class Iterator {
    public:
        using difference_type = std::ptrdiff_t;
        using value_type = T;
        using pointer = T*;
        using reference = T&;
        using iterator_category = std::random_access_iterator_tag;

    
    Node* current_node;  // ��������� �� ����

    public:
        Iterator(Node* pointer_node) : current_node(pointer_node) {}

        /// <summary>
        /// ���������� ������������ ����������
        /// </summary>
        /// <param name="">int</param>
        void operator++(int) {
            this->current_node = this->current_node->next;

        }

        /// <summary>
        /// ���������� ����������� ����������
        /// </summary>
        /// <returns>Iterator&</returns>
        Iterator& operator++() {
            this->current_node = this->current_node->next;
            return (*this);
        }
        /// <summary>
        /// ���������� ��������� �������������
        /// </summary>
        /// <returns>��������������� ��������</returns>
        T& operator*() {
            return *(this->current_node->value);
  
        }

        /// <summary>
        /// ���������� ��������� ���������
        /// </summary>
        /// <param name="b">�������� �� ������������ �������</param>
        /// <returns>bool</returns>
        bool operator==(const Iterator& b) const {
            return this->current_node == b.current_node;
        }

        /// <summary>
        /// ���������� ��������� �����������
        /// </summary>
        /// <param name="b">�������� �� ������������ �������</param>
        /// <returns>bool</returns>
        bool operator!=(const Iterator& b) const {
            return this->current_node != b.current_node;
        }


    };
   
    /// <summary>
    /// ���������� �������� � �����
    /// </summary>
    /// <param name="value">��������</param>
    void push(T value) {
        Node* new_node = new Node(value);//������� ����� ������� �������      
        if (this->last == nullptr) {//��������� �� �������� ������� ������
            this->first = new_node;
            this->last = new_node;
        }
        else {//� ��������� �������
            this->last->next = new_node;//������ � �������� ���������� �������� � ������� � ��������� �� ����������� ������� ��������� �� ������������� �������
            this->last = new_node;//����������� � ��������� �� ������� �������� �������, ��������� �� ������������� �������
        }

        this->elems++;
    }

    /// <summary>
    /// ������� �������� �� �����
    /// </summary>
    void pop() {
        if (this->last == nullptr) {//��������� �� �������� ������� ������
            throw std::out_of_range("�� �������� ��������� ��� ��������");
        }
        else {
            Node* temp = this->first;//����� ������(�������) ������� �������
            this->first = this->first->next;//���������� �������� ���� ��������� �� ��������� ������� � ������� �������� � ���������� ��������� �� ������� ������ ������� ������
            if (this->first == nullptr) {
                this->last = nullptr;
            };
            delete(temp);//������� ������ ������� �������
            this->elems--;
        }

    }
    /// <summary>
    /// ����� �������� ��������� �� ������
    /// </summary>
    /// <returns>Iterator</returns>
    Iterator begin() {
        return Iterator(this->first);
    }

    /// <summary>
    /// ����� �������� ��������� �� �����
    /// </summary>
    /// <returns>Iterator</returns>
    Iterator end() {
        return nullptr;
    }
    /// <summary>
    /// ��������� ������� ��������
    /// </summary>
    /// <returns>������ �������</returns>
    T front() {
        return *(this->first->value);
    }
    /// <summary>
    /// ������ �������
    /// </summary>
    /// <returns>�������� ������� ��������</returns>
    int size() {
        return this->elems;
    }
    /// <summary>
    /// �������� �� �������
    /// </summary>
    /// <returns>bool</returns>
    bool empty() {
        return this->first ? false : true;
    }
    /// <summary>
    /// ���������� �������
    /// </summary>
    void sort() {
        vector<T> temp_vector;
        for (auto el : *this) {//��������� ������ ���������� �������
            temp_vector.push_back(el);
        }
        std::sort(temp_vector.begin(), temp_vector.end());//��������� ������
        int temp_elems = elems;
        for (int i = 0; i < temp_elems; i++) {//������� �������
            this->pop();
            this->elems--;
        }
        for (int i = 0; i < temp_elems; i++) {//��������� ������� ���������� ���������������� �������
            this->push(temp_vector[i]);
        }
    }
};
/// <summary>
/// ����1
/// </summary>
void test_1() {
    std::ofstream out;
    out.open("Test1.txt");
    if (out.is_open())
    {
        Queue<int> queue;
        const int AMOUNT = 1000;

        const int MIN = -1000;
        const int MAX = 1000;

        for (int i = 0; i < AMOUNT; i++) {
            queue.push(MIN + rand() % (MAX - MIN + 1));
        }
        int sum = std::accumulate(queue.begin(), queue.end(), 0, std::plus<int>());
        out << "�����: " << sum << endl;
        int max = *max_element(queue.begin(), queue.end());
        int min = *min_element(queue.begin(), queue.end());
        out << "����������� �������: " << min << "\n������������ �������: " << max << "\n������� �������: " << sum / queue.size() << endl;

    }
    out.close();
}

/// <summary>
/// ����2
/// </summary>
void test_2() {
    std::ofstream out;
    out.open("Test2.txt");

    if (out.is_open())
    {
        Queue<string> queue;
        const int AMOUNT = 10;
        const string AMOUNTSTRING[AMOUNT] = { "Midnight calling", "Mist of resolving", "Crown me with the", "Pure green leaf", "Praise to my father", "Blessed by the water" , "Black night, dark sky", "The devil's cry", "Bless me with the", "Leaf of the world tree" };
        out << "--------------����������� ������--------------" << endl;
        for (int i = 0; i < AMOUNT; i++) {
            queue.push(AMOUNTSTRING[i]);
        }
        for (auto el : queue) {
            out << el << endl;
        }
        out << "--------------C ����������� ���������--------------" << endl;
        queue.push("On it I see");
        for (auto el : queue) {
            out << el << endl;
        }
        out << "--------------C ������� ���������--------------" << endl;
        queue.pop();
        queue.pop();
        for (auto el : queue) {
            out << el << endl;
        }
    }
    out.close();
}
//��������� ����
struct Date {
    int day;
    int month;
    int year;
};

//��������� ��������
struct Person {
    string name;
    string surname;
    string patronymic;
    Date birth;
};
/// <summary>
/// ����3
/// </summary>
void test_3() {
    std::ofstream out;
    out.open("Test3.txt");

    if (out.is_open())
    {
        const int DAY = 86400;
        const int AMOUNT = 100;
        const int NAMES_POOL = 7;
        // ������ ��� ���������
        const string NAMES[NAMES_POOL] = { "Peter", "Betty", "Miguel", "Rendy", "Hobby", "Paul", "J.M." };
        const string SURNAMES[NAMES_POOL] = { "O`Harra", "Parker", "Brant", "Robertson", "Brown", "Jenkins", "Dematees" };
        const string PATRONYMICS[NAMES_POOL] = { "Grigirovich", "Ivanovna", "Petrovich", "Fedorovna", "Pavlova", "Vladislavovich", };


        // ������ ��� ��������� ����:
        const int DAY_MAX = 31;
        const int MONTH_MAX = 12;
        const int YEAR_MIN = 1980;
        const int YEAR_MAX = 2020;

        Queue<Person> queue;

        // ���������
        for (int i = 0; i < AMOUNT; i++) {

            queue.push(Person{
                NAMES[rand() % NAMES_POOL],
                SURNAMES[rand() % NAMES_POOL],
                PATRONYMICS[rand() % NAMES_POOL],
                Date{
                    (1 + rand() % DAY_MAX),
                    (1 + rand() % MONTH_MAX),
                    (YEAR_MIN + rand() % (YEAR_MAX - YEAR_MIN + 1))
                }
                });

        }
        for (auto el : queue) {
            out << el.name << " " << el.surname << " " << el.patronymic << " ";
            out << el.birth.day << ":" << el.birth.month << ":" << el.birth.year << endl;
        }
        Queue<Person> older_30;
        Queue<Person> younger_20;


        time_t time_now = time(0);

        // ����������
        for (int i = queue.size(); i > 0; i--) {
            queue.pop();
            Person temp_q = queue.front();


            tm dateBirth = { 0, 0, 0, temp_q.birth.day, temp_q.birth.month - 1, temp_q.birth.year - 1900, 0, 0, 0 };

            time_t t_birth = mktime(&dateBirth);

            time_t daysDifference = ((t_birth > time_now) ? t_birth - time_now : time_now - t_birth) / DAY;
            daysDifference /= 365;


            if (daysDifference < 20) {
                younger_20.push(temp_q);
            }
            else if (daysDifference > 30) {
                older_30.push(temp_q);
            }

            else {
                queue.push(temp_q);
            }
        }
        out << "\n---------------Test3---------------" << endl;
        out << "������ 20: " << younger_20.size() << "\n������ 30: " << older_30.size() << "\n���������: " << queue.size() << endl;
        out << "\n�������� ����������: " << younger_20.size() << " + " << older_30.size() << " + " << queue.size() << " = " << AMOUNT << "? - ";
        (((younger_20.size() + older_30.size() + queue.size()) == AMOUNT) ? (out << "���������") : (out << "�����������"));
    }
    out.close();
}

/// <summary>
/// ����4
/// </summary>
void test_4() {
    std::ofstream out;
    out.open("Test4.txt");

    if (out.is_open())
    {
        Queue<int> queue1;
        deque<int> queue_test;

        const int AMOUNT = 1000;

        const int MIN = -1000;
        const int MAX = 1000;
        //���������� ����� ��������
        for (int i = 0; i < AMOUNT; i++) {
            int r = MIN + rand() % (MAX - MIN + 1);
            queue_test.push_back(r);
            queue1.push(r);

        }
        out << "------------------���������� �� ���������� ���������� ����������-------------------------" << endl;
        for (auto el : queue1) {
            out << el << " ";
        }
        out << endl;
        out << "------------------���������� �� ���������� stl ����������-------------------------" << endl;
        for (auto el : queue_test) {
            out << el << " ";
        }
        out << endl;
        queue1.sort();
        sort(queue_test.begin(), queue_test.end());
        out << "------------------���������� ����� ���������� ���������� ����������-------------------------" << endl;
        for (auto el : queue1) {
            out << el << " ";
        }
        out << endl;
        out << "------------------���������� ����� ���������� stl ����������-------------------------" << endl;
        for (auto el : queue_test) {
            out << el << " ";
        }
        // �������� ������������
        bool is_correct = true;

        for (int i = 0; i < queue1.size(); i++) {
            if (queue1.front() != queue_test.front()) {
                is_correct = false;
                break;
            }
            queue_test.pop_front();
        }
        out << endl << "���������:" << endl;
        if (is_correct) {
            out << "������� ������������� ���������" << endl;
        }
        else {
            out << "������� ������������� ��-�������" << endl;
        }
    }
    out.close();
}
/// <summary>
/// ����5
/// </summary>
void test_5() {
    std::ofstream out;
    out.open("Test5.txt");

    if (out.is_open())
    {
        Queue<int> queue1;
        const int AMOUNT = 1000;

        const int MIN = -1000;
        const int MAX = 1000;

        // ���������
        for (int i = 0; i < AMOUNT; i++) {
            int r = MIN + rand() % (MAX - MIN + 1);
            queue1.push(r);

        }
        //����������
        queue1.sort();
        out << "---------------------�� ��������---------------------" << endl;
        for (auto el : queue1) {
            out << el << " ";
        }
        int temp_array[AMOUNT];//�������� ������� ��� �������� ���������
        for (int i = 0; i < AMOUNT; i++) {//���������� �������
            temp_array[i] = queue1.front();
            queue1.pop();
        }
        for (int i = AMOUNT - 1; i >= 0; i--) {//�������� ����� ����� �������
            queue1.push(temp_array[i]);//���������� ������� � �������� �������
        }
        out << endl << endl;
        out << "---------------------����� ��������---------------------" << endl;
        for (auto el : queue1) {
            out << el << " ";
        }

    }
    out.close();
}
/// <summary>
/// ���������, ����������� ������ � ������������ ������ �������
/// </summary>
/// <returns></returns>
int main() {
    srand(time(0));
    setlocale(LC_ALL, "Russian");
    test_1();
    test_2();
    test_3();
    test_4();
    test_5();
}