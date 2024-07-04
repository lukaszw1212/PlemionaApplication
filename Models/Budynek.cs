namespace PlemionaApplication.Models
{
    public class Budynek
    {
        public string Nazwa { get; set; }
        public int Poziom { get; set; }
        public int Koszt { get; set; }
        public TimeSpan CzasBudowy { get; set; }

        public Budynek(string nazwa, int koszt, TimeSpan czasBudowy)
        {
            Nazwa = nazwa;
            Poziom = 1;
            Koszt = koszt;
            CzasBudowy = czasBudowy;
        }

        public void Ulepsz()
        {
            Poziom++;
            Koszt = (int)(Koszt * 1.5); 
            CzasBudowy = TimeSpan.FromTicks((long)(CzasBudowy.Ticks * 1.5));
        }

        public override string ToString()
        {
            return $"{Nazwa} (Poziom {Poziom}): Koszt - {Koszt}, Czas budowy - {CzasBudowy.TotalMinutes} minut";
        }
    }
}
