using ConsoleApp;

internal class Program
{
    static List<Ship> ships = new List<Ship>();
    static List<Container> containers = new List<Container>();
    static Dictionary<string, double> products = new Dictionary<string, double>()
           {
               {"Bananas", 13.3 },
               {"Chocolate", 18 },
               {"Fish", 2 },
               {"Meat", -15 },
               {"Ice cream", -18 },
               {"Frozen pizza", -30 },
               {"Cheese", 7.2 },
               {"Sausages", 5 },
               {"Butter", 20.5 },
               {"Eggs", 19 }
           };

    private static void Main(string[] args)
    {
        InitializeExampleData();

        while (true)
        {
            Console.Clear();

            DisplayMainMenu();

            Console.WriteLine("Dostępne opcje:");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddShip();
                    break;
                case "2":
                    RemoveShip();
                    break;
                case "3":
                    AddContainer();
                    break;
                case "4":
                    RemoveContainer();
                    break;
                case "5":
                    LoadCargoToContainer();
                    break;
                case "6":
                    UnloadCargoFromContainer();
                    break;
                case "7":
                    LoadContainerOnShip();
                    break;
                case "8":
                    UnloadContainerFromShip();
                    break;
                case "9":
                    ReplaceContainerOnShip();
                    break;
                case "10":
                    MoveContainerBetweenShips();
                    break;
                case "11":
                    DisplayInformationAboutShip();
                    break;
                case "12":
                    DisplayInformationAboutContainer();
                    break;
                case "0":
                    return;
                default:
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void DisplayInformationAboutContainer()
    {
        Console.WriteLine("Podaj numer seryjny kontenera o którym chcesz wyświetlić informację: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();

        var container = containers.Find(c => c.SerialNumber.Equals(containerSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (container == null)
        {
            Console.WriteLine("Kontener o podanym numerze seryjnym nie istnieje");
        }
        else
        {
            container.ShowInformation();
        }
        Console.ReadKey();
    }

    private static void DisplayInformationAboutShip()
    {
        Console.Write("Podaj nazwę statku o którym chcesz wyświetlić informacje: ");
        string shipName = Console.ReadLine()?.Trim();

        var ship = ships.Find(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));

        if (ship == null)
        {
            Console.WriteLine("Statek o takiej nazwie nie istnieje.");
        }
        else
        {
            ship.ShowInformation();
        }
        Console.ReadKey();
    }

    private static void MoveContainerBetweenShips()
    {
        Console.Write("Podaj nazwę statku źródłowego: ");
        string sourceShipName = Console.ReadLine()?.Trim();
        var sourceShip = ships.Find(s => s.Name.Equals(sourceShipName, StringComparison.OrdinalIgnoreCase));

        if (sourceShip == null)
        {
            Console.WriteLine("Statek źródłowy nie istnieje.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj nazwę statku docelowego: ");
        string destinationShipName = Console.ReadLine()?.Trim();
        var destinationShip = ships.Find(s => s.Name.Equals(destinationShipName, StringComparison.OrdinalIgnoreCase));

        if (destinationShip == null)
        {
            Console.WriteLine("Statek docelowy nie istnieje.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera do przeniesienia: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();

        try
        {
            sourceShip.TransferContainerTo(destinationShip, containerSerialNumber);
            Console.WriteLine($"Kontener {containerSerialNumber} został przeniesiony na statek {destinationShip.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd przy próbie przeniesienia: {ex.Message}");
        }

        Console.ReadKey();
    }

    private static void ReplaceContainerOnShip()
    {
        Console.Write("Podaj nazwę statku: ");
        string shipName = Console.ReadLine()?.Trim();
        var ship = ships.Find(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));

        if (ship == null)
        {
            Console.WriteLine("Statek o takiej nazwie nie istnieje.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera do podmiany na statku: ");
        string containerOnShipSerialNumber = Console.ReadLine()?.Trim();
        var containerOnShip = ship.Containers.Find(c => c.SerialNumber.Equals(containerOnShipSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (containerOnShip == null)
        {
            Console.WriteLine("Kontener nie znaleziony na statku.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera do podmiany z magazynu: ");
        string containerAtStorageSerialNumber = Console.ReadLine()?.Trim();
        var containerAtStorage = containers.Find(c => c.SerialNumber.Equals(containerAtStorageSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (containerAtStorage == null)
        {
            Console.WriteLine("Kontener nie znaleziony w magazynie.");
            Console.ReadKey();
            return;
        }

        try
        {
            ship.ReplaceContainer(containerOnShipSerialNumber, containerAtStorage);
            containers.Add(containerOnShip);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd przy próbie podmiany: {ex.Message}");
        }
    }

    private static void UnloadContainerFromShip()
    {
        Console.Write("Podaj nazwę statku: ");
        string shipName = Console.ReadLine()?.Trim();
        var ship = ships.Find(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));

        if (ship == null)
        {
            Console.WriteLine("Statek o takiej nazwie nie istnieje.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera do rozładunku: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();
        var container = ship.Containers.Find(c => c.SerialNumber.Equals(containerSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (container == null)
        {
            Console.WriteLine("Kontener o takim numerze seryjnym nie znajduje się na tym statku.");
            Console.ReadKey();
            return;
        }

        try
        {
            ship.UnloadContainer(containerSerialNumber);
            containers.Add(container);
            Console.WriteLine($"Kontener {containerSerialNumber} został rozładowany z statku {ship.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd przy próbie rozładunku: {ex.Message}");
        }

        Console.ReadKey();
    }

    private static void LoadContainerOnShip()
    {
        Console.Write("Podaj nazwę statku: ");
        string shipName = Console.ReadLine()?.Trim();
        var ship = ships.Find(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));

        if (ship == null)
        {
            Console.WriteLine("Statek o takiej nazwie nie istnieje.");
            Console.ReadKey();
            return;
        }

        Console.Write("Podaj numer seryjny kontenera do załadowania: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();
        var container = containers.Find(c => c.SerialNumber.Equals(containerSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (container == null)
        {
            Console.WriteLine("Kontener o takim numerze seryjnym nie istnieje.");
            Console.ReadKey();
            return;
        }

        try
        {
            ship.LoadContainer(container);
            containers.Remove(container);
            Console.WriteLine($"Kontener {containerSerialNumber} został załadowany na statek {ship.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }

        Console.ReadKey();
    }

    private static void UnloadCargoFromContainer()
    {
        Console.Write("Podaj numer seryjny kontenera: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();
        var container = containers.Find(c => c.SerialNumber.Equals(containerSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (container == null)
        {
            Console.WriteLine("Kontener o takim numerze seryjnym nie istnieje.");
            Console.ReadKey();
            return;
        }

        container.Unload();
        Console.WriteLine($"Wszystkie ładunki zostały rozładowane z kontenera {container.SerialNumber}.");

        Console.ReadKey();
    }

    private static void LoadCargoToContainer()
    {
        Console.Write("Podaj numer seryjny kontenera: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();
        var container = containers.Find(c => c.SerialNumber.Equals(containerSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (container == null)
        {
            Console.WriteLine("Kontener o takim numerze seryjnym nie istnieje.");
            Console.ReadKey();
            return;
        }

        double cargoWeight;

        while (true)
        {
            Console.Write("Podaj wagę ładunku do załadowania (kg): ");
            string input = Console.ReadLine()?.Trim();

            if (double.TryParse(input, out cargoWeight) && cargoWeight > 0)
            {
                try
                {
                    container.Load(cargoWeight);
                    Console.WriteLine($"Załadowano {cargoWeight} kg do kontenera {container.SerialNumber}.");
                }
                catch (OverfillException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            }
            else
            {
                Console.WriteLine("Proszę wprowadzić poprawną wagę.");
            }
        }
        Console.ReadKey();
    }

    private static void RemoveContainer()
    {
        Console.Write("Podaj numer seryjny kontenera który chcesz zniszczyć: ");
        string containerSerialNumber = Console.ReadLine()?.Trim();

        var container = containers.Find(c => c.SerialNumber.Equals(containerSerialNumber, StringComparison.OrdinalIgnoreCase));

        if (container == null)
        {
            Console.WriteLine("Nie znaleziono takiego kontenera");
        }
        else
        {
            containers.Remove(container);
            Console.WriteLine($"Kontener {containerSerialNumber} został usunięty");
        }

        Console.ReadKey();
    }

    private static void AddContainer()
    {
        int[] validChoices = { 1, 2, 3 };

        Console.WriteLine("Wybierz typ konteneru:");
        Console.WriteLine("1. Kontener na płyny (L)");
        Console.WriteLine("2. Kontener na gaz (G)");
        Console.WriteLine("3. Kontener chłodniczy (C)");

        string containerInputType = Console.ReadLine()?.Trim();

        if (!int.TryParse(containerInputType, out int containerType) || !validChoices.Contains(containerType))
        {
            Console.WriteLine("Niepoprawny typ kontenera.");
            return;
        }

        double GetDoubleInput(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string userInput = Console.ReadLine()?.Trim();

                if (double.TryParse(userInput, out double value))
                    return value;

                Console.WriteLine("Nieprawidłowa wartość. Wprowadź ponownie.");
            }
        }


        double containerSelfWeight = GetDoubleInput("Wprowadź wagę pustego kontenera (kg): ");

        double containerMaxLoadCapacity = GetDoubleInput("Wprowadź maksymalną masę ładunku (kg): ");

        double containerHeight = GetDoubleInput("Wprowadź wysokość kontenera (cm): ");

        double containerDepth = GetDoubleInput("Wprowadź głębokość kontenera (cm): ");

        Container newContainer;

        switch (containerType)
        {
            // Liquid
            case 1:
                Console.WriteLine("Czy będzie kontenerem na ładunek niebiezpieczny? (Tak/Nie)");
                bool containerIsHazardous;

                while (true)
                {
                    string isHazardousInput = Console.ReadLine()?.Trim();
                    if (string.Equals(isHazardousInput, "Tak", StringComparison.OrdinalIgnoreCase))
                    {
                        containerIsHazardous = true;
                        break;
                    }
                    else if (string.Equals(isHazardousInput, "Nie", StringComparison.OrdinalIgnoreCase))
                    {
                        containerIsHazardous = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Błędnie wprowadzona informacja. Spróbuj ponownie.");
                    }
                }

                newContainer = new LiquidContainer(containerSelfWeight, containerMaxLoadCapacity, containerHeight, containerDepth, containerIsHazardous);
                break;

            // Gas
            case 2:
                double containerPressure = GetDoubleInput("Wprowadź ciśnienie w kontenerze (atmosfery): ");

                newContainer = new GasContainer(containerSelfWeight, containerMaxLoadCapacity, containerHeight, containerDepth, containerPressure);
                break;

            // Refrigerated
            case 3:
                Console.WriteLine("Jakie produkty będą przechowywane w kontenerze: ");
                foreach (var product in products)
                {
                    Console.WriteLine(product.Key);
                }

                string containerProduct;
                double containerMinTemperature;

                while (true)
                {
                    string productInput = Console.ReadLine()?.Trim();

                    if (products.TryGetValue(productInput, out containerMinTemperature))
                    {
                        containerProduct = productInput;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Produkt nieobsługiwany. Wprowadź produkt z listy dostępnych.");
                    }
                }

                newContainer = new RefrigeratedContainer(containerSelfWeight, containerMaxLoadCapacity, containerHeight, containerDepth, containerProduct, containerMinTemperature, containerMinTemperature);
                break;

            default:
                Console.WriteLine("Niepoprawny typ kontenera.");
                return;
        }

        containers.Add(newContainer);
        Console.WriteLine("Kontener został dodany.");
        Console.ReadKey();
    }


    private static void RemoveShip()
    {
        Console.Write("Podaj nazwę statku który chcesz zniszczyć:  ");
        string shipName = Console.ReadLine()?.Trim();

        var ship = ships.Find(s => s.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));

        if (ship == null)
        {
            Console.WriteLine("Nie znaleziono takiego statku");
        }
        else
        {
            ships.Remove(ship);
            Console.WriteLine($"Statek {shipName} został usunięty");
        }

        Console.ReadKey();
    }

    private static void AddShip()
    {
        Console.Write("Podaj nazwę kontenerowca: ");
        string shipName = Console.ReadLine();

        Console.Write("Podaj maksymalną liczbę kontenerów, które mogą być przewożone: ");
        int maxContainerCapacity = int.Parse(Console.ReadLine());

        Console.Write("Podaj maksymalną możliwa wagę wszystkich kontenerów jakie mogą być transportowane (w tonach): ");
        double maxLoadCapacity = double.Parse(Console.ReadLine());

        Console.Write("Podaj maksymalną prędkość jaką kontenerowiec może rozwinąć (w węzłach): ");
        double maxSpeed = double.Parse(Console.ReadLine());

        Ship newShip = new Ship(shipName, maxContainerCapacity, maxLoadCapacity, maxSpeed);
        ships.Add(newShip);
        Console.WriteLine($"Kontenerowiec {shipName} został dodany");

        Console.ReadKey();
    }

    private static void DisplayMainMenu()
    {
        Console.WriteLine("_____ System do zarządzania załadunkiem kontenerów _____");

        var numberOfShips = ships.Count;
        Console.WriteLine("Lista kontenerowców:");
        if (numberOfShips == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            foreach (Ship ship in ships)
            {
                Console.WriteLine($"{ship.Name} (speed = {ship.MaxSpeed}, maxContainerNum = {ship.MaxContainerCapacity}, maxWeight = {ship.MaxLoadCapacity})");

                string serialNumbers = string.Join(", ", ship.Containers.Select(container => container.SerialNumber));
                Console.WriteLine($"    {serialNumbers}");
            }
        }


        var numberOfContainers = containers.Count;
        Console.WriteLine("\nLista kontenerów:");
        if (numberOfContainers == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            foreach (Container container in containers)
            {
                Console.WriteLine($"{container.SerialNumber} (type = {container.GetType().Name}, load = {container.CargoWeight}, maxLoad = {container.MaxLoadCapacity})");
            }
        }

        Console.WriteLine("\nMożliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usuń kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");
        Console.WriteLine("4. Usuń kontener");
        Console.WriteLine("5. Załaduj ładunek do kontenera");
        Console.WriteLine("6. Rozładuj ładunek z kontenera");
        Console.WriteLine("7. Załaduj kontener na statek");
        Console.WriteLine("8. Rozładuj kontener ze statku");
        Console.WriteLine("9. Podmień kontener na statku");
        Console.WriteLine("10. Przenieś kontener pomiędzy statkami");
        Console.WriteLine("11. Wyświetl informację o statku");
        Console.WriteLine("12. Wyświetl informację o kontenerze");
        Console.WriteLine("0. Wyjdź");
    }

    private static void InitializeExampleData()
    {
        ships.Add(new Ship("Borsuk", 15, 3000, 300));
        ships.Add(new Ship("Bocian", 10, 2000, 330));

        var liquidContainerHazardous = new LiquidContainer(200, 1500, 300, 1000, true);
        liquidContainerHazardous.Load(500);
        var liquidContainerNotHazardous = new LiquidContainer(200, 1500, 300, 1000, false);
        liquidContainerNotHazardous.Load(1300);
        var gasContainer = new GasContainer(500, 4000, 300, 1000, 15);
        gasContainer.Load(100);

        ships[0].LoadContainer(liquidContainerHazardous);
        ships[0].LoadContainer(gasContainer);
        ships[1].LoadContainer(liquidContainerNotHazardous);


        var refrigeratedContainerEggs = new RefrigeratedContainer(1000, 7000, 300, 1000, "Eggs", 10, 10);
        refrigeratedContainerEggs.Load(500);
        var refrigeratedContainerCheese = new RefrigeratedContainer(1000, 5000, 300, 1000, "Cheese", 3, 3);


        containers.Add(refrigeratedContainerEggs);
        containers.Add(refrigeratedContainerCheese);
    }
}


