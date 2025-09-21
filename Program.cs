using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class Program
    {
        private static Film[] films;
        private static int filmCount = 0;
        private static int maxFilms;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("-СИСТЕМА УПРАВЛІННЯ ФІЛЬМАМИ-");
            Console.Write("Введіть кількість об'єктів: ");
            maxFilms = ReadPositiveInt();
            films = new Film[maxFilms];
            int choice;
            do
            {
                ShowMenu();
                choice = ReadInt();
                switch (choice)
                {
                    case 1: AddFilm(); break;
                    case 2: ShowAllFilms(); break;
                    case 3: FindFilm(); break;
                    case 4: DemonstrateFilmBehavior(); break;
                    case 5: DeleteFilm(); break;
                    case 0: Console.WriteLine("Вихід"); break;
                    default: Console.WriteLine("Невірний вибір! Введіть число від 0 до 5."); break;
                }
                if (choice != 0)
                    Console.WriteLine();
            } while (choice != 0);
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n-МЕНЮ-");
            Console.WriteLine("1 – Додати об'єкт");
            Console.WriteLine("2 – Переглянути всі об'єкти");
            Console.WriteLine("3 – Знайти об'єкт");
            Console.WriteLine("4 – Продемонструвати поведінку");
            Console.WriteLine("5 – Видалити об'єкт");
            Console.WriteLine("0 – Вийти з програми");
            Console.Write("Ваш вибір: ");
        }

        //ЗМІНЕНО: додавання нового фільму з обробкою винятків
        static void AddFilm()
        {
            if (filmCount >= maxFilms)
            {
                Console.WriteLine("Досягнуто максимум об'єктів!");
                return;
            }
            Console.WriteLine("\n-ДОДАВАННЯ ФІЛЬМУ-");
            Film newFilm = new Film();
            //НОВЕ: Введення назви з обробкою винятків
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Назва: ");
                string input = Console.ReadLine();
                try
                {
                    newFilm.Title = input;
                    isValid = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                }
            }

            //НОВЕ: Введення режисера з обробкою винятків
            isValid = false;
            while (!isValid)
            {
                Console.Write("Режисер: ");
                string input = Console.ReadLine();
                try
                {
                    newFilm.Director = input;
                    isValid = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                }
            }

            //НОВЕ: Введення року з обробкою винятків
            isValid = false;
            while (!isValid)
            {
                Console.Write("Рік: ");
                int year = ReadInt();
                try
                {
                    newFilm.ReleaseYear = year;
                    isValid = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                }
            }

            //НОВЕ: Введення рейтингу з обробкою винятків
            isValid = false;
            while (!isValid)
            {
                Console.Write("Рейтинг: ");
                double rate = ReadDouble();
                try
                {
                    newFilm.Rating = rate;
                    isValid = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                }
            }

            //НОВЕ: Введення жанру з обробкою винятків
            isValid = false;
            while (!isValid)
            {
                Console.Write("Жанр (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
                int genreChoice = ReadGenreChoice();
                try
                {
                    newFilm.Genre = (FilmGenre)(genreChoice - 1);
                    isValid = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                }
            }

            //НОВЕ: Роки з прем'єри розраховуються автоматично
            Console.WriteLine($"Років з прем'єри: {newFilm.YearsFromPremiere}");
            films[filmCount] = newFilm;
            filmCount++;
            Console.WriteLine("Фільм додано!");
        }

        static void ShowAllFilms()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n-ВСІ ФІЛЬМИ-");
            for (int i = 0; i < filmCount; i++)
            {
                Console.Write($"{i + 1}. ");
                films[i].DisplayInfo();
            }
        }

        //ЗМІНЕНО: пошук фільму тільки за назвою або режисером
        static void FindFilm()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n1 - Пошук за назвою");
            Console.WriteLine("2 - Пошук за режисером");
            Console.Write("Ваш вибір: ");
            int choice = ReadSearchChoice();
            bool found = false;
            if (choice == 1)
            {
                Console.Write("Введіть назву: ");
                string searchTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(searchTitle))
                {
                    for (int i = 0; i < filmCount; i++)
                    {
                        if (films[i].Title.ToLower().Contains(searchTitle.ToLower()))
                        {
                            films[i].DisplayInfo();
                            found = true;
                        }
                    }
                }
            }
            else if (choice == 2)
            {
                Console.Write("Введіть режисера: ");
                string searchDirector = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(searchDirector))
                {
                    for (int i = 0; i < filmCount; i++)
                    {
                        if (films[i].Director.ToLower().Contains(searchDirector.ToLower()))
                        {
                            films[i].DisplayInfo();
                            found = true;
                        }
                    }
                }
            }

            if (!found)
                Console.WriteLine("Нічого не знайдено!");
        }

        //РОЗШИРЕНО: поведінка фільмів з новими опціями
        static void DemonstrateFilmBehavior()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n-ДЕМОНСТРАЦІЯ ПОВЕДІНКИ-");
            Console.WriteLine("1 - Високорейтингові фільми");
            Console.WriteLine("2 - Фільми за жанром");
            //НОВЕ: додано дві нові опції
            Console.WriteLine("3 - Класичні фільми");
            Console.WriteLine("4 - Сучасні фільми");
            Console.Write("Ваш вибір: ");
            int choice = ReadDemoChoice();
            if (choice == 1)
            {
                Console.WriteLine("Високорейтингові фільми (>= 8,0):");
                bool foundHighRated = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].IsHighRated())
                    {
                        films[i].DisplayInfo();
                        foundHighRated = true;
                    }
                }
                if (!foundHighRated)
                {
                    Console.WriteLine("Немає високорейтингових фільмів!");
                }
            }
            else if (choice == 2)
            {
                Console.Write("Жанр (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
                int genreChoice = ReadGenreChoice();
                FilmGenre selectedGenre = (FilmGenre)(genreChoice - 1);
                bool foundByGenre = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].Genre == selectedGenre)
                    {
                        films[i].DisplayInfo();
                        foundByGenre = true;
                    }
                }
                if (!foundByGenre)
                {
                    Console.WriteLine($"Немає фільмів обраного жанру!");
                }
            }
            //НОВЕ: демонстрація класичних фільмів
            else if (choice == 3)
            {
                Console.WriteLine("Класичні фільми (старше 20 років):");
                bool foundClassic = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].IsClassic())
                    {
                        films[i].DisplayInfo();
                        Console.WriteLine($"Категорія: {films[i].GetAgeCategory()} ({films[i].YearsFromPremiere} років з прем'єри)");
                        foundClassic = true;
                    }
                }
                if (!foundClassic)
                {
                    Console.WriteLine("Немає класичних фільмів!");
                }
            }
            //НОВЕ: демонстрація сучасних фільмів
            else if (choice == 4)
            {
                Console.WriteLine("Сучасні фільми (молодше 5 років):");
                bool foundModern = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].IsModern())
                    {
                        films[i].DisplayInfo();
                        Console.WriteLine($"Категорія: {films[i].GetAgeCategory()} ({films[i].YearsFromPremiere} років з прем'єри)");
                        foundModern = true;
                    }
                }
                if (!foundModern)
                {
                    Console.WriteLine("Немає сучасних фільмів!");
                }
            }
        }

        //Видалення фільмів за індексом або жанром
        static void DeleteFilm()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n-ВИДАЛЕННЯ ФІЛЬМУ-");
            Console.WriteLine("1 - Видалити за номером");
            Console.WriteLine("2 - Видалити за жанром");
            Console.Write("Ваш вибір: ");
            int choice = ReadDeleteMethodChoice();
            if (choice == 1)
            {
                DeleteFilmByIndex();
            }
            else if (choice == 2)
            {
                DeleteFilmByGenre();
            }
        }

        //видалення фільму за індексом
        static void DeleteFilmByIndex()
        {
            ShowAllFilms();
            Console.Write("Номер для видалення: ");
            int index = ReadDeleteIndex() - 1;
            if (index >= 0 && index < filmCount)
            {
                for (int i = index; i < filmCount - 1; i++)
                {
                    films[i] = films[i + 1];
                }
                filmCount--;
                Console.WriteLine("Видалено!");
            }
            else
            {
                Console.WriteLine("Невірний номер!");
            }
        }

        //видалення фільмів за жанром
        static void DeleteFilmByGenre()
        {
            Console.Write("Жанр для видалення (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
            int genreChoice = ReadGenreChoice();
            FilmGenre genreToDelete = (FilmGenre)(genreChoice - 1);
            int deletedCount = 0;
            for (int i = filmCount - 1; i >= 0; i--)
            {
                if (films[i].Genre == genreToDelete)
                {
                    for (int j = i; j < filmCount - 1; j++)
                    {
                        films[j] = films[j + 1];
                    }
                    filmCount--;
                    deletedCount++;
                }
            }
            if (deletedCount > 0)
            {
                Console.WriteLine($"Видалено {deletedCount} фільм(ів) жанру {GetGenreName(genreToDelete)}!");
            }
            else
            {
                Console.WriteLine($"Фільми жанру {GetGenreName(genreToDelete)} не знайдені!");
            }
        }

        //отримання назви жанру
        private static string GetGenreName(FilmGenre genre)
        {
            switch (genre)
            {
                case FilmGenre.ACTION: return "Бойовик";
                case FilmGenre.COMEDY: return "Комедія";
                case FilmGenre.DRAMA: return "Драма";
                case FilmGenre.HORROR: return "Жахи";
                case FilmGenre.ROMANCE: return "Романтика";
                default: return "Невідомий";
            }
        }

        //валідація вибору методу видалення
        private static int ReadDeleteMethodChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 2)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 2: ");
            }
        }

        //зчитування цілих чисел
        public static int ReadInt()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result))
                    return result;
                Console.Write("Помилка! Введіть ціле число: ");
            }
        }

        //зчитування додатних цілих чисел
        public static int ReadPositiveInt()
        {
            while (true)
            {
                int result = ReadInt();
                if (result > 0)
                    return result;
                Console.Write("Помилка! Введіть додатне число: ");
            }
        }

        //зчитування дійсних чисел
        public static double ReadDouble()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double result))
                    return result;
                Console.Write("Помилка! Введіть число (наприклад 7,5): ");
            }
        }

        //зчитування вибору жанру
        private static int ReadGenreChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 5)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 5: ");
            }
        }

        //зчитування вибору пошуку
        private static int ReadSearchChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 2)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 2: ");
            }
        }

        //зчитування вибору демонстрації
        private static int ReadDemoChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 4)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 4: ");
            }
        }

        //зчитування індексу для видалення
        private static int ReadDeleteIndex()
        {
            while (true)
            {
                int index = ReadInt();
                if (index >= 1 && index <= filmCount)
                    return index;
                Console.Write($"Помилка! Введіть число від 1 до {filmCount}: ");
            }
        }
    }
}