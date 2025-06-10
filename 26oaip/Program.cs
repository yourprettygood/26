using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public class Worker
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string House { get; set; }
    public string Apartment { get; set; }
    public string Nationality { get; set; }
    public DateTime BirthDate { get; set; }
    public int WorkshopNumber { get; set; }
    public string PersonnelNumber { get; set; }
    public string Education { get; set; }
    public int HireYear { get; set; }

    public override string ToString()
    {
        return string.Format("{0} {1} {2}, " +
               "Адрес: {3}, {4}, {5}, {6}, {7}, {8}, {9}, кв.{10}, " +
               "Национальность: {11}, " +
               "Дата рождения: {12}, " +
               "Цех: {13}, " +
               "Табельный номер: {14}, " +
               "Образование: {15}, " +
               "Год поступления: {16}",
               LastName, FirstName, MiddleName,
               PostalCode, Country, Region, District, City, Street, House, Apartment,
               Nationality,
               BirthDate.ToString("dd.MM.yyyy"),
               WorkshopNumber,
               PersonnelNumber,
               Education,
               HireYear);
    }
}

public class WorkerProcessor
{
    public static void ProcessWorkerData()
    {
        string inputFileName = "workers.txt";
        string outputFileName = "workers_2010.txt";

        try
        {
            // Позволяем пользователю выбрать способ ввода данных
            Console.WriteLine("Выберите способ ввода данных:");
            Console.WriteLine("1 - Ввести данные вручную");
            Console.WriteLine("2 - Использовать тестовые данные");
            Console.Write("Ваш выбор (1 или 2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CreateDataFileFromUserInput(inputFileName);
            }
            else
            {
                CreateSampleDataFile(inputFileName);
                Console.WriteLine("Используются тестовые данные.");
            }

            // Читаем и отображаем содержимое файла
            Console.WriteLine("\nСодержимое исходного файла:");
            Console.WriteLine(new string('-', 80));
            string[] lines = File.ReadAllLines(inputFileName);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.WriteLine(new string('-', 80));

            // Парсим данные о рабочих
            List<Worker> workers = ParseWorkerData(lines);

            // Фильтруем рабочих, поступивших в 2010 году
            List<Worker> workers2010 = new List<Worker>();
            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].HireYear == 2010)
                {
                    workers2010.Add(workers[i]);
                }
            }

            Console.WriteLine(string.Format("\nНайдено рабочих, поступивших в 2010 году: {0}", workers2010.Count));

            if (workers2010.Count > 0)
            {
                Console.WriteLine("\nДанные о рабочих, поступивших в 2010 году:");

                // Выводим результаты на экран
                for (int i = 0; i < workers2010.Count; i++)
                {
                    Console.WriteLine(workers2010[i].ToString());
                }

                // Сохраняем результаты в новый файл
                using (StreamWriter writer = new StreamWriter(outputFileName))
                {
                    writer.WriteLine("Рабочие, поступившие на работу в 2010 году");
                    writer.WriteLine(new string('=', 60));
                    writer.WriteLine();

                    for (int i = 0; i < workers2010.Count; i++)
                    {
                        writer.WriteLine(workers2010[i].ToString());
                        writer.WriteLine();
                    }
                }

                Console.WriteLine(string.Format("\nРезультаты сохранены в файл: {0}", outputFileName));
            }
            else
            {
                Console.WriteLine("\nРабочих, поступивших в 2010 году, не найдено.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Ошибка: {0}", ex.Message));
        }
    }

    private static void CreateDataFileFromUserInput(string fileName)
    {
        List<string> workerData = new List<string>();

        Console.WriteLine("\nВвод данных о рабочих");
        Console.WriteLine("Для завершения ввода введите пустую строку в поле 'Фамилия'");
        Console.WriteLine(new string('-', 50));

        while (true)
        {
            Console.WriteLine(string.Format("\nВвод данных о рабочем #{0}:", workerData.Count + 1));

            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
                break;

            Console.Write("Имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Отчество: ");
            string middleName = Console.ReadLine();

            Console.WriteLine("\nДомашний адрес:");
            Console.Write("Почтовый индекс: ");
            string postalCode = Console.ReadLine();

            Console.Write("Страна: ");
            string country = Console.ReadLine();

            Console.Write("Область: ");
            string region = Console.ReadLine();

            Console.Write("Район: ");
            string district = Console.ReadLine();

            Console.Write("Город: ");
            string city = Console.ReadLine();

            Console.Write("Улица: ");
            string street = Console.ReadLine();

            Console.Write("Дом: ");
            string house = Console.ReadLine();

            Console.Write("Квартира: ");
            string apartment = Console.ReadLine();

            Console.Write("Национальность: ");
            string nationality = Console.ReadLine();

            Console.Write("Дата рождения (дд.мм.гггг): ");
            string birthDate = Console.ReadLine();

            Console.Write("Номер цеха: ");
            string workshopNumber = Console.ReadLine();

            Console.Write("Табельный номер: ");
            string personnelNumber = Console.ReadLine();

            Console.Write("Образование: ");
            string education = Console.ReadLine();

            Console.Write("Год поступления на работу: ");
            string hireYear = Console.ReadLine();

            // Формируем строку данных
            string workerRecord = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}",
                lastName, firstName, middleName, postalCode, country, region, district,
                city, street, house, apartment, nationality, birthDate, workshopNumber,
                personnelNumber, education, hireYear);

            workerData.Add(workerRecord);

            Console.WriteLine("Данные добавлены успешно!");
        }

        if (workerData.Count > 0)
        {
            File.WriteAllLines(fileName, workerData.ToArray());
            Console.WriteLine(string.Format("\nВсего введено записей: {0}", workerData.Count));
        }
        else
        {
            Console.WriteLine("\nДанные не были введены.");
        }
    }

    private static void CreateSampleDataFile(string fileName)
    {
        string[] sampleData = new string[]
        {
            "Иванов;Иван;Иванович;123456;Россия;Московская область;Подольский район;Москва;Ленина;10;15;русский;15.05.1985;1;001234;высшее;2010",
            "Петров;Петр;Петрович;654321;Россия;Санкт-Петербург;Невский район;Санкт-Петербург;Невский проспект;25;8;русский;23.08.1980;2;002345;среднее специальное;2008",
            "Сидоров;Сидор;Сидорович;789012;Россия;Нижегородская область;Нижегородский район;Нижний Новгород;Горького;33;12;русский;10.12.1990;1;003456;высшее;2010",
            "Козлов;Алексей;Владимирович;345678;Россия;Свердловская область;Екатеринбургский район;Екатеринбург;Ленина;45;20;русский;03.03.1987;3;004567;среднее;2012",
            "Морозов;Владимир;Александрович;901234;Россия;Краснодарский край;Краснодарский район;Краснодар;Красная;67;5;русский;28.07.1983;2;005678;высшее;2010",
            "Волков;Андрей;Сергеевич;567890;Россия;Челябинская область;Челябинский район;Челябинск;Кирова;89;14;русский;18.11.1988;1;006789;среднее специальное;2009",
            "Лебедев;Дмитрий;Николаевич;234567;Россия;Ростовская область;Ростовский район;Ростов-на-Дону;Пушкинская;12;7;русский;05.02.1986;3;007890;высшее;2010"
        };

        File.WriteAllLines(fileName, sampleData);
    }

    private static List<Worker> ParseWorkerData(string[] lines)
    {
        List<Worker> workers = new List<Worker>();

        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] parts = lines[i].Split(';');
            if (parts.Length != 17) continue;

            try
            {
                Worker worker = new Worker();
                worker.LastName = parts[0];
                worker.FirstName = parts[1];
                worker.MiddleName = parts[2];
                worker.PostalCode = parts[3];
                worker.Country = parts[4];
                worker.Region = parts[5];
                worker.District = parts[6];
                worker.City = parts[7];
                worker.Street = parts[8];
                worker.House = parts[9];
                worker.Apartment = parts[10];
                worker.Nationality = parts[11];
                worker.BirthDate = DateTime.ParseExact(parts[12], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                worker.WorkshopNumber = int.Parse(parts[13]);
                worker.PersonnelNumber = parts[14];
                worker.Education = parts[15];
                worker.HireYear = int.Parse(parts[16]);

                workers.Add(worker);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Ошибка при парсинге строки: {0}. Ошибка: {1}", lines[i], ex.Message));
            }
        }

        return workers;
    }
}

public class NumberProcessor
{
    public static void ProcessNumbers()
    {
        string inputFileName = "numbers.txt";
        string outputFileName = "even_numbers_result.txt";

        try
        {
            // Позволяем пользователю выбрать способ создания файла
            Console.WriteLine("\nВыберите способ создания файла с числами:");
            Console.WriteLine("1 - Ввести числа вручную");
            Console.WriteLine("2 - Использовать тестовые числа");
            Console.Write("Ваш выбор (1 или 2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CreateNumbersFileFromUserInput(inputFileName);
            }
            else
            {
                CreateSampleNumbersFile(inputFileName);
                Console.WriteLine("Используются тестовые числа.");
            }

            // Читаем и отображаем содержимое файла
            Console.WriteLine("\nСодержимое файла с числами:");
            Console.WriteLine(new string('-', 50));
            string fileContent = File.ReadAllText(inputFileName);
            Console.WriteLine(fileContent);
            Console.WriteLine(new string('-', 50));

            // Парсим числа из файла
            List<int> numbers = ParseNumbers(fileContent);

            if (numbers.Count == 0)
            {
                Console.WriteLine("В файле не найдено корректных целых чисел.");
                return;
            }

            // Находим четные числа
            List<int> evenNumbers = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    evenNumbers.Add(numbers[i]);
                }
            }

            int evenCount = evenNumbers.Count;

            // Выводим результаты
            Console.WriteLine(string.Format("\nВсего чисел в файле: {0}", numbers.Count));
            Console.WriteLine(string.Format("Количество четных чисел: {0}", evenCount));

            if (evenCount > 0)
            {
                Console.WriteLine("Четные числа:");
                for (int i = 0; i < evenNumbers.Count; i++)
                {
                    Console.Write(evenNumbers[i] + " ");
                }
                Console.WriteLine();
            }

            // Сохраняем результаты в файл
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine("Анализ четных чисел в файле");
                writer.WriteLine(new string('=', 40));
                writer.WriteLine();
                writer.WriteLine(string.Format("Исходный файл: {0}", inputFileName));
                writer.WriteLine(string.Format("Всего чисел: {0}", numbers.Count));
                writer.WriteLine(string.Format("Количество четных чисел: {0}", evenCount));
                writer.WriteLine();

                if (evenCount > 0)
                {
                    writer.WriteLine("Четные числа:");
                    for (int i = 0; i < evenNumbers.Count; i++)
                    {
                        writer.Write(evenNumbers[i] + " ");
                    }
                    writer.WriteLine();
                    writer.WriteLine();
                }

                writer.WriteLine("Все числа из файла:");
                for (int i = 0; i < numbers.Count; i++)
                {
                    writer.Write(numbers[i] + " ");
                }
                writer.WriteLine();
            }

            Console.WriteLine(string.Format("\nРезультаты сохранены в файл: {0}", outputFileName));
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Ошибка: {0}", ex.Message));
        }
    }

    private static void CreateNumbersFileFromUserInput(string fileName)
    {
        Console.WriteLine("\nВвод целых чисел");
        Console.WriteLine("Числа можно вводить через пробел, запятую или каждое с новой строки");
        Console.WriteLine("Для завершения ввода введите пустую строку");
        Console.WriteLine(new string('-', 50));

        List<string> inputLines = new List<string>();

        while (true)
        {
            Console.Write("Введите числа: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                break;

            inputLines.Add(input);
        }

        if (inputLines.Count > 0)
        {
            File.WriteAllLines(fileName, inputLines.ToArray());
            Console.WriteLine(string.Format("Данные сохранены в файл: {0}", fileName));
        }
        else
        {
            Console.WriteLine("Числа не были введены.");
        }
    }

    private static void CreateSampleNumbersFile(string fileName)
    {
        string[] sampleNumbers = new string[]
        {
            "12 45 78 23 34 67 89 102 56 73",
            "88 91 44 37 66 55 128 99 42 81",
            "17 24 39 48 63 76 85 94 101 112"
        };

        File.WriteAllLines(fileName, sampleNumbers);
    }

    private static List<int> ParseNumbers(string content)
    {
        List<int> numbers = new List<int>();

        // Разделители: пробел, запятая, табуляция, перенос строки
        char[] separators = new char[] { ' ', ',', '\t', '\n', '\r' };
        string[] parts = content.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < parts.Length; i++)
        {
            int number;
            if (int.TryParse(parts[i].Trim(), out number))
            {
                numbers.Add(number);
            }
            else
            {
                Console.WriteLine(string.Format("Предупреждение: '{0}' не является корректным целым числом и будет пропущено.", parts[i].Trim()));
            }
        }

        return numbers;
    }
}

public class MatrixProcessor
{
    public static void ProcessMatrices()
    {
        string firstFileName = "matrices1.txt";
        string secondFileName = "matrices2.txt";

        try
        {
            Console.WriteLine("\nВвод данных для первого файла матриц:");
            int k, n;
            CreateMatrixFile(firstFileName, out k, out n, "первого");

            Console.WriteLine("\nВвод данных для второго файла матриц:");
            int l, m;
            CreateMatrixFile(secondFileName, out l, out m, "второго");

            // Проверяем, что размеры матриц одинаковые
            if (n != m)
            {
                Console.WriteLine("Ошибка: Размеры матриц в файлах должны быть одинаковыми!");
                return;
            }

            Console.WriteLine(string.Format("\nПервый файл содержит {0} матриц размера {1}x{1}", k, n));
            Console.WriteLine(string.Format("Второй файл содержит {0} матриц размера {1}x{1}", l, n));

            // Если количество матриц разное, добавляем единичные матрицы
            if (k != l)
            {
                if (k < l)
                {
                    AddIdentityMatrices(firstFileName, l - k, n);
                    Console.WriteLine(string.Format("В первый файл добавлено {0} единичных матриц", l - k));
                    k = l;
                }
                else
                {
                    AddIdentityMatrices(secondFileName, k - l, n);
                    Console.WriteLine(string.Format("Во второй файл добавлено {0} единичных матриц", k - l));
                    l = k;
                }
            }

            // Выводим содержимое файлов
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("СОДЕРЖИМОЕ ПЕРВОГО ФАЙЛА:");
            Console.WriteLine(new string('=', 60));
            DisplayMatrixFile(firstFileName, k, n);

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("СОДЕРЖИМОЕ ВТОРОГО ФАЙЛА:");
            Console.WriteLine(new string('=', 60));
            DisplayMatrixFile(secondFileName, l, n);

        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Ошибка: {0}", ex.Message));
        }
    }

    private static void CreateMatrixFile(string fileName, out int matrixCount, out int matrixSize, string fileDescription)
    {
        Console.WriteLine(string.Format("Создание {0} файла матриц", fileDescription));

        // Выбор способа создания файла
        Console.WriteLine("Выберите способ создания файла:");
        Console.WriteLine("1 - Ввести матрицы вручную");
        Console.WriteLine("2 - Использовать случайные матрицы");
        Console.Write("Ваш выбор (1 или 2): ");

        string choice = Console.ReadLine();

        Console.Write("Введите количество матриц: ");
        matrixCount = int.Parse(Console.ReadLine());

        Console.Write("Введите размер матриц (n для матрицы nxn): ");
        matrixSize = int.Parse(Console.ReadLine());

        if (choice == "1")
        {
            CreateMatrixFileFromUserInput(fileName, matrixCount, matrixSize);
        }
        else
        {
            CreateRandomMatrixFile(fileName, matrixCount, matrixSize);
        }
    }

    private static void CreateMatrixFileFromUserInput(string fileName, int matrixCount, int matrixSize)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            for (int matrixIndex = 0; matrixIndex < matrixCount; matrixIndex++)
            {
                Console.WriteLine(string.Format("\nВвод матрицы #{0} ({1}x{1}):", matrixIndex + 1, matrixSize));

                for (int i = 0; i < matrixSize; i++)
                {
                    Console.Write(string.Format("Введите элементы строки {0} через пробел: ", i + 1));
                    string row = Console.ReadLine();
                    writer.WriteLine(row);
                }

                // Добавляем пустую строку между матрицами (кроме последней)
                if (matrixIndex < matrixCount - 1)
                {
                    writer.WriteLine();
                }
            }
        }

        Console.WriteLine(string.Format("Матрицы сохранены в файл: {0}", fileName));
    }

    private static void CreateRandomMatrixFile(string fileName, int matrixCount, int matrixSize)
    {
        Random random = new Random();

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            for (int matrixIndex = 0; matrixIndex < matrixCount; matrixIndex++)
            {
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        int value = random.Next(1, 10); // Случайные числа от 1 до 9
                        writer.Write(value);
                        if (j < matrixSize - 1)
                            writer.Write(" ");
                    }
                    writer.WriteLine();
                }

                // Добавляем пустую строку между матрицами (кроме последней)
                if (matrixIndex < matrixCount - 1)
                {
                    writer.WriteLine();
                }
            }
        }

        Console.WriteLine(string.Format("Случайные матрицы сохранены в файл: {0}", fileName));
    }

    private static void AddIdentityMatrices(string fileName, int count, int size)
    {
        using (StreamWriter writer = new StreamWriter(fileName, true)) // append = true
        {
            for (int matrixIndex = 0; matrixIndex < count; matrixIndex++)
            {
                writer.WriteLine(); // Пустая строка перед новой матрицей

                // Создаем единичную матрицу
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == j)
                            writer.Write("1");
                        else
                            writer.Write("0");

                        if (j < size - 1)
                            writer.Write(" ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }

    private static void DisplayMatrixFile(string fileName, int matrixCount, int matrixSize)
    {
        string[] lines = File.ReadAllLines(fileName);
        int currentMatrix = 1;
        int currentRow = 0;

        Console.WriteLine(string.Format("Файл содержит {0} матриц размером {1}x{1}:\n", matrixCount, matrixSize));

        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                // Пустая строка - переход к следующей матрице
                currentMatrix++;
                currentRow = 0;
                Console.WriteLine();
                continue;
            }

            if (currentRow == 0)
            {
                Console.WriteLine(string.Format("Матрица #{0}:", currentMatrix));
            }

            Console.WriteLine(lines[i]);
            currentRow++;

            if (currentRow == matrixSize)
            {
                Console.WriteLine();
                currentRow = 0;
            }
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("=== ЗАДАНИЕ 1: Обработка данных рабочих ===");
        WorkerProcessor.ProcessWorkerData();

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("=== ЗАДАНИЕ 2: Поиск четных чисел ===");
        NumberProcessor.ProcessNumbers();

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("=== ЗАДАНИЕ 3: Обработка матриц ===");
        MatrixProcessor.ProcessMatrices();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}