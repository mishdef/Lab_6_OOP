using Lab_6;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ProgramClassTest
{ 
    [TestClass()]
    [DoNotParallelize]       //многопоточность для статики не очень хорошо работает (ОЧЕНЬ ОЧЕНЬ ОЧЕНЬ ОЧЕНЬ ОЧЕНЬ ПЛОХО!!!!!!!!)
    public sealed class ProgramTest
    {
        private const int TotalSeedItems = 5;

        [TestInitialize()]
        public void Initialize()
        {
            Lab_6.Program.maxCapacity = 100;
            Lab_6.Program.cars = new List<Car>();
            Car.Count = 0;
            Car.ResetCountres();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            Lab_6.Program.cars.Clear();
            Lab_6.Program.maxCapacity = 0;
            Car.Count = 0;
            Car.ResetCountres();
        }

        [TestMethod()]
        public void AddSeedData_AddsAllFiveCars()
        {
            Lab_6.Program.maxCapacity = 5;
            int expectedCarsAdded = TotalSeedItems;

            Lab_6.Program.AddSeedData();

            int finalCount = Lab_6.Program.cars.Count;
            Assert.AreEqual(expectedCarsAdded, finalCount);
            Assert.AreEqual(expectedCarsAdded, Car.Count);
        }

        [TestMethod()]
        public void AddSeedData_RespectsMaxCapacity()
        {
            Lab_6.Program.maxCapacity = 3;
            int expectedFinalCount = 3;

            Lab_6.Program.AddSeedData();

            int finalCount = Lab_6.Program.cars.Count;
            Assert.AreEqual(expectedFinalCount, finalCount);
            Assert.AreEqual(expectedFinalCount, Car.Count);
        }

        [TestMethod()]
        public void AddSeedData_WhenFull_NoMoreAdded()
        {
            Lab_6.Program.maxCapacity = 2;

            Lab_6.Program.AddSeedData();

            int initialCount = Lab_6.Program.cars.Count;
            Assert.AreEqual(2, initialCount);

            Lab_6.Program.AddSeedData();

            int finalCount = Lab_6.Program.cars.Count;
            int expectedFinalCount = initialCount;

            Assert.AreEqual(expectedFinalCount, finalCount);
            Assert.AreEqual(expectedFinalCount, Car.Count);
        }
    }
}
