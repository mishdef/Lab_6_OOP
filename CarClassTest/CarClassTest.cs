using Lab_5;

namespace CarClassTest
{
    [TestClass]
    public sealed class CarClassTest
    {
        #region Initialization
        Car car;

        [TestInitialize]
        public void Initialize()
        {
            Car.Count = 0;
            car = new Car("Temp", "Temp", Color.Black);
        }

        [TestCleanup]
        public void Cleanup()
        {
            car = null;
            Car.ResetCountres();
        }
        #endregion

        #region Methods testing

        [TestMethod()]
        public void CarStaticStatisticsTest()
        {
            //Arrange
            int expectedCount = 1;
            double expectedConsumedFuel = 0.8;
            double expectedMilage = 10;
            double expectedConsumedFuelPrice = 1.6;
            string expectedCountres = "Total consumed fuel price: 1,6$. Total fuel consumption: 0,8 liters. Total milage: 10 km.";
            Car.Count=0;
            Car car = new Car("Temp", "Temp", Color.Black);

            //Act
            car.StartEnige();
            car.SpeedUp(10);
            car.RideCar(10);

            //Assert
            Assert.AreEqual(expectedCount, Car.Count);
            Assert.AreEqual(expectedConsumedFuel, Car.TotalConsumedFuel);
            Assert.AreEqual(expectedMilage, Car.TotalMilage);
            Assert.AreEqual(expectedConsumedFuelPrice, Car.TotalConsumedFuelPrice);
            Assert.AreEqual(expectedCountres, Car.ShowCountres());



            //Arrange
            expectedConsumedFuel = 0;
            expectedMilage = 0;
            expectedConsumedFuelPrice = 0;
            expectedCountres = "Total consumed fuel price: 0$. Total fuel consumption: 0 liters. Total milage: 0 km.";

            //Act
            Car.ResetCountres();

            //Assert
            Assert.AreEqual(expectedConsumedFuel, Car.TotalConsumedFuel);
            Assert.AreEqual(expectedMilage, Car.TotalMilage);
            Assert.AreEqual(expectedConsumedFuelPrice, Car.TotalConsumedFuelPrice);
            Assert.AreEqual(expectedCountres, Car.ShowCountres());
        }

        [TestMethod()]
        [DataRow(20, 20)]
        [DataRow(-20, 0)]
        [DataRow(0, 0)]
        public void CarSpeedUpTest(int increment, int expected)
        {
            //Arrange

            //Act
            car.StartEnige();
            car.SpeedUp(increment);

            //Assert
            Assert.AreEqual(expected, car.CurrentSpeed);
        }

        [DataRow(20, 30)]
        [DataRow(-20, 50)]
        [DataRow(0, 50)]
        [TestMethod()]
        public void CarSlowDownTest(int decrement, int expected)
        {
            //Arrange
            car.StartEnige();
            car.SpeedUp(50);

            //Act
            car.SlowDown(decrement);

            //Assert
            Assert.AreEqual(expected, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarStaticCountTest()
        {
            //Arrange
            int initialCount = Car.Count;

            //Act
            Car car1 = new Car("M1", "M1", Color.Red, 150, 1550, 12000, 60, new DateTime(2022, 1, 1), 10, 4);
            Car car2 = new Car("M2", "M2", Color.Red, 150, 1550, 12000, 60, new DateTime(2022, 1, 1), 10, 4);
            Car car3 = new Car("M3", "M3", Color.Red, 150, 1550, 12000, 60, new DateTime(2022, 1, 1), 10, 4);

            // Assert
            Assert.AreEqual(initialCount + 3, Car.Count);
        }

        [TestMethod()]
        public void CarParseTestCorrect()
        {
            //Arrange
            string text = "Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;10;4";

            //Act
            Car car = Car.Parse(text);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;")]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2028 0:00:00;10;4")]
        [DataRow("Audiadsadsadasd;A4;0;150;1550;12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;-1550;-12000;60;01.01.2028 0:00:00;10;4")]
        [ExpectedException(typeof(ArgumentException))]
        public void CarPhaseTestUncorrect(string text)
        {
            //Arrange
            
            //Act
            Car car = Car.Parse(text);

            //Assert
        }

        [TestMethod()]
        public void TryParseTestCorrect()
        {
            //Arrange
            string text = "Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;10;4";

            //Act
            bool result = Car.TryParse(text, out Car car);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;")]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2028 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2028 0:00:00;10;4;132;235;adsd")]
        [DataRow("Audi;A444444444444444444;0;150;1550;12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;-5;150;1550;12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;-150;1550;12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;-1550;12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;1550;-12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;1550;12000;-60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;-10;4")]
        [DataRow("Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;10;-4")]
        [DataRow("Audi;A4;0;150")]
        [DataRow("Audiadsadsadasd;A4;0;150;1550;12000;60;01.01.2022 0:00:00;10;4")]
        [DataRow("Audi;A4;0;150;-1550;-12000;60;01.01.2028 0:00:00;10;4")]
        public void TryParseTestUncorrect(string text)
        {
            //Arrange

            //Act
            bool result = Car.TryParse(text, out Car car);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CarToStringTest()
        {
            //Arrange
            Car temp = new Car("Audi", "A4", 0, 150, 1550, 12000, 60, new DateTime(2022, 1, 1), 10, 4);
            string expected = "Audi;A4;0;150;1550;12000;60;01.01.2022 0:00:00;10;4";

            //Act
            string result = temp.ToString();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarStartEnigeNotStartedTest()
        {
            //Arrange
            string expected = "The car has started.";

            //Act
            string result = car.StartEnige();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarStartEnigeStartedTest()
        {
            //Arrange
            car.StartEnige();
            string expected = "The car is already started.";

            //Act
            string result = car.StartEnige();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarStartWithEmptyTankTest()
        {
            //Arrange
            car.CurrentFuel = 0;
            string expected = "Cannot start engine. The fuel tank is empty.";

            //Act
            string result = car.StartEnige();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarStopAndSpeedResetWithEnigeStartedTest()
        {
            //Arrange
            car.StartEnige();
            car.SpeedUp(20);
            string expectedRes = "The car has stopped.";
            int expectedSpeed = 0;

            //Act
            string result = car.StopEngine();

            //Assert
            Assert.AreEqual(expectedRes, result);
            Assert.AreEqual(0, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarStopEnigeNotStartedTest()
        {
            //Arrange
            string expected = "The car is already stopped.";

            //Act
            string result = car.StopEngine();

            //Assert
            Assert.AreEqual(expected, result);
            
        }

        [TestMethod()]
        public void CarSpeedUpNoParametersTest()
        {
            //Arrange
            car.StartEnige();
            string expected = "The car's current speed is 10,00 km/h.";

            //Act
            string result = car.SpeedUp();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarSpeedUpNoParametersEngineNotStartedTest()
        {
            //Arrange
            string expected = "The car is not started. Please start the engine first.";

            //Act
            string result = car.SpeedUp();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod()]
        public void CarSpeedUpWithParametersNormalTest()
        {
            //Arrange
            car.StartEnige();
            string expected = "The car's current speed is 20,00 km/h.";

            //Act
            string result = car.SpeedUp(20);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarSpeedUpWithParametersNegativeTest()
        {
            //Arrange
            car.StartEnige();
            string expected = "Increment must be a positive value.";

            //Act
            string result = car.SpeedUp(-20);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarSpeedUpWithParametersZeroTest()
        {
            //Arrange
            car.StartEnige();
            string expected = "The car's current speed is 0,00 km/h.";

            //Act
            string result = car.SpeedUp(0);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarSpeedUpWithEmptyTankTest()
        {
            //Arrange
            car.StartEnige();
            car.CurrentFuel = 0;
            string expected = "Out of fuel! The car stopped. Please refuel.";

            //Act
            string result = car.SpeedUp(20);

            //Assert
            Assert.AreEqual(expected, result);
        }

        public void CarSpeedUpWithBigSpeedTest()
        {
            //Arrange
            car.StartEnige();
            string expected = $"The car has reached its maximum speed of {car.MaxSpeed:F2} km/h.";
            double expectedSpeed = car.MaxSpeed;

            //Act
            string result = car.SpeedUp(1000);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarSpeedUpWithTurboTest()
        {
            //Arrange
            car.StartEnige();
            string expected = $"The car's current speed is 75,00 km/h.";
            int expectedSpeed = 75;

            //Act
            string result = car.SpeedUp(50, true);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarSpeedUpWithTurboFalseTest()
        {
            //Arrange
            car.StartEnige();
            string expected = $"The car's current speed is 50,00 km/h.";
            int expectedSpeed = 50;

            //Act
            string result = car.SpeedUp(50, false);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarSlowDownTest()
        {
            //Arrange
            car.StartEnige();
            car.SpeedUp(50);
            string expected = $"The car's current speed is 25,00 km/h.";
            int expectedSpeed = 25;

            //Act
            string result = car.SlowDown(25);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarSlowDownNegativeTest()
        {
            //Arrange
            car.StartEnige();
            car.SpeedUp(50);
            string expected = $"Decrement must be a positive value.";
            int expectedSpeed = 50;

            //Act
            string result = car.SlowDown(-25);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarSlowDownZeroTest()
        {
            //Arrange
            car.StartEnige();
            car.SpeedUp(50);
            string expected = $"The car's current speed is 50,00 km/h.";
            int expectedSpeed = 50;

            //Act
            string result = car.SlowDown(0);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarSlowDownWithNotStartedEngineTest()
        {
            //Arrange
            string expected = $"The car is not started. Please start the engine first.";

            //Act
            string result = car.SlowDown(25);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarSlowDownWithBigValueTest()
        {
            //Arrange
            car.StartEnige();
            car.SpeedUp(50);
            string expected = $"The car has come to a complete stop.";
            int expectedSpeed = 0;

            //Act
            string result = car.SlowDown(1000);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedSpeed, car.CurrentSpeed);
        }

        [TestMethod()]
        public void CarRideCarWithNotStartedEngineTest()
        {
            //Arrange
            string expected = $"Cannot ride. The car is not started. Please start the engine first.";

            //Act
            string result = car.RideCar(10);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [DataRow(10, 30, 50, "Drove for 12,00 minutes (10,00 km) at 50,00 km/h. Fuel consumed: 0,80 liters. Remaining fuel: 29,20 / 50 liters. Total milage: 10,00 km.")]
        [DataRow(20, 30, 40, "Drove for 30,00 minutes (20,00 km) at 40,00 km/h. Fuel consumed: 1,60 liters. Remaining fuel: 28,40 / 50 liters. Total milage: 20,00 km.")]
        [DataRow(100, 1, 50, "Ran out of fuel after driving for 15,00 minutes and 12,50 km. The car stopped. Total milage: 12,50 km. Please refuel.")]
        [DataRow(10, 30, 0, "Cannot ride. The car is not moving. Increase speed first.")]
        [DataRow(-10, 30, 50, "Driving distance must be positive.")]
        [DataRow(10, 0, 50, "Out of fuel! The car has stopped. Please refuel.")]
        public void CarRideCarTest(int distance, double currentFuel, double currentSpeed, string expected)
        {
            //Arrange
            car.StartEnige();
            car.CurrentSpeed = currentSpeed;
            car.CurrentFuel = currentFuel;

            //Act
            string result = car.RideCar(distance);

            //Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod()]
        [DataRow(0,50, 30, "Refueled 30,00 liters. Current fuel: 30,00 / 50 liters.")]
        [DataRow(40,50, 30, "Refueled 10,00 liters. The tank is now full: 50,00 / 50 liters.")]
        [DataRow(50,50, 30, "Tank is already full.")]
        [DataRow(20,50, -30, "Refuel amount must be positive.")]
        public void CarRefuelTest(double currentFuel, double maxFuel, double refuel, string expected)
        {
            //Arrange
            car.FuelCapacity = maxFuel;
            car.CurrentFuel = currentFuel;

            //Act
            string result = car.Refuel(refuel);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CarToDisplayStringTest()
        {
            //Arrange
            string expected = $"Car: Temp Temp, Color: Black, HorsePower: 100, Weight: 1200, Milage: 0,00 km, CurrentSpeed: 0,00 km/h ,MaxSpeed: 83,33 km/h, Fuel: 50,00/50 liters. Fuel per 100km: 8,00 liters.";

            //Act
            string result = car.ToDisplayString();
            //
            //Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region CarConstructor

        [TestMethod()]
        public void CarDefaultConstructorTest()
        {
            //Arrange

            //Act
            Car car = new Car();

            //Assert
            Assert.AreEqual("TempMark", car.MarkAndModel.Split(' ')[0]);
            Assert.AreEqual("TempModel", car.MarkAndModel.Split(' ')[1]);
            Assert.AreEqual(Color.White, car.Color);
        }

        [TestMethod()]
        [DataRow("Audi", "A4", Color.Red)]
        [DataRow("Mark", "Model", Color.Blue)]
        public void CarMinimalConstructorTestCorrect(string mark, string model, Color color)
        {
            //Arrange

            Car car = new Car(mark, model, color);

            //Act

            //Assert
            Assert.AreEqual(mark + " " + model, car.MarkAndModel);
            Assert.AreEqual(color, car.Color);
        }


        [DataRow("Markkkkkkkkkkkkk", "Model", Color.Red)]
        [DataRow("Mark", "Modelllllllllllllll", Color.Red)]
        [DataRow("Mark", "Model", (Color)10)]
        [DataRow("", "Model", Color.Red)]
        [DataRow("Mark", "", Color.Red)]
        [DataRow("", "", Color.Red)]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CarMinimalConstructorTestUncorrect(string mark, string model, Color color)
        {
            //Arrange

            //Act
            Car car = new Car(mark, model, color);

            //Assert
        }

        [TestMethod()]
        public void CarFullConstructorTestCorrect()
        {
            //Arrange

            //Act
            Car car = new Car("Audi", "A4", Color.Red, 150, 1550, 12000, 60, new DateTime(2022, 1, 1), 10, 4);

            //Assert
            Assert.AreEqual("Audi A4", car.MarkAndModel);
            Assert.AreEqual(Color.Red, car.Color);
            Assert.AreEqual(150, car.HorsePower);
            Assert.AreEqual(1550, car.Weight);
            Assert.AreEqual(12000, car.Milage);
            Assert.AreEqual(60, car.FuelCapacity);
            Assert.AreEqual(new DateTime(2022, 1, 1), car.ProductionDate);
            Assert.AreEqual(10, car.FuelConsumptionPer100km);
            Assert.AreEqual(4, car.NumberOfDoors);
        }

        [TestMethod()]
        [DataRow("Audiiiiiiiiiiiiiiiii", "A4", Color.Red, 150, 1550, 12000, 60, 10, 4)]
        [DataRow("Audi", "A44444444444444444444", Color.Red, 150, 1550, 12000, 60, 10, 4)]
        [DataRow("Audi", "A4", (Color)10, 150, 1550, 12000, 60, 10, 4)]
        [DataRow("Audi", "A4", Color.Red, -150, 1550, 12000, 60, 10, 4)]
        [DataRow("Audi", "A4", Color.Red, 150, -1550, 12000, 60, 10, 4)]
        [DataRow("Audi", "A4", Color.Red, 150, 1550, -12000, 60, 10, 4)]
        [DataRow("Audi", "A4", Color.Red, 150, 1550, 12000, -60, 10, 4)]
        [DataRow("Audi", "A4", Color.Red, 150, 1550, 12000, 60, -10, 4)]
        [DataRow("Audi", "A4", Color.Red, 150, 1550, 12000, 60, 10, -4)]
        [DataRow("Mark", "Model", Color.Blue, 0, 0, 0, 0, 0, 0)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarFullConstructorTestUncorrect(string mark, string model, Color color, int horsePower, int weight, int milage, int fuelCapacity, int fuelConsumptionPer100km, int numberOfDoors)
        {
            //Arrange
            DateTime dateTime = new DateTime(2022, 1, 1);

            //Act
            Car car = new Car(mark, model, color, horsePower, weight, milage, fuelCapacity, dateTime, fuelConsumptionPer100km, numberOfDoors);

            //Assert            
        }

        #endregion

        #region Properties testing
        [TestMethod()]
        public void CarSetMarkTestCorrect()
        {
            //Arrange
            string mark = "Mark";

            //Act
            car.Mark = mark;

            //Assert
            Assert.IsTrue(true);
        }

        [DataRow("Markkkkkkkkkkkkk")]
        [DataRow("")]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CarSetModelTestUnorrect(string mark)
        {
            //Arrange

            //Act
            car.Mark = mark;

            //Assert
        }

        [TestMethod()]
        public void CarSetModelTestCorrect()
        {
            //Arrange
            string model = "Model";

            //Act
            car.Model = model;

            //Assert
            Assert.IsTrue(true);
        }

        [DataRow("Modelllllllll")]
        [DataRow("")]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CarSetColorTestUncorrect(string model)
        {
            //Arrange

            //Act
            car.Model = model;

            //Assert
        }

        [TestMethod()]
        public void CarSetColorTestCorrect()
        {
            //Arrange
            Color color = Color.Red;

            //Act
            car.Color = color;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow(-1)]
        [DataRow(10)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetColorTestUncorrect(int value)
        {
            //Arrange

            //Act
            car.Color = (Color)value;

            //Assert
        }

        [TestMethod()]
        public void CarSetHorsePowerTestCorrect()
        {
            //Arrange
            int horsePower = 150;

            //Act
            car.HorsePower = horsePower;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow(-1)]
        [DataRow(10)]
        [DataRow(0)]
        [DataRow(10000)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetHorsePowerTestUncorrect(int value)
        {
            //Arrange

            //Act
            car.HorsePower = value;

            //Assert
        }

        [TestMethod()]
        public void CarSetWeightTestCorrect()
        {
            //Arrange
            int weight = 1550;

            //Act
            car.Weight = weight;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow(-1)]
        [DataRow(10)]
        [DataRow(10000)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetWeightTestUncorrect(int value)
        {
            //Arrange

            //Act
            car.Weight = value;

            //Assert
        }

        [TestMethod()]
        public void CarSetMilageTestCorrect()
        {
            //Arrange
            int milage = 12000;

            //Act
            car.Milage = milage;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetMilageTestUncorrect()
        {
            //Arrange
            int milage = -10;

            //Act
            car.Milage = milage;

            //Assert
        }

        [TestMethod()]
        public void CarSetFuelCapacityTestCorrect()
        {
            //Arrange
            int fuelCapacity = 60;

            //Act
            car.FuelCapacity = fuelCapacity;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow(-1)]
        [DataRow(10)]
        [DataRow(10000)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetFuelCapacityTestUncorrect(int value)
        {
            //Arrange

            //Act
            car.FuelCapacity = value;

            //Assert
        }

        [TestMethod()]
        public void CarSetProductionDateTestCorrect()
        {
            //Arrange
            DateTime productionDate = new DateTime(2022, 1, 1);

            //Act
            car.ProductionDate = productionDate;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetProductionDateTestUncorrect()
        {
            //Arrange
            DateTime productionDate = new DateTime(2027, 1, 1);

            //Act
            car.ProductionDate = productionDate;

            //Assert
        }

        [TestMethod()]
        public void CarSetNumberOfDoorsTestCorrect()
        {
            //Arrange
            int numberOfDoors = 4;
            car.NumberOfDoors = numberOfDoors;

            //Act
            car.NumberOfDoors = numberOfDoors;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow(-1)]
        [DataRow(10)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetNumberOfDoorsTestUncorrect(int value)
        {
            //Arrange

            //Act
            car.NumberOfDoors = value;

            //Assert
        }

        [TestMethod()]
        public void CarSetFuelConsumptionPer100kmTestCorrect()
        {
            //Arrange
            int fuelConsumptionPer100km = 10;

            //Act
            car.FuelConsumptionPer100km = fuelConsumptionPer100km;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetFuelConsumptionPer100kmTestUncorrect()
        {
            //Arrange
            int fuelConsumptionPer100km = -1;

            //Act
            car.FuelConsumptionPer100km = fuelConsumptionPer100km;

            //Assert
        }

        [TestMethod()]
        public void CarSetCurrentSpeedTestCorrect()
        {
            //Arrange
            double currentSpeed = 10;

            //Act
            car.CurrentSpeed = currentSpeed;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetCurrentSpeedTestUncorrect()
        {
            //Arrange
            double currentSpeed = -10;

            //Act
            car.CurrentSpeed = currentSpeed;

            //Assert
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetCurrentSpeedBigerThanMaxSpeedTestCorrect()
        {
            //Arrange
            double currentSpeed = 600;

            //Act
            car.CurrentSpeed = currentSpeed;

            //Assert
        }

        [TestMethod()]
        public void CarSetCurrentFuelTestCorrect()
        {
            //Arrange
            double currentFuel = 10;

            //Act
            car.CurrentFuel = currentFuel;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [DataRow(-10)]
        [DataRow(1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetCurrentFuelTestUncorrect(double currentFuel)
        {
            //Arrange

            //Act
            car.CurrentFuel = currentFuel;

            //Assert
        }

        [TestMethod()]
        public void CarSetFuelPriceTestCorrect()
        {
            //Arrange
            double fuelPrice = 10;

            //Act
            Car.FuelPrice = fuelPrice;

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CarSetFuelPriceTestUncorrect()
        {
            //Arrange
            double fuelPrice = -10;

            //Act
            Car.FuelPrice = fuelPrice;

            //Assert
        }
        #endregion   
    }
}
