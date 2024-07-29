using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CustomerInfo
    {
        private string m_Name;
        private string m_PhoneNumber;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InRepair;

        public CustomerInfo(string i_Name, string i_PhoneNumber) 
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public override string ToString()
        {
            return string.Format(
@"---Customer Info---
Owner Name: {0}
Current State: {1}",
Name,
m_VehicleStatus.ToString());
        }
    }
}
