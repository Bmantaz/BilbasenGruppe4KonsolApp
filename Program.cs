using System.Drawing;
using System.Net.WebSockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
namespace Bilbasen
{
    internal class Program
    {
        static List<IVehicle> Vehicles = new List<IVehicle>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Vehicles = GenerateVehicles(300); //Generator til køretøjer

            while (true)
            {
                Console.WriteLine("Bilbasen");
                Console.WriteLine("1. Vis køretøjer med samme mærke som det første køretøjer");
                Console.WriteLine("2. Vis biler med over 200 HK");
                Console.WriteLine("3. Vis alle røde biler");
                Console.WriteLine("4. Vis antal af biler med samme mærke som den første bil");
                Console.WriteLine("5. Vis biler fra 1980 - 1999");
                Console.WriteLine("6. Vis alle biler");
                Console.WriteLine("0. Afslut");

                Console.Write("Vælg en mulighed: ");
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        PrintSameBrandAsFirstVehicle();
                        break;

                    case "2":
                        PrintVehiclesWithOver200HP(200);
                        break;

                    case "3":
                        PrintRedVehicles("Red");
                        break;

                    case "4":
                        PrintCountBrandAsFirstVehicle();
                        break;

                    case "5":
                        PrintVehiclesByYearRange(1980, 1999);
                        break;

                    case "6":
                        PrintAllVehicles();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }

        private static List<IVehicle> GenerateVehicles(int count)
        {
            Random rand = new Random();
            var vbrands = new[] { "Ford", "Toyota", "Chevrolet", "BMW", "Audi", "Honda", "Mercedes", "Volkswagen" };
            var mbrands = new[] { "Yamaha", "Suzuki", "Ducati", "BMW", "KTM", "Honda", "Kawasaki", "Augusta" };
            var models = new[] { "Model A", "Model B", "Model C", "Model D", "Model E" };
            var colors = new[] { "Red", "Blue", "Green", "Black", "White", "Silver", "Yellow" };
            var rnd = new Random();
            var cylinderOptions = new[] { 3, 4, 5, 6, 8, 10, 12 };
            var vehicles = new List<IVehicle>();

            for (int i = 0; i < count; i++)
            {
                var model = models[rnd.Next(models.Length)];
                var year = rnd.Next(1970, 2023);
                var color = colors[rnd.Next(colors.Length)];
                var horsePower = rnd.Next(100, 701);
                var price = rnd.Next(50000, 2000001);

                int type = rand.Next(3);

                switch (type)
                {
                    case 0: //FuelCar
                        int NumberOfCylinders = cylinderOptions[rnd.Next(cylinderOptions.Length)];
                        var fbrand = vbrands[rnd.Next(vbrands.Length)];
                        vehicles.Add(new FuelCar(fbrand, model, year, color, horsePower, NumberOfCylinders, price));
                        break;

                    case 1: //ElectricCar
                        int batteryCapacity = rand.Next(20, 101);
                        var ebrand = vbrands[rnd.Next(vbrands.Length)];
                        vehicles.Add(new ElectricCar(ebrand, model, year, color, horsePower, batteryCapacity, price));
                        break;

                    case 2: //Motorcycle
                        bool hasSideCar = rand.Next(2) == 0 ;
                        var mcbrand = mbrands[rnd.Next(mbrands.Length)];
                        vehicles.Add(new Motorcycle(mcbrand, model, year, color, horsePower, hasSideCar, price));
                        break;
                }
            }
            return vehicles;
        }

        static void PrintSameBrandAsFirstVehicle()
        {
            string firstbrand = Vehicles[0].Brand;

            Console.WriteLine($"Kørtøjer med samme mærke som {firstbrand}:");
            Vehicles.Where(v => v.Brand == firstbrand)
                .ToList()
                .ForEach(v => Console.WriteLine(v));

        }

        static void PrintVehiclesWithOver200HP(int minimumHP)
        {
            Console.WriteLine($"-- Biler med over {minimumHP} hk --");
            Vehicles.Where(v => v.HorsePower > minimumHP)
                .ToList()
                .ForEach(v => Console.WriteLine(v));

        }

        static void PrintRedVehicles(string color)
        {
            Console.WriteLine($"-- Biler med farven {color} --");
            Vehicles.Where(v => v.Color == color)
                .ToList()
                .ForEach(v => Console.WriteLine(v));
        }


        static void PrintCountBrandAsFirstVehicle()
        {
            string firstbrand = Vehicles[0].Brand;

            var antal = Vehicles.Count(v => v.Brand == firstbrand);
            Console.WriteLine($"Antal køretøjer med samme mærke som {firstbrand} - {antal}");
        }

        static void PrintVehiclesByYearRange(int fromYear, int toYear)
        {
            Console.WriteLine($"-- Køretøjer fra årgang {fromYear} til {toYear} --");
            Vehicles.Where(v => v.Year >= fromYear && v.Year <= toYear)
                .ToList()
                .ForEach(v => Console.WriteLine(v));
        }


        static void PrintAllVehicles()
        {
            Console.WriteLine("-- Alle køretøjer --");
            Vehicles.ForEach(v => Console.WriteLine(v));
        }






        //Interface
        public interface IVehicle
        {
            string Brand { get; set; }
            string Model { get; set; }
            int Year { get; set; }
            string Color { get; set; }
            int HorsePower { get; set; }
            decimal Price { get; set; }
        }

        //Abstrakt klasse til køretøjer
        public abstract class Vehicle : IVehicle
        {
            private string _brand;
            private string _model;
            private int _year;
            private string _color;
            private int _horsePower;
            private decimal _price;
            public int Year { get => _year; set => _year = value; }
            public string Color { get => _color; set => _color = value; }
            public int HorsePower { get => _horsePower; set => _horsePower = value; }
            public decimal Price { get => _price; set => _price = value; }
            public string Brand { get => _brand; set => _brand = value; }
            public string Model { get => _model; set => _model = value; }

            protected Vehicle(string brand, string model, int year, string color, int horsePower, decimal price)
            {
                Brand = brand;
                Model = model;
                Year = year;
                Color = color;
                Price = price;
                HorsePower = horsePower;

            }

            public override string ToString()
            {
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {Price} Dkk";
            }
        }

        //Benzin bil klasse
        public class FuelCar : Vehicle
        {
            private int _NumberOfCylinders;

            public int NumberOfCylinders { get => _NumberOfCylinders; set => _NumberOfCylinders = value; }


            public FuelCar(string brand, string model, int year, string color, int horsePower, int _NumberOfCylinders, decimal price)
                          : base(brand, model, year, color, horsePower, price)

            {
                NumberOfCylinders = _NumberOfCylinders;
            }

            public override string ToString()
            {
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {NumberOfCylinders} cyl, {Price} Dkk";
            }
        }

        //Elbil klasse
        public class ElectricCar : Vehicle
        {
            private int _BatteryCapacity;

            public int BatteryCapacity { get => _BatteryCapacity; set => _BatteryCapacity = value; }

            public ElectricCar(string brand, string model, int year, string color, int horsePower, int _BatteryCapacity, decimal price)
                          : base(brand, model, year, color, horsePower, price)
            {
                BatteryCapacity = _BatteryCapacity;
            }

            public override string ToString()
            {
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {BatteryCapacity} KWh, {Price} Dkk";
            }
        }

        //Motorcykel klasse
        public class Motorcycle : Vehicle
        {
            private bool _HasSideCar;

            public bool HasSideCar { get => _HasSideCar; set => _HasSideCar = value; }

            public Motorcycle(string brand, string model, int year, string color, int horsePower, bool _HasSideCar, decimal price)
                          : base(brand, model, year, color, horsePower, price)

            {
                HasSideCar = _HasSideCar;
            }

            public override string ToString()
            {
                var HasSideCarMessage = HasSideCar ? "Med Sidevogn" : "Uden Sidevogn";
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {HasSideCarMessage}, {Price} Dkk ";
            }
        }
    }
}