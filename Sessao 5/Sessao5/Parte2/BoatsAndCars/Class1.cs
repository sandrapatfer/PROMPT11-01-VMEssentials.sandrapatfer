using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatsAndCars
{
    public interface IBoat
    {
        void Move();
        void Dock();
    }
    public interface ICar
    {
        void Move();
        void UpGear();
        void DownGear();
    }
    public class SailBoat : IBoat
    {
        public SailBoat() { Console.WriteLine("Is a SailBoat"); }
        void IBoat.Dock() { throw new NotImplementedException(); }
        void IBoat.Move() { throw new NotImplementedException(); }
        public bool AuxiliaryMotor { set; get; }
    }
    public class SportCar : ICar
    {
        public SportCar() { Console.WriteLine("Is a SportCar"); }
        void ICar.Move() { throw new NotImplementedException(); }
        void ICar.UpGear() { throw new NotImplementedException(); }
        void ICar.DownGear() { throw new NotImplementedException(); }
        public int MaxRotations { set; get; }
    }
}
