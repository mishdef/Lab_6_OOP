using MyFunctions;
using System.Drawing;
using static MyFunctions.Tools;

namespace Lab_6
{
    public class Program
    {
        public static List <Car> cars = new List<Car>();
        public static int maxCapacity;

        static void Main(string[] args)
        {
            Menu.EnableBeepOnError = false;
            MessageBox.SelectedButtonBgColor = ConsoleColor.Gray;
            MessageBox.SelectedButtonFgColor = ConsoleColor.Black;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowWidth = 160;
            Console.WindowHeight = 50;
            Console.BufferWidth = 160;

            bool firstRun = true;

            maxCapacity = InputInt("Enter the maximum number of cars this storage can hold (1-100): ", InputType.With, 1, 100);

            do
            {
                try
                {
                    switch (Menu.DisplayMenu("          MAIN MENU          ", new[] { 
                        "Add car", 
                        "Show all cars", 
                        "Search car", 
                        "Demonstrate behaviour", 
                        "Delete car", 
                        "Demonstrate static methods",
                        "FOR TESTS ONLY: Add seed data",
                        }, 
                        allowLooping: true, showHowToUse: firstRun))
                    {
                        case 1:
                            MenuAddCar();
                            break;
                        case 2:
                            ShowAllCars();
                            break;
                        case 3:
                            SearchCars();
                            break;
                        case 4:
                            DemonstrateBehaviour();
                            break;
                        case 5:
                            RemoveCar();
                            break;
                        case 6:
                            DemonstrateStaticMethods();
                            break;
                        case 7:
                            AddSeedData();
                            break;
                        case -1:
                            if (MessageBox.Show("Are you shoore? Perform exit?", "Question", MessageBox.Buttons.YesNo) == MessageBox.Button.Yes)
                            {
                                Console.WriteLine(
                                    "\n" +
                                    "██████████                      ██  ██\r\n" +
                                    "██                              ██  ██\r\n" +
                                    "██  ██████  ██████  ██████  ██████  ██████  ██  ██  ██████\r\n" +
                                    "██      ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  ██\r\n" +
                                    "██████████  ██████  ██████  ██████  ██████  ██████  ██████\r\n" +
                                    "                                                ██  ██\r\n" +
                                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓  ██████  ██████    ██  ██  ██\n");
                                return;
                            }
                            break;
                    }
                    firstRun = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            } while (true);
        }

        static void DemonstrateStaticMethods()
        {
            do
            {
                try
                {
                    switch (Menu.DisplayMenu("STATIC MENU", new[] {
                    "Show statistics",
                    "Show fuel price",
                    "Change fuel price",
                    "Show count",
                    "Reset statistics"},
                        allowLooping: true, showHowToUse: false))
                    {
                        case -1:
                            return;
                        case 1:
                            Console.WriteLine(Car.ShowCountres());
                            break;
                        case 2:
                            Console.WriteLine($"Fuel price: {Car.FuelPrice}$ per liter.");
                            break;
                        case 3:
                            do
                            {
                                try
                                {
                                    int newPrice = InputInt("Enter new fuel price: ");
                                    Car.FuelPrice = newPrice;
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            } while (true);
                            break;
                        case 4:
                            Console.WriteLine($"Total cars count: {Car.Count}");
                            break;
                        case 5:
                            Car.ResetCountres();
                            Console.WriteLine("Statistics cleared.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            } while (true);
        }

        static public void AddSeedData() //cheat code
        {
            if (cars.Count >= maxCapacity)
            {
                Console.WriteLine("Storage is full, cannot add seed data.");
                return;
            }

            var seedDataItems = new[]
            {
            new { Mark = "Audi", Model = "A4", Color = Color.Red, HorsePower = 150f, Weight = 1550m, Milage = 12000.0, FuelConsumptionPer100km = 10.0, FuelCapacity = 60.0, ProductionDate = new DateTime(2022, 1, 1), NumberOfDoors = 4 },
            new { Mark = "Audi", Model = "A6", Color = Color.Black, HorsePower = 250f, Weight = 1800m, Milage = 0.0, FuelConsumptionPer100km = 14.0, FuelCapacity = 70.0, ProductionDate = new DateTime(2020, 6, 12), NumberOfDoors = 4 },
            new { Mark = "BMW", Model = "M3", Color = Color.Blue, HorsePower = 420f, Weight = 1600m, Milage = 85000.0, FuelConsumptionPer100km = 16.0, FuelCapacity = 63.0, ProductionDate = new DateTime(2008, 5, 17), NumberOfDoors = 2 },
            new { Mark = "Mini", Model = "Cooper", Color = Color.Green, HorsePower = 40f, Weight = 650m, Milage = 0.0, FuelConsumptionPer100km = 6.0, FuelCapacity = 30.0, ProductionDate = new DateTime(1995, 3, 4), NumberOfDoors = 3 },
            new { Mark = "Ford", Model = "F-150", Color = Color.Black, HorsePower = 400f, Weight = 2500m, Milage = 25000.0, FuelConsumptionPer100km = 24.0, FuelCapacity = 120.0, ProductionDate = new DateTime(2021, 1, 1), NumberOfDoors = 4 }
        };

            int initialCarsCount = cars.Count;
            int carsAddedCount = 0;

            foreach (var item in seedDataItems)
            {
                if (cars.Count < maxCapacity)
                {
                    cars.Add(new Car(item.Mark, item.Model, item.Color, item.HorsePower, item.Weight, item.Milage, item.FuelCapacity, item.ProductionDate, item.FuelConsumptionPer100km, item.NumberOfDoors));
                    carsAddedCount++;
                }
                else
                {
                    Console.WriteLine("Storage became full. Not all seed data cars were added.");
                    break;
                }
            }

            if (carsAddedCount > 0)
            {
                Console.WriteLine($"{carsAddedCount} cars added from seed data.");
                Console.WriteLine("CHEAT CODE ACTIVATED: Seed data added.");
            }
            else if (initialCarsCount == cars.Count)
            {
                Console.WriteLine("No seed data cars could be added due to storage capacity.");
            }
        }

        static void DemonstrateBehaviour()
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("No cars yet.");
                return;
            }

            ShowAllCars();

            int selectedCarIndex;

            do
            {
                int userInputIndex = InputInt("Select car number to interact (0 to back to main menu): ", InputType.With, 0, cars.Count);
                if (userInputIndex == 0) return;
                selectedCarIndex = userInputIndex - 1;
                break;
            } while (true);

            InteractWithCar(cars[selectedCarIndex]);
        }

        static void MenuAddCar()
        {
            if (cars.Count >= maxCapacity)
            {
                Console.WriteLine("The object storage is full. Cannot add more cars.");
                return;
            }
            
            int choice = Menu.DisplayMenu("Choose how to add a car: ", new string[] { 
                "Manually", 
                "Using string data", 
                "With minimal parameters (Mark, Model, Color)", 
                "With all parameters(checks input data after input)" }, 
                allowLooping:false, showHowToUse: false, boxItems: false
                );

            if (choice == 1)
            {
                cars.Add(new Car());

                while (true)
                {
                    string mark = InputString("Enter the cars mark: ");

                    try
                    {
                        cars[cars.Count - 1].Mark = mark;
                        break;

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].Model = InputString("Enter the cars model: ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].Color = (Color)InputInt("Choose the cars color:\n0. Red\n1. Blue\n2. Green\n3. Black\n4. White\n5. Grey\nYour choice: ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    int numberOfDoors = InputInt("Enter the number of doors: ");
                    try
                    {
                        cars[cars.Count - 1].NumberOfDoors = numberOfDoors;
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].HorsePower = (float)InputDouble("Enter the car's horse power: ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].Weight = (decimal)InputDouble("Enter the car's weight (kg): ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].Milage = InputDouble("Enter the car's milage (km): ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].FuelConsumptionPer100km = (double)InputDouble("Enter the car's fuel consumption (l/100km): ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].FuelCapacity = InputDouble("Enter the car's fuel capacity (l): ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                while (true)
                {
                    try
                    {
                        cars[cars.Count - 1].ProductionDate = InputDateTime("Enter the cars production date: ");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Console.WriteLine($"Car {cars[cars.Count - 1].MarkAndModel} added successfully.");
            }
            else if (choice == 2)
            {
                string data = InputString("Enter string data: ");

                Car temp;
                if (Car.TryParse(data, out temp))
                {
                    cars.Add(temp);
                    Console.WriteLine("Car added successfully using TryParse.");
                }
                else
                {
                    Console.WriteLine("Error creating car from string data. Please check the format.");
                }
            }
            else if (choice == 3)
            {
                string mark = InputString("Enter the cars mark: ");
                string model = InputString("Enter the cars model: ");
                Color color = (Color)InputInt("Choose the cars color:\n0. Red\n1. Blue\n2. Green\n3. Black\n4. White\n5. Grey\nYour choice: ", InputType.With, 0, 5);

                Car temp = null;

                try
                {
                    temp = new Car(mark, model, color);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating car: " + ex.Message);
                    return;
                }
                if (temp != null)
                {
                    cars.Add(temp);

                    Console.WriteLine("Car added successfully with default parameters");
                }
            }
            else if (choice == 4)
            {
                string mark = InputString("Enter the cars mark: ");
                string model = InputString("Enter the cars model: ");
                Color color = (Color)InputInt("Choose the cars color:\n0. Red\n1. Blue\n2. Green\n3. Black\n4. White\n5. Grey\nYour choice: ");
                float horsePower = (float)InputDouble("Enter the car's horse power: ");
                decimal weight = (decimal)InputDouble("Enter the car's weight (kg): ");
                double milage = InputDouble("Enter the car's milage (km): ");
                double fuelConsumption = (double)InputDouble("Enter the car's fuel consumption (l/100km): ");
                double fuelCapacity = InputDouble("Enter the car's fuel capacity (l): ");
                DateTime productiDate = InputDateTime("Enter the cars production date: ");
                int numberOfDoors = InputInt("Enter the number of doors: ");

                Car temp = null;

                try
                {
                    temp = new Car(mark, model, color, horsePower, weight, milage, fuelCapacity, productiDate, fuelConsumption, numberOfDoors);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating car: " + ex.Message);
                    return;
                }
                if (temp != null)
                {
                    cars.Add(temp);

                    Console.WriteLine("Car added successfully with default parameters");
                }
            }
            else if (choice == -1) return;
        } 

        static void ShowAllCars()
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("No cars yet...");
                return;
            }

            PrintHeader();
            for (int i = 0; i < cars.Count; i++)
            {
                PrintCarLine(i + 1, cars[i]);
            }
            Console.WriteLine($"Total number of correctly created cars: {Car.Count}");
        }

        static void SearchCars()
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("No cars yet...");
                return;
            }

            int choose = Menu.DisplayMenu("Search by: ", new[] { "Mark and Model", "Color" }, showHowToUse: false, allowLooping: false, boxItems:false);
            List<Car> foundCars = new List<Car>();
            bool anyFound = false;

            if (choose == 1)
            {
                string text = InputString("Enter part of mark/model: ");
                PrintHeader();
                foundCars = cars.FindAll(car => car.MarkAndModel.ToLower().Contains(text.ToLower()));
                foreach (Car car in foundCars)
                {
                    PrintCarLine(cars.IndexOf(car) + 1, car);
                    anyFound = true;
                }
            }
            else
            {
                int colorVal = InputInt("Choose color:\n0. Red\n1. Blue\n2. Green\n3. Black\n4. White\n5. Grey\nYour choice: ", InputType.With, 0, 5);
                Color searchColor = (Color)colorVal;
                PrintHeader();
                foundCars = cars.FindAll(car => car.Color == searchColor);
                foreach (Car car in foundCars)
                {
                    PrintCarLine(cars.IndexOf(car) + 1, car);
                    anyFound = true;
                }
            }

            if (!anyFound)
            {
                Console.WriteLine("No cars found...");
            }
        }

        static void RemoveCar()
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("No cars yet...");
                return;
            }

            List<Car> itemsToRemove = new List<Car>();

            switch (Menu.DisplayMenu("Remove by: ", new[] { "Mark and Model", "Color", "Index" }, showHowToUse: false, allowLooping: false, boxItems: false))
            {
                case 1:
                    string searchText = InputString("Enter search prompt of mark/model: ");
                    itemsToRemove = cars.FindAll(car => car.MarkAndModel.ToLower().Contains(searchText.ToLower()));

                    cars.RemoveAll(car => car.MarkAndModel.ToLower().Contains(searchText.ToLower()));                        //не кайф
                    break;

                case 2:
                    int colorVal = InputInt("Choose color:\n0. Red\n1. Blue\n2. Green\n3. Black\n4. White\n5. Grey\nYour choice: ", InputType.With, 0, 5);
                    Color searchColor = (Color)colorVal;
                    itemsToRemove = cars.FindAll(car => car.Color == searchColor);

                    cars.RemoveAll(car => car.Color == searchColor);
                    break;

                case 3:
                    ShowAllCars();
                    int indexToDelete = InputInt("Enter index of car to remove (or 0 to cancel): ", InputType.With, 0, cars.Count);
                    if (indexToDelete == 0)
                    {
                        Console.WriteLine("Removal cancelled");
                        return;
                    }

                    itemsToRemove.Add(cars[indexToDelete - 1]); 

                    cars.RemoveAt(indexToDelete - 1);
                    break;
            }

            if (itemsToRemove.Count == 0)
            {
                Console.WriteLine("No cars found matching the criteria for removal...");
                return;
            }

            Car.Count -= itemsToRemove.Count;
            string carList = "";
            foreach (Car car in itemsToRemove)
            {
                carList += car.MarkAndModel + ", ";
            }
            carList = carList.Substring(0, carList.Length - 2);

            MessageBox.Show($"Removed the following cars: {carList}. Total cars remaining: {cars.Count}");
        }

        static void InteractWithCar(Car carSel)
        {
            do
            {
                try
                {
                    PrintHeader();
                    PrintCarLine(0, carSel);

                    int action = Menu.DisplayMenu("     Behaviour Menu    ", new[] { 
                        "Start engine", 
                        "Stop engine",
                        "Speed up (default) +10km/h",
                        "Speed up (custom increment) +Xkm/h",
                        "Speed up (custom increment with turbo) +Xkm/h * 1.5",
                        "Slow down",
                        "Ride the car",
                        "Refuel",
                        "ToExportString",
                    }, showHowToUse: false);

                    switch (action)
                    {
                        case -1:
                            return;
                        case 1:
                            Console.WriteLine(carSel.StartEnige());
                            break;
                        case 2:
                            Console.WriteLine(carSel.StopEngine());
                            break;
                        case 3:
                            Console.WriteLine(carSel.SpeedUp());
                            break;
                        case 4:
                            double inc = InputDouble("Speed increment (km/h): ");
                            Console.WriteLine(carSel.SpeedUp(inc));
                            break;
                        case 5:
                            double incTurbo = InputDouble("Speed increment (km/h): ");
                            Console.WriteLine(carSel.SpeedUp(incTurbo, true));
                            break;
                        case 6:
                            double dec = InputDouble("Speed decrement (km/h): ");
                            Console.WriteLine(carSel.SlowDown(dec));
                            break;
                        case 7:
                            double distance = InputDouble("Distance to drive (km): ");
                            Console.WriteLine(carSel.RideCar(distance));
                            break;
                        case 8:
                            if (carSel.CurrentFuel >= carSel.FuelCapacity)
                            {
                                Console.WriteLine("Tank is full.");
                                break;
                            }
                            double maxAdd = carSel.FuelCapacity - carSel.CurrentFuel;
                            double fuel = InputDouble($"Fuel to add (max {maxAdd:F1}): ", InputType.With, 0, maxAdd);
                            Console.WriteLine(carSel.Refuel(fuel));
                            break;
                        case 9:
                            Console.WriteLine("Car data: " + carSel.ToString());
                            break;
                        case 0:
                            Console.WriteLine("Returning to main menu...");
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

        static void PrintHeader()
        {
            DrawLine(151);
            Console.WriteLine("Index| Mark&Model           | Color  | Doors | HP   | Weight | Milage     | Cap    | Fuel   | Fuel per 100km | Speed  | Max speed | Date of production");
            DrawLine(151);
        }

        static void PrintCarLine(int index, Car car)
        {
            Console.WriteLine(
                $"{index,4} | " +
                $"{car.MarkAndModel,-20} | " +
                $"{car.Color,-6} | " +
                $"{car.NumberOfDoors,5} | " +
                $"{car.HorsePower,4} | " +
                $"{car.Weight,6} | " +
                $"{car.Milage,10:F1} | " +
                $"{car.FuelCapacity,6:F1} | " +
                $"{car.CurrentFuel,6:F1} | " +
                $"{car.FuelConsumptionPer100km,14:F1} | " +
                $"{car.CurrentSpeed,6:F1} | " +
                $"{car.MaxSpeed,9:F1} | " +
                $"{car.ProductionDate:yyyy-MM-dd}"
                );
            DrawLine(151);
        }
    }
}