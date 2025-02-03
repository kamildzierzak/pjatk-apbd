namespace ConsoleApp
{
    internal class GasContainer : Container, IHazardNotifier
    {

        // atmospheres
        public double Pressure { get; set; }

        public GasContainer(double selfWeight, double maxLoadCapacity, double height, double depth, double pressure) : base(selfWeight, maxLoadCapacity, height, depth)
        {
            Pressure = pressure;
        }

        protected override string CreateSerialNumber()
        {
            return $"KON-G-{GetUniqueId()}";
        }

        public void Notify(string message)
        {
            Console.WriteLine("Kontener: " + SerialNumber);
            Console.WriteLine("Wiadomość:" + message);
        }

        public override void Load(double weight)
        {
            if (CargoWeight + weight > MaxLoadCapacity)
            {
                Notify("Podjęto próbę załadowania kontenera ponad maksymalną bezpieczną ładowność!");
                throw new OverfillException("Przekroczono maksymalną ładowność kontenera!");
            }
            CargoWeight += weight;
        }

        public override void Unload()
        {
            CargoWeight = 0.05 * CargoWeight;
        }

        public override void ShowInformation()
        {
            base.ShowInformation();
            Console.WriteLine($"Ciśnienie (atmosfery): {Pressure}");
        }
    }
}
