namespace ConsoleApp
{
    internal abstract class Container
    {

        private static int counter = 1;

        public string SerialNumber { get; }

        // kg
        public double CargoWeight { get; set; }

        // kg
        public double SelfWeight { get; }

        // kg
        public double MaxLoadCapacity { get; }

        // cm
        public double Height { get; }

        // cm
        public double Depth { get; }

        protected Container(double selfWeight,
                            double maxLoadCapacity,
                            double height,
                            double depth)
        {
            this.SerialNumber = CreateSerialNumber();
            this.CargoWeight = 0;
            this.SelfWeight = selfWeight;
            this.MaxLoadCapacity = maxLoadCapacity;
            this.Height = height;
            this.Depth = depth;
        }

        protected static int GetUniqueId()
        {
            return counter++;
        }

        protected abstract string CreateSerialNumber();

        public double TotalWeight()
        {
            return CargoWeight + SelfWeight;
        }

        public virtual void Load(double weight)
        {
            if (CargoWeight + weight > MaxLoadCapacity)
            {
                throw new OverfillException("Przekroczono maksymalną ładowność kontenera!");
            }
            CargoWeight += weight;
        }

        public virtual void Unload()
        {
            CargoWeight = 0;
        }

        public virtual void ShowInformation()
        {
            Console.WriteLine($"Numer seryjny: {SerialNumber}");
            Console.WriteLine($"Masa ładunku (kg): {CargoWeight}");
            Console.WriteLine($"Waga własna (kg): {SelfWeight}");
            Console.WriteLine($"Maksymalna ładowność (kg): {MaxLoadCapacity}");
            Console.WriteLine($"Wysokość (cm): {Height}");
            Console.WriteLine($"Głębokość (cm): {Depth}");
        }
    }
}
