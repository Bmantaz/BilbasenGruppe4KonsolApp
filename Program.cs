using System.Net.WebSockets;
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
                        //PrintCarsWithOver200HP();
                        break;

                    case "3":
                        //PrintRedCars();
                        break;

                    case "4":
                        PrintSameBrandAsFirstCar();
                        break;

                    case "5":
                        //PrintCarsFrom1980To1999();
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

        static void PrintAllCars()
        {
            Console.WriteLine("-- Alle biler --");
            cars.ForEach (c => Console.WriteLine(c));
        }

        static void PrintSameBrandAsFirstCar()
        {
            string firstbrand = cars[0].Brand;

            Console.WriteLine($"Biler med samme mærke som {firstbrand}:");
            foreach (var car in cars.Where(c => c.Brand == firstbrand))
            {
                
            }

        }

        static void PrintCarsByYearRange(int fromYear, int toYear)
        {
            Console.Writeline($"-- Biler fra årgang {fromYear} til {toYear} --");
            cars.Where(c => c.Year >= fromYear && c.Year <= toYear)
                .ToList()
                .ForEach(c => Console.WriteLine(c));
        }

    }

}