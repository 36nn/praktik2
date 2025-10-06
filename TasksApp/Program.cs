using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите задачу (1-7) или 0 для выхода:");
            string choice = Console.ReadLine();
            if (choice == "0") break;
            switch (choice)
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "4": Task4(); break;
                case "5": Task5(); break;
                case "6": Task6(); break;
                case "7": Task7(); break;
                default: Console.WriteLine("Неверный выбор."); break;
            }
        }
    }

    // Задание 1. Ряды
    static void Task1()
    {
        Console.WriteLine("Введите x:");
        if (!double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double x))
        {
            Console.WriteLine("Неверный ввод x");
            return;
        }

        Console.WriteLine("Введите точность (e < 0.01):");
        if (!double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double epsilon))
        {
            Console.WriteLine("Неверный ввод точности");
            return;
        }
        if (epsilon >= 0.01)
        {
            Console.WriteLine("Точность должна быть меньше 0.01");
            return;
        }

        double result = CalculateSeries(x, epsilon);
        Console.WriteLine($"Значение функции с точностью {epsilon}: {result}");

        Console.WriteLine("\nВведите номер члена ряда (n):");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Неверный ввод n");
            return;
        }
        double nthTerm = GetNthTerm(x, n);
        Console.WriteLine($"Значение {n}-го члена ряда: {nthTerm}");
    }

    static double Factorial(int n)
    {
        if (n == 0) return 1;
        double result = 1;
        for (int i = 1; i <= n; i++)
            result *= i;
        return result;
    }

    static double GetNthTerm(double x, int n)
    {
        return Math.Pow(x, n) / Factorial(n);
    }

    static double CalculateSeries(double x, double epsilon)
    {
        double sum = 0;
        double term = 1;
        int n = 0;

        while (Math.Abs(term) > epsilon)
        {
            sum += term;
            n++;
            term = Math.Pow(x, n) / Factorial(n);
        }

        return sum;
    }

    // Задание 2. Счастливый билет
    static void Task2()
    {
        Console.Write("Введите шестизначный номер билета: ");
        if (!int.TryParse(Console.ReadLine(), out int ticket))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        if (ticket < 100000 || ticket > 999999)
        {
            Console.WriteLine("Ошибка: введите корректный шестизначный номер");
            return;
        }

        int sum1 = 0, sum2 = 0;
        int temp = ticket;
        for (int i = 0; i < 3; i++)
        {
            sum1 += temp % 10;
            temp /= 10;
        }
        for (int i = 0; i < 3; i++)
        {
            sum2 += temp % 10;
            temp /= 10;
        }

        if (sum1 == sum2)
        {
            Console.WriteLine("Билет счастливый!");
        }
        else
        {
            Console.WriteLine("Билет обычный.");
        }
    }

    // Задание 3. Сокращение дроби
    static void Task3()
    {
        Console.Write("Введите числитель M: ");
        if (!int.TryParse(Console.ReadLine(), out int m))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        Console.Write("Введите знаменатель N: ");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        int gcd = FindGCD(m, n);

        int numerator = m / gcd;
        int denominator = n / gcd;

        Console.WriteLine($"Несократимая дробь: {numerator}/{denominator}");
    }

    static int FindGCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Задание 4. Угадай число
    static void Task4()
    {
        Console.WriteLine("Загадайте число от 0 до 63. Я попробую его угадать.");
        Console.WriteLine("Отвечайте '1' (да) или '0' (нет) на мои вопросы.");

        int lower = 0;
        int upper = 63;

        int[] masks = { 32, 16, 8, 4, 2, 1 };
        int result = 0;

        foreach (int mask in masks)
        {
            Console.Write($"Ваше число больше или равно {lower + mask}? (1/0): ");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                result |= mask;
                lower += mask;
            }
            else if (answer == "0")
            {
                upper = lower + mask - 1;
            }
            else
            {
                Console.WriteLine("Неверный ввод, повторите");
                return;
            }
        }

        Console.WriteLine($"Ваше число: {result}");
        Console.WriteLine("Я угадал?");
    }

    // Задание 5. Кофейный аппарат
    static void Task5()
    {
        const int AMERICANO_WATER = 300;
        const int LATTE_WATER = 30;
        const int LATTE_MILK = 270;
        const int AMERICANO_PRICE = 150;
        const int LATTE_PRICE = 170;

        Console.Write("Введите количество воды (мл): ");
        if (!int.TryParse(Console.ReadLine(), out int water))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        Console.Write("Введите количество молока (мл): ");
        if (!int.TryParse(Console.ReadLine(), out int milk))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        int americanoCount = 0;
        int latteCount = 0;
        int totalEarnings = 0;

        while (true)
        {
            bool canMakeAmericano = water >= AMERICANO_WATER;
            bool canMakeLatte = water >= LATTE_WATER && milk >= LATTE_MILK;

            if (!canMakeAmericano && !canMakeLatte)
            {
                Console.WriteLine("\nИнгредиенты закончились.");
                Console.WriteLine($"Остаток воды: {water} мл");
                Console.WriteLine($"Остаток молока: {milk} мл");
                Console.WriteLine($"Приготовлено американо: {americanoCount}");
                Console.WriteLine($"Приготовлено латте: {latteCount}");
                Console.WriteLine($"Общий заработок: {totalEarnings} руб.");
                break;
            }

            Console.WriteLine("\nВыберите напиток:");
            Console.WriteLine("1 - Американо");
            Console.WriteLine("2 - Латте");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Неверный ввод");
                continue;
            }

            if (choice == 1)
            {
                if (canMakeAmericano)
                {
                    water -= AMERICANO_WATER;
                    americanoCount++;
                    totalEarnings += AMERICANO_PRICE;
                    Console.WriteLine("Ваш напиток готов (Американо)");
                }
                else
                {
                    Console.WriteLine("Не хватает воды");
                }
            }
            else if (choice == 2)
            {
                if (canMakeLatte)
                {
                    water -= LATTE_WATER;
                    milk -= LATTE_MILK;
                    latteCount++;
                    totalEarnings += LATTE_PRICE;
                    Console.WriteLine("Ваш напиток готов (Латте)");
                }
                else
                {
                    Console.WriteLine("Не хватает ингредиентов");
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор");
            }
        }
    }

    // Задание 6. Лабораторный опыт
    static void Task6()
    {
        Console.Write("Введите количество бактерий (N): ");
        if (!int.TryParse(Console.ReadLine(), out int N))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        Console.Write("Введите количество капель антибиотика (X): ");
        if (!int.TryParse(Console.ReadLine(), out int X))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        int bacteria = N;
        int hours = 0;
        int killPower = X * 10;

        Console.WriteLine("\nДинамика изменения количества бактерий:");

        while (killPower > 0)
        {
            hours++;

            bacteria *= 2;

            bacteria -= killPower;

            killPower -= X;

            if (bacteria < 0) bacteria = 0;

            Console.WriteLine($"Час {hours}: Бактерий = {bacteria}, Мощность антибиотика = {killPower}");
        }

        Console.WriteLine($"\nПроцесс завершен через {hours} часов");
        Console.WriteLine($"Конечное количество бактерий: {bacteria}");
    }

    // Задание 7. Колонизация Марса
    static void Task7()
    {
        Console.Write("Введите количество модулей (n): ");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        Console.Write("Введите размеры модуля (a b): ");
        string[] input = Console.ReadLine()?.Split();
        if (input == null || input.Length != 2 || !int.TryParse(input[0], out int a) || !int.TryParse(input[1], out int b))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        Console.Write("Введите размеры поля (h w): ");
        input = Console.ReadLine()?.Split();
        if (input == null || input.Length != 2 || !int.TryParse(input[0], out int h) || !int.TryParse(input[1], out int w))
        {
            Console.WriteLine("Неверный ввод");
            return;
        }

        int maxD = CalculateMaxProtection(n, a, b, h, w);

        Console.WriteLine($"Максимальная толщина защиты: {maxD}");
    }

    static int CalculateMaxProtection(int n, int a, int b, int h, int w)
    {
        if (!CanPlaceModules(n, a, b, h, w, 0))
            return -1;

        int left = 0;
        int right = Math.Min(h, w) / 2;
        int result = 0;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (CanPlaceModules(n, a, b, h, w, mid))
            {
                result = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }

    static bool CanPlaceModules(int n, int a, int b, int h, int w, int d)
    {
        int aWithD = a + 2 * d;
        int bWithD = b + 2 * d;

        return (h >= aWithD && w >= bWithD && CanFit(n, aWithD, bWithD, h, w)) ||
               (h >= bWithD && w >= aWithD && CanFit(n, bWithD, aWithD, h, w));
    }

    static bool CanFit(int n, int a, int b, int h, int w)
    {
        int maxHeight = h / a;
        int maxWidth = w / b;

        return maxHeight * maxWidth >= n;
    }
}
