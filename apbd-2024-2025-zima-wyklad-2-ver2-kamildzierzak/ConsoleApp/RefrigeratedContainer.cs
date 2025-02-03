namespace ConsoleApp
{
    internal class RefrigeratedContainer : Container
    {
        public string ProductType { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }

        public RefrigeratedContainer(double selfWeight, double maxLoadCapacity, double height, double depth, string productType, double temperature, double minTemperature) : base(selfWeight, maxLoadCapacity, height, depth)
        {
            ProductType = productType;
            Temperature = temperature;
            MinTemperature = minTemperature;
        }

        protected override string CreateSerialNumber()
        {
            return $"KON-C-{GetUniqueId()}";
        }

        public void SetTemperature(double temperature)
        {
            if (temperature < MinTemperature)
            {
                throw new InvalidOperationException($"Temperatura nie może być niższa niż {MinTemperature} dla {ProductType}!");
            }
            Temperature = temperature;
        }

        public override void ShowInformation()
        {
            base.ShowInformation();
            Console.WriteLine($"Rodzaj ładunku: {ProductType}");
            Console.WriteLine($"Aktualna temperatura: {Temperature}");
            Console.WriteLine($"Minimalna wymagana temperatura: {MinTemperature}");
        }
    }
}
