using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FactoryCatalog;
using BoatsAndCars;

namespace FactoryProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            FactoryCatalog catalog = new FactoryCatalog();
            catalog.Bind<IBoat, SailBoat>().Singleton.
            Builder(b => b.AuxiliaryMotor = false);
            catalog.Bind<ICar, SportCar>().Builder(c => c.MaxRotations = 8000);
            ICar car = catalog.CreateInstance<ICar>();
            IBoat boat = catalog.CreateInstance<IBoat>();
            car.Show();
            boat.Show();
        }
    }
}
