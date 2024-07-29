using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private const int k_GetAll = -1;
        private readonly Dictionary<string, Vehicle> m_Vehicles = new Dictionary<string, Vehicle>();
        private readonly Dictionary<string, CustomerInfo> m_CustomersInfo = new Dictionary<string, CustomerInfo>();

        public bool IsGarageEmpty()
        {
            return m_Vehicles.Count == 0;
        }
        public void InsertVehicleToGarage(Vehicle i_Vehicle, CustomerInfo i_ClientInfo)
        {
            m_Vehicles.Add(i_Vehicle.LicenseNumber, i_Vehicle);
            m_CustomersInfo.Add(i_Vehicle.LicenseNumber, i_ClientInfo);
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public Vehicle GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage.");
            }

            return m_Vehicles[i_LicenseNumber];
        }

        public void ChangeVehicleState(string i_LicenseNumber, eVehicleStatus i_Status)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage.");
            }

            m_CustomersInfo[i_LicenseNumber].VehicleStatus = i_Status;
        }

        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage.");
            }

            foreach(Wheel wheel in m_Vehicles[i_LicenseNumber].Wheels)
            {
                wheel.InflateToMax();
            }
        }

        public void FuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountOfFuel)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage.");
            }

            if (m_Vehicles[i_LicenseNumber].Engine is ElectricEngine)
            {
                throw new ArgumentException("You cannot fuel an electric vehicle.");
            }

            (m_Vehicles[i_LicenseNumber].Engine as FuelEngine).AddFuel(i_AmountOfFuel, i_FuelType);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_MinutesToCharge)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage.");
            }

            if (m_Vehicles[i_LicenseNumber].Engine is FuelEngine)
            {
                throw new ArgumentException("You cannot charge an electric vehicle.");
            }

            (m_Vehicles[i_LicenseNumber].Engine as ElectricEngine).AddCharge(i_MinutesToCharge);
        }

        public string VehicleInfoToString(string i_LicenseNumber)
        {
            Vehicle currentVehicle;
            CustomerInfo currentCustomer;

            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("There is no vehicle with this license number in the garage.");
            }

            currentVehicle = m_Vehicles[i_LicenseNumber];
            currentCustomer = m_CustomersInfo[i_LicenseNumber];

            return string.Format(
@"{0}

{1}
",
currentCustomer.ToString(), 
currentVehicle.ToString());
        }

        public List<string> GetLicenseListByVehicleState(int i_VehicleStatus)
        {
            List<string> licenseList = new List<string>();

            foreach(string licenseNumber in m_CustomersInfo.Keys)
            {
                if (i_VehicleStatus == k_GetAll || m_CustomersInfo[licenseNumber].VehicleStatus == (eVehicleStatus)i_VehicleStatus)
                {
                    licenseList.Add(licenseNumber);
                }
            }

            return licenseList;
        }
    }
}
