namespace ConsoleApp
{
    internal class Ship
    {
        public string Name { get; set; }

        // maximum number of containers
        public int MaxContainerCapacity { get; set; }

        // maximum weight of containers in ton
        public double MaxLoadCapacity { get; set; }

        // maximum speed in knots
        public double MaxSpeed { get; set; }

        // containers on ship
        public List<Container> Containers { get; set; }

        public Ship(string name, int maxContainerCapacity, double maxLoadCapacity, double maxSpeed)
        {
            Name = name;
            MaxContainerCapacity = maxContainerCapacity;
            MaxLoadCapacity = maxLoadCapacity;
            MaxSpeed = maxSpeed;
            Containers = [];
        }

        public double CurrentLoadWeight()
        {
            return Containers.Sum(container => container.TotalWeight());
        }

        public int CurrentContainerCount()
        {
            return Containers.Count();
        }

        public void LoadContainer(Container container)
        {
            if (CurrentContainerCount() > MaxContainerCapacity)
            {
                throw new InvalidOperationException("Nie można załadować kontenera. Załadowano już maksymalną możliwą ilość.");
            }

            if (CurrentLoadWeight() + container.TotalWeight() > MaxLoadCapacity * 1000)
            {
                throw new InvalidOperationException("Nie można załadować kontenera. Osiągnięto już maksymalną możliwą wagę ładunku");
            }

            Containers.Add(container);
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.Find(container => container.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));

            if (container == null)
            {
                throw new InvalidOperationException("Nie można rozładować kontenera. Kontenera nie znaleziono.");
            }
            else
            {
                Containers.Remove(container);
            }
        }

        public void ReplaceContainer(string serialNumber, Container container)
        {
            UnloadContainer(serialNumber);
            LoadContainer(container);
        }

        public void TransferContainerTo(Ship targetShip, string serialNumber)
        {
            var container = Containers.Find(container => container.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));

            if (container == null)
            {
                throw new InvalidOperationException("Nie można przenieść kontenera. Kontenera nie znaleziono.");
            }

            targetShip.LoadContainer(container);
            UnloadContainer(serialNumber);
        }

        public void ShowInformation()
        {
            string containerList = "";

            foreach (var container in Containers)
            {
                containerList += container.SerialNumber + ", ";
            }

            Console.WriteLine($"Ładunek: {containerList}");
            Console.WriteLine($"Maksymalna liczba kontenerów: {MaxContainerCapacity}");
            Console.WriteLine($"Aktualna liczba kontenerów: {CurrentContainerCount()}");
            Console.WriteLine($"Maksymalna możliwa waga kontenerów (tony): {MaxLoadCapacity}");
            Console.WriteLine($"Aktualna waga kontenerów: {CurrentLoadWeight()}");
            Console.WriteLine($"Maksymalna prędkość statku: {MaxSpeed}");

        }
    }
}
