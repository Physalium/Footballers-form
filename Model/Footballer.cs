using System;

namespace Footballers.Model
{
    internal class Footballer
    {
        #region prop

        private string forename;
        private string surname;
        private double age;
        private double weight;

        public string Forename { get => forename; set => forename = value; }
        public string Surname { get => surname; set => surname = value; }
        public double Age { get => age; set => age = value; }
        public double Weight { get => weight; set => weight = value; }

        #endregion prop

        #region constructor

        public Footballer()
        {
        }

        public Footballer(string forename, string surname, double age, double weight)
        {
            this.Forename = forename;
            this.Age = age;
            this.Surname = surname;
            this.Weight = weight;
        }

        #endregion constructor

        public override string ToString()
        {
            return $"{Forename} {Surname}, Wiek: {Age} lat, {Weight}kg";
        }

        

        

        public void Copy(Footballer pilkarz)
        {
            Forename = pilkarz.Forename;
            Surname = pilkarz.Surname;
            Age = pilkarz.Age;
            Weight = pilkarz.Weight;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Footballer footballer = obj as Footballer;
            return (this.Age == footballer.Age && this.Forename == footballer.Forename && this.Surname == footballer.Surname
                && this.Weight == footballer.Weight);
        }
    }
}