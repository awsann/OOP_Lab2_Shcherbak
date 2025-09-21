using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Film
    {
        private string title;
        private string director;
        private int releaseYear;
        private double rating;
        private FilmGenre genre;

        //НОВЕ: Обчислювана властивість для автоматичного розрахунку років з прем'єри
        public int YearsFromPremiere
        {
            get { return 2025 - releaseYear; }
        }

        public string Title
        {
            get { return title; }
            set
            {
                // ЗМІНЕНО: Замінено цикл while з Console.ReadLine на викидання винятку
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 20)
                {
                    throw new ArgumentException("Помилка! Назва від 3 до 20 символів. Введіть ще раз: ");
                }
                title = value.Trim();
            }
        }

        public string Director
        {
            get { return director; }
            set
            {
                // ЗМІНЕНО: Замінено цикл while з Console.ReadLine на викидання винятку
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 20 || !IsOnlyLetters(value))
                {
                    throw new ArgumentException("Помилка! Режисер від 3 до 20 букв (тільки літери). Введіть ще раз: ");
                }
                director = value.Trim();
            }
        }

        //перевірка чи містить тільки букви та пробіли
        private bool IsOnlyLetters(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return false;
            }
            return true;
        }

        public int ReleaseYear
        {
            get { return releaseYear; }
            set
            {
                //ЗМІНЕНО: Замінено цикл while з Console.ReadLine на викидання винятку
                if (value < 1960 || value > 2025)
                {
                    throw new ArgumentException("Помилка! Рік від 1960 до 2025. Введіть ще раз: ");
                }
                releaseYear = value;
            }
        }

        public double Rating
        {
            get { return rating; }
            set
            {
                //ЗМІНЕНО: Замінено цикл while з Console.ReadLine на викидання винятку
                if (value < 0.0 || value > 10.0)
                {
                    throw new ArgumentException("Помилка! Рейтинг від 0.0 до 10.0. Введіть ще раз: ");
                }
                rating = value;
            }
        }

        public FilmGenre Genre
        {
            get { return genre; }
            set
            {
                //НОВЕ: Додано валідацію enum значень
                if (!Enum.IsDefined(typeof(FilmGenre), value))
                {
                    throw new ArgumentException("Помилка! Невірне значення жанру.");
                }
                genre = value;
            }
        }

        //НОВЕ: Методи для визначення категорії фільму за віком
        public bool IsClassic()
        {
            return YearsFromPremiere >= 20; //класика якщо старше 20 років
        }

        public bool IsModern()
        {
            return YearsFromPremiere <= 5; //сучасний якщо молодше 5 років
        }

        public string GetAgeCategory()
        {
            if (YearsFromPremiere >= 20) return "Класичний";
            else if (YearsFromPremiere >= 5) return "Недавній";
            else return "Сучасний";
        }

        //конструктор за замовчуванням
        public Film()
        {
            title = "";
            director = "";
            releaseYear = 2024;
            rating = 0.0;
            genre = FilmGenre.DRAMA;
        }

        //метод відображення інформації
        public void DisplayInfo()
        {
            Console.WriteLine($"Назва: {title}, Режисер: {director}, Рік: {releaseYear}, Рейтинг: {rating:F1}, Жанр: {GetGenreName()}");
        }

        //отримання назви жанру
        private string GetGenreName()
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

        //перевірка високого рейтингу
        public bool IsHighRated()
        {
            return rating >= 8.0;
        }
    }
}