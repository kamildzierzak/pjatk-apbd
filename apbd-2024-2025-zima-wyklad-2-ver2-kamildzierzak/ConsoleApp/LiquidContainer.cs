namespace ConsoleApp
{
    internal class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; set; }

        public LiquidContainer(double selfWeight, double maxLoadCapacity, double height, double depth, bool isHazardous) : base(selfWeight, maxLoadCapacity, height, depth)
        {
            IsHazardous = isHazardous;
        }

        protected override string CreateSerialNumber()
        {
            return $"KON-L-{GetUniqueId()}";
        }

        public void Notify(string message)
        {
            Console.WriteLine("Kontener: " + SerialNumber);
            Console.WriteLine("Wiadomość:" + message);
        }

        public override void Load(double weight)
        {
            double maxAllowedLoad = IsHazardous ? MaxLoadCapacity / 2 : MaxLoadCapacity * 0.9;

            if (IsHazardous)
            {
                if (CargoWeight + weight > maxAllowedLoad)
                {
                    Notify("Podjęto próbę załadowania kontenera ponad maksymalną bezpieczną ładowność! Kontener z niebezpieczną zawartością może być załadowany tylko do 50% jego pojemności!");
                    throw new OverfillException("Przekroczono maksymalną ładowność kontenera!");
                }
            }
            else
            {
                if (CargoWeight + weight > maxAllowedLoad)
                {
                    Notify("Podjęto próbę załadowania kontenera ponad maksymalną ładowność! Kontener może być wypełniony tylko do 90% jego pojemności!");
                    throw new OverfillException("Przekroczono maksymalną ładowność kontenera!");
                }
            }

            base.Load(weight);
        }

        public override void ShowInformation()
        {
            base.ShowInformation();
            if (IsHazardous)
            {
                Console.WriteLine("Ładunek niebiepieczny (Tak/Nie): Tak");
            }
            else
            {
                Console.WriteLine("Ładunek niebiepieczny (Tak/Nie): Nie");
            }
        }
    }
}
