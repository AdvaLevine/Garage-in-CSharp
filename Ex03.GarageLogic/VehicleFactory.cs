using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                {
                    newVehicle = new Car();
                    newVehicle.Engine = new ElectricEngine(newVehicle.MaxCharge);
                    break;
                }

                case eVehicleType.FuelCar:
                {
                    newVehicle = new Car();
                    newVehicle.Engine = new FuelEngine(newVehicle.MaxFuel, newVehicle.FuelType);
                    break;
                }

                case eVehicleType.Truck:
                {
                    newVehicle = new Truck();
                    newVehicle.Engine = new FuelEngine(newVehicle.MaxFuel, newVehicle.FuelType);
                    break;
                }

                case eVehicleType.FuelMotorcycle:
                {
                    newVehicle = new Motorcycle();
                    newVehicle.Engine = new FuelEngine(newVehicle.MaxFuel, newVehicle.FuelType);
                    break;
                }

                case eVehicleType.ElectricMotorcycle:
                {
                    newVehicle = new Motorcycle();
                    newVehicle.Engine = new ElectricEngine(newVehicle.MaxCharge);
                    break;
                }
            }

            return newVehicle;
        }

        public static List<Wheel> CreateWheels(int i_NumOfWheels, string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            List<Wheel> wheels = new List<Wheel>();

            for(int i = 0; i < i_NumOfWheels; i++)
            {
                wheels.Add(new Wheel(i_ManufacturerName, i_CurrentAirPressure, i_MaxAirPressure));
            }

            return wheels;
        }
    }
}
