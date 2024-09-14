//// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.Globalization;
public class Bunc_Rocks
{
    /// <summary>
    /// Функция, реализующая решение задачи о рюкзаке
    /// </summary>
    /// <param name="W">Максимальный вес</param>
    /// <param name="weight">массив со значениями по весу</param>
    /// <param name="n">колиество предметов</param>
    /// <returns>Идеальное заполнение рюкзака</returns>
    static int KnapSack(int W, int[] weight, int n)
    {
        int[] arr = new int[W + 1];
        for (int i = 1; i < n + 1; i++)
        {
            for (int w = W; w >= 0; w--)
            {
                if (weight[i - 1] <= w)
                    arr[w]= Math.Max(arr[w], arr[w - weight[i - 1]] + weight[i - 1]);
            }
        }
        return arr[W];
    }
    /// <summary>
    /// Функция, решающая задачу о двух кучах камней
    /// </summary>
    private static void Main()
    {                 
            while (true)
            {
                Console.Write("Для Тимуса - 1; Для Лабы - 2: ");
                int chosen_number = Convert.ToInt32(Console.ReadLine());
                if (chosen_number == 1) TestTimus();
                if (chosen_number == 2) TestLaba();
                else Console.WriteLine("Выберите имеющийся вариант");
            }        
    }
    /// <summary>
    /// Функция для решения задачи из тимуса
    /// </summary>
    public static void TestTimus()
    {
        double W_temp = 0;
        int N = Convert.ToInt32(Console.ReadLine());
        string str2 = Console.ReadLine();
        string[] w_2 = str2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] weights = new int[N];
        for (int i = 0; i < N; i++) { weights[i] = Convert.ToInt32(w_2[i]); W_temp += weights[i]; }
        int W = (int)Math.Floor(W_temp / 2);

        Console.WriteLine(W_temp - 2 * KnapSack(W, weights, N));
    }
    /// <summary>
    /// Функция с рандомной генерацией значений
    /// </summary>
    public static void TestLaba()
    {
        Random rnd = new Random();
        double W_temp = 0;
        int N = rnd.Next(1,21);
        
        int[] weights = new int[N];
        GetRandomArr(ref weights, 1, 10000);
        for (int i = 0; i < N; i++) { W_temp += weights[i]; }
        int W = (int)Math.Floor(W_temp / 2);

        Console.WriteLine(W_temp - 2 * KnapSack(W, weights, N));
    }
    /// <summary>
    /// Заполнение массива рандомными неповторяющими числами в диапазоне 
    /// </summary>
    /// <param name="arr">массив, который требуется заполнить</param>
    /// <param name="min">От</param>
    /// <param name="max">До</param>
    public static void GetRandomArr(ref int[] arr, int min, int max)
    {
        Random random = new Random();
        for (int i = 0; i < arr.Length; i++)
        {
            var num = random.Next(min, max+1);

            if (arr.Contains(num))
            {
                i--;
            }
            else
            {
                arr[i] = num;
            }
        }
    }
}