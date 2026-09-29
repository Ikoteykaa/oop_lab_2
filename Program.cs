using System;

namespace oop_lab_1
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int N = 0;
            while (true)
            {
                Console.Write("Введіть максимальну кількість ігор, яку можна зберегти (N > 0): ");
                if (int.TryParse(Console.ReadLine(), out N) && N > 0)
                {
                    break;
                }
                Console.WriteLine("Помилка! Введіть ціле додатне число.");
            }

            VideoGames[] games = new VideoGames[N];
            int gameCount = 0;
            int doChoice;

            do
            {
                Console.WriteLine("\n ГОЛОВНЕ МЕНЮ ");
                Console.WriteLine("1 - Додати об'єкт (Гру)");
                Console.WriteLine("2 - Переглянути всі об'єкти");
                Console.WriteLine("3 - Знайти об'єкт");
                Console.WriteLine("4 - Продемонструвати поведінку");
                Console.WriteLine("5 - Видалити об'єкт");
                Console.WriteLine("0 - Вийти з програми");
                Console.Write("Ваш вибір: ");

                if (!int.TryParse(Console.ReadLine(), out doChoice))
                {
                    Console.WriteLine("Потрібно ввести число!");
                    continue;
                }

                switch (doChoice)
                {
                    case 1:
                        if (gameCount >= games.Length)
                        {
                            Console.WriteLine($"\nМасив заповнений! Ви не можете додати більше ніж {N} ігор.");
                            break;
                        }

                        VideoGames newGame = AddGame();
                        if (newGame != null)
                        {
                            games[gameCount] = newGame;
                            gameCount++;
                            Console.WriteLine("\nГру успішно додано!");
                        }
                        break;

                    case 2:
                        PrintGamesTable(games, gameCount);
                        break;

                    case 3:
                        SearchGames(games, gameCount);
                        break;

                    case 4:
                        DemonstrateBehavior(games, gameCount);
                        break;

                    case 5:
                        gameCount = DeleteGame(games, gameCount);
                        break;

                    case 0:
                        Console.WriteLine("\nПрограму завершено. До побачення!");
                        break;

                    default:
                        Console.WriteLine("\nНекоректний вибір! Оберіть пункт від 0 до 5.");
                        break;
                }

            } while (doChoice != 0);
        }

        static VideoGames AddGame()
        {
            Console.WriteLine("\n Додавання нової гри");
            VideoGames newGame = new VideoGames();
            bool isValid = false;

            while (!isValid)
            {
              
                try
                {
                    Console.Write("Введіть назву гри (2-50 символів): ");
                    string name = Console.ReadLine();
                    newGame.Name = name;
                    Console.Write("Оберіть жанр гри (1-Action, 2-Adventure, 3-RPG, 4-Strategy, 5-Sport, 6-Horror, 7-Simulator): ");
                    GameGenre genre = (GameGenre)int.Parse(Console.ReadLine());
                    newGame.Genre = genre;
                    Console.Write("Введіть рейтинг гри (від 0 до 5, напр. 4,5): ");
                    double rating = double.Parse(Console.ReadLine());
                    newGame.Rating = rating;
                    Console.Write("Введіть дату релізу (рррр-мм-дд): ");
                    DateOnly releaseYear = DateOnly.Parse(Console.ReadLine());
                    newGame.ReleaseYear = releaseYear;  
                    Console.Write("Введіть ціну гри (більше 0): ");
                    double price = double.Parse(Console.ReadLine());
                    newGame.Price = price;  
                    isValid = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"\nПомилка значення: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nПомилка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nПомилка формату! Перевірте правильність введення чисел або дат.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nНевідома помилка: {ex.Message}");
                }

                if (newGame == null)
                {
                    Console.WriteLine("Спробуйте ввести дані ще раз.\n");
                }
            }

            return newGame;
        }

        static void PrintGamesTable(VideoGames[] games, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("\nСписок ігор порожній! Спочатку додайте об'єкти.");
                return;
            }

            Console.WriteLine("\n============================================= СПИСОК ІГОР ==============================================");
            Console.WriteLine($"| {"№",-2} | {"Назва",-20} | {"Жанр",-12} | {"Рейтинг",-7} | {"Дата релізу",-11} | {"Ціна",-8} | {"Гравці",-6} | {"TopTier",-7} |");
            Console.WriteLine(new string('-', 96));

            for (int i = 0; i < count; i++)
            {

                Console.WriteLine($"| {i + 1,-2} | {games[i].Name,-20} | {games[i].Genre,-12} | {games[i].Rating,-7:F1} | {games[i].ReleaseYear,-11} | {games[i].Price,-8:F2} | {games[i].CountPlayers,-6} | {games[i].IsTopTier,-7} |");
            }
            Console.WriteLine(new string('-', 96));
        }

        static void SearchGames(VideoGames[] games, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("\nНемає ігор для пошуку.");
                return;
            }

            Console.WriteLine("\nПошук");
            Console.WriteLine("1 - За жанром");
            Console.WriteLine("2 - За роком випуску");
            Console.Write("Оберіть критерій: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)) return;

            bool found = false;

            if (choice == 1)
            {
                Console.WriteLine("Оберіть жанр (1-Action, 2-Adventure, 3-RPG, 4-Strategy, 5-Sport, 6-Horror, 7-Simulator): ");
                if (int.TryParse(Console.ReadLine(), out int gChoice) && gChoice >= 1 && gChoice <= 7)
                {
                    GameGenre searchGenre = (GameGenre)gChoice;
                    Console.WriteLine("\nРезультати пошуку:");
                    PrintHeader();
                    for (int i = 0; i < count; i++)
                    {
                        if (games[i].Genre == searchGenre)
                        {
                            PrintRow(games[i], i);
                            found = true;
                        }
                    }
                }
            }
            else if (choice == 2)
            {
                Console.Write("Введіть рік випуску (напр. 2020): ");
                if (int.TryParse(Console.ReadLine(), out int searchYear))
                {
                    Console.WriteLine("\nРезультати пошуку:");
                    PrintHeader();
                    for (int i = 0; i < count; i++)
                    {
                        if (games[i].ReleaseYear.Year == searchYear)
                        {
                            PrintRow(games[i], i);
                            found = true;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Некоректний вибір критерію.");
                return;
            }

            if (!found) Console.WriteLine("\nЖодної гри за цим критерієм не знайдено!");
            else Console.WriteLine(new string('-', 96));
        }

        static void DemonstrateBehavior(VideoGames[] games, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("\nСписок порожній.");
                return;
            }

            PrintGamesTable(games, count);
            Console.Write("\nВведіть номер гри для взаємодії (або 0 для відміни): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= count)
            {
                VideoGames selectedGame = games[index - 1];
                int subChoice = -1;

                while (subChoice != 0)
                {
                    Console.WriteLine($"\n--- Дії з грою: {selectedGame.Name} ---");
                    Console.WriteLine("1 - Запустити гру (додати гравця)");
                    Console.WriteLine("2 - Вийти з гри (відняти гравця)");
                    Console.WriteLine("3 - Змінити рейтинг");
                    Console.WriteLine("0 - Повернутися до головного меню");
                    Console.Write("Вибір: ");

                    if (int.TryParse(Console.ReadLine(), out subChoice))
                    {
                        switch (subChoice)
                        {
                            case 1:
                                selectedGame.StartGame();
                                Console.WriteLine("Гру запущено! Кількість гравців збільшено.");
                                break;
                            case 2:
                                if (selectedGame.ExitGame())
                                    Console.WriteLine("Один гравець вийшов з гри.");
                                else
                                    Console.WriteLine("Помилка! У грі наразі немає активних гравців.");
                                break;
                            case 3:
                                Console.Write("Введіть новий рейтинг (0 - 5): ");
                                try
                                {
                                    double newRating = double.Parse(Console.ReadLine());
                                    selectedGame.Rating = newRating;
                                    Console.WriteLine("Рейтинг успішно оновлено!");
                                }
                                catch (ArgumentOutOfRangeException ex)
                                {
                                    Console.WriteLine($"\nПомилка: {ex.Message}");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nПомилка: Введено некоректний формат числа.");
                                }
                                break;
                            case 0:
                                break;
                            default:
                                Console.WriteLine("Некоректна дія.");
                                break;
                        }
                    }
                }
            }
        }

        static int DeleteGame(VideoGames[] games, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("\nНемає об'єктів для видалення.");
                return count;
            }

            Console.WriteLine("\nВидалення гри");
            Console.WriteLine("1 - Видалити за номером у таблиці");
            Console.WriteLine("2 - Видалити всі ігри певного жанру");
            Console.WriteLine("0 - Відміна");
            Console.Write("Ваш вибір: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)) return count;

            if (choice == 1)
            {
                PrintGamesTable(games, count);
                Console.Write("\nВведіть номер гри для видалення: ");
                if (int.TryParse(Console.ReadLine(), out int id) && id > 0 && id <= count)
                {
                    for (int i = id - 1; i < count - 1; i++)
                    {
                        games[i] = games[i + 1];
                    }
                    games[count - 1] = null;
                    count--;
                    Console.WriteLine("\nОб'єкт успішно видалено!");
                }
                else
                {
                    Console.WriteLine("\nНекоректний номер!");
                }
            }
            else if (choice == 2)
            {
                Console.WriteLine("Оберіть жанр (1-Action, 2-Adventure, 3-RPG, 4-Strategy, 5-Sport, 6-Horror, 7-Simulator): ");
                if (int.TryParse(Console.ReadLine(), out int gChoice) && gChoice >= 1 && gChoice <= 7)
                {
                    GameGenre delGenre = (GameGenre)gChoice;
                    int initialCount = count;

                    for (int i = count - 1; i >= 0; i--)
                    {
                        if (games[i].Genre == delGenre)
                        {
                            for (int j = i; j < count - 1; j++)
                            {
                                games[j] = games[j + 1];
                            }
                            games[count - 1] = null;
                            count--;
                        }
                    }

                    if (initialCount == count)
                        Console.WriteLine($"\nІгор з жанром {delGenre} не знайдено!");
                    else
                        Console.WriteLine($"\nУспішно видалено {initialCount - count} об'єкт(ів).");
                }
            }

            return count;
        }

        static void PrintHeader()
        {
            Console.WriteLine(new string('-', 96));
            Console.WriteLine($"| {"№",-2} | {"Назва",-20} | {"Жанр",-12} | {"Рейтинг",-7} | {"Дата релізу",-11} | {"Ціна",-8} | {"Гравці",-6} | {"TopTier",-7} |");
            Console.WriteLine(new string('-', 96));
        }

        static void PrintRow(VideoGames game, int originalIndex)
        {
            Console.WriteLine($"| {originalIndex + 1,-2} | {game.Name,-20} | {game.Genre,-12} | {game.Rating,-7:F1} | {game.ReleaseYear,-11} | {game.Price,-8:F2} | {game.CountPlayers,-6} | {game.IsTopTier,-7} |");
        }
    }
}