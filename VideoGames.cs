using System;

namespace oop_lab_1
{
    internal class VideoGames
    {
 
        private string _name;
        private GameGenre _genre;
        private double _rating;
        private DateOnly _releaseYear;
        private double _price;


        public string Publisher { get; set; } = "Unknown";


        public int CountPlayers { get; private set; }


        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
                    throw new ArgumentException("Назва має містити від 2 до 50 символів.");
                _name = value;
            }
        }

        public GameGenre Genre
        {
            get { return _genre; }
            set
            {
                if (!Enum.IsDefined(typeof(GameGenre), value))
                    throw new ArgumentException("Недопустимий жанр гри.");
                _genre = value;
            }
        }

        public double Rating
        {
            get { return _rating; }
            set
            {
                if (value < 0 || value > 5)
                    throw new ArgumentOutOfRangeException("Rating", "Рейтинг повинен бути числом від 0 до 5.");
                _rating = value;
            }
        }

        public DateOnly ReleaseYear
        {
            get { return _releaseYear; }
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Now))
                    throw new ArgumentException("Дата релізу не може бути у майбутньому.");
                _releaseYear = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Ціна повинна бути більшою за 0.");
                _price = value;
            }
        }

 
        public bool IsTopTier => Rating >= 4.5 && CountPlayers > 0;

        //public VideoGames(string name, GameGgenre genre, double rating, DateOnly release_year, double price)
        //{

        //    Name = name;
        //    Genre = genre;
        //    Rating = rating;
        //    ReleaseYear = release_year;
        //    Price = price;
        //    CountPlayers = 0;
        //}

 
        private void ChangePlayerCount(int delta)
        {
            CountPlayers += delta;
        }

        public void StartGame()
        {
            ChangePlayerCount(1);
        }

        public bool ExitGame()
        {
            if (CountPlayers > 0)
            {
                ChangePlayerCount(-1);
                return true;
            }
            return false;
        }
    }
}