using System;

namespace Memento
{
    class GameWorld
    {
        public string Name { get; set; }
        public long Population { get; set; }

        
        public GameWorldMemento Save()
        {
            return new GameWorldMemento { Name = this.Name, Population = this.Population }; 
        }

        public void Undo(GameWorldMemento memento)
        {
            this.Name = memento.Name;
            this.Population = memento.Population;
        }

        public override string ToString()
        {
            return String.Format("{0} dünyasında {1} insan var", Name, Population);
        }
    }
   // GameWorldMemento(Memento class): T anında save etmek istediğimiz obyektin saxlanacagi  class . hansi özellikleri saxxlamaq isteyersek onlara uygun propertyler tanimlamaq yeterli olacaq.
      class GameWorldMemento
    {
        public string Name { get; set; }
        public long Population { get; set; }
    }
    class GameWorldCareTaker
    {
        public GameWorldMemento World { get; set; } //mementoyu döner.
    }
    class Program
    {

        static void Main(string[] args)
        {
            GameWorld zula = new GameWorld { Name = "Zula", Population = 100000 };

            Console.WriteLine(zula.ToString());

            GameWorldCareTaker taker = new GameWorldCareTaker();
            taker.World = zula.Save(); //obyektin hal hazirki halini saxlayir

            zula.Population += 10;
            Console.WriteLine(zula.ToString());

            zula.Undo(taker.World); //obyekyin kohne versiyasina qayidir
            Console.WriteLine(zula.ToString());
        }

    }
}
