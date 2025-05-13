using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
namespace Bilbasen
{
    internal class Program
    {
        static List<Car> cars = new List<Car>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            cars = GenerateCars(100); //Generator til biler

            while (true)
            {
                Console.WriteLine("Bilbasen");
                Console.WriteLine("1. Vis biler med samme mærke som den første bil");
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
                        PrintSameBrandAsFirstCar();
                        break;

                    case "2":
                        PrintCarsWithOver200HP(200);
                        break;

                    case "3":
                        PrintRedCars("Red");
                        break;

                    case "4":
                        PrintCountBrandAsFirstCar();
                        break;

                    case "5":
                        PrintCarsByYearRange(1980, 1999);
                        break;

                    case "6":
                        PrintAllCars();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }

        public class Car
        {
            public string Brand;
            public string Model;
            public int Year;
            public string Color;
            public int HorsePower;
            public int NumberOfCylinders;
            public Car(string brand, string model, int year, string color, int horsePower, int numberOfCylinders)
            {
                Brand = brand;
                Model = model;
                Year = year;
                Color = color;
                HorsePower = horsePower;
                NumberOfCylinders = numberOfCylinders;
            }

            public override string ToString()
            {
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {NumberOfCylinders} cyl";
            }

        }
        static List<Car> GenerateCars(int count)
        {
            Random rand = new Random();
            var brands = new[] { "Ford", "Toyota", "Chevrolet", "BMW", "Audi", "Honda", "Mercedes", "Volkswagen" };
            var models = new[] { "Model A", "Model B", "Model C", "Model D", "Model E" };
            var colors = new[] { "Red", "Blue", "Green", "Black", "White", "Silver", "Yellow" };
            var cylinderOptions = new[] { 3, 4, 5, 6, 8, 12 };
            var rnd = new Random();
            var cars = new List<Car>();

            for (int i = 0; i < count; i++)
            {
                var brand = brands[rnd.Next(brands.Length)];
                var model = models[rnd.Next(models.Length)];
                var year = rnd.Next(1970, 2023);
                var color = colors[rnd.Next(colors.Length)];
                var horsePower = rnd.Next(100, 701);
                var numberOfCylinders = cylinderOptions[rnd.Next(cylinderOptions.Length)];
                cars.Add(new Car(brand, model, year, color, horsePower, numberOfCylinders));
            }

            return cars;
        }

        static void PrintSameBrandAsFirstCar()
        {
            string firstbrand = cars[0].Brand;

            Console.WriteLine($"Biler med samme mærke som {firstbrand}:");
            cars.Where(c => c.Brand == firstbrand)
                .ToList()
                .ForEach(c => Console.WriteLine(c));

        }

        static void PrintCarsWithOver200HP(int minimumHP)
        {
            Console.WriteLine($"-- Biler med over {minimumHP} hk --");
            cars.Where(c => c.HorsePower > minimumHP)
                .ToList()
                .ForEach(c => Console.WriteLine(c));

        }

        static void PrintRedCars(string color)
        {
            Console.WriteLine($"-- Biler med farven {color} --");
            cars.Where(c => c.Color == color)
                .ToList()
                .ForEach(c => Console.WriteLine(c));

        }


        static void PrintCountBrandAsFirstCar()
        {
            string firstbrand = cars[0].Brand;

            var antal = cars.Count(c => c.Brand == firstbrand);
            Console.WriteLine($"Antal biler med samme mærke som {firstbrand} - {antal}");



        }

        static void PrintCarsByYearRange(int fromYear, int toYear)
        {
            Console.WriteLine($"-- Biler fra årgang {fromYear} til {toYear} --");
            cars.Where(c => c.Year >= fromYear && c.Year <= toYear)
                .ToList()
                .ForEach(c => Console.WriteLine(c));
        }


        static void PrintAllCars()
        {
            Console.WriteLine("-- Alle biler --");
            cars.ForEach(c => Console.WriteLine(c));
        }








        public interface IVehicle
        {
            string Brand { get; set; }
            string Model { get; set; }
            int Year { get; set; }
            string Color { get; set; }
            int HorsePower { get; set; }
            decimal Price { get; set; }
        }


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

            public abstract override string ToString();

        }





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
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {NumberOfCylinders} cyl";
            }

        }

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
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {BatteryCapacity} cyl";
            }

        }

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
                return $"{Year} {Brand} {Model} - {Color}, {HorsePower} HK, {HasSideCar} cyl";
            }

        }






    }

}