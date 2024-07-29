using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const string k_LicenseTypeString = "1";
        private const string k_EngineCapacityString = "2";
        private const int k_MinLicenseNum = 1;
        private const int k_MaxLicenseNum = 4;
        private const int k_NumberOfWheelsInMotorcycle = 2;
        private const float k_MaxMotorcycleWheelPressure = 33f;
        private const float k_MaxMotorcycleFuel = 5.5f;
        private const float k_MaxMotorcycleCharge = 2.5f;
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle() : base(k_MaxMotorcycleFuel, k_MaxMotorcycleCharge, k_MaxMotorcycleWheelPressure)
        {
            NumOfWheels = k_NumberOfWheelsInMotorcycle;
            FuelType = Enums.eFuelType.Octan98;
        }

        public override string GetSpecialAttributePrompt(string i_SpecialAttributeNumber)
        {
            string attributeString = string.Empty;

            switch (i_SpecialAttributeNumber)
            {
                case k_LicenseTypeString:
                    {
                        attributeString = "Please enter the motorcycle's license (A=1 , A1=2, AA=3, B1=4): ";
                        break;
                    }
                case k_EngineCapacityString:
                    {
                        attributeString = "Please enter the motorcycle's engine capacity: ";
                        break;
                    }
                default:
                    {
                        throw new ValueOutOfRangeException(1, 2);
                    }
            }

            return attributeString;
        }

        public override void SetAttribute(string i_AttributeNum, string i_AttributeValue)
        {
            switch (i_AttributeNum)
            {
                case k_LicenseTypeString:
                    {
                        this.setLicenseType(i_AttributeValue);
                        break;
                    }
                case k_EngineCapacityString:
                    {
                        this.setEngineCapacity(i_AttributeValue);
                        break;
                    }
                default:
                    {
                        throw new ValueOutOfRangeException(1, 2);
                    }
            }
        }

        private void setLicenseType(string i_LicenseInput)
        {
            if (!int.TryParse(i_LicenseInput, out int licenseInput))
            {
                throw new FormatException("Invalid input! can only accept integers");
            }

            if (licenseInput < k_MinLicenseNum || licenseInput > k_MaxLicenseNum)
            {
                throw new ValueOutOfRangeException(k_MinLicenseNum, k_MaxLicenseNum);
            }

            m_LicenseType = (eLicenseType)licenseInput;
        }

        private void setEngineCapacity(string i_EngineCapacity)
        {
            if (!int.TryParse(i_EngineCapacity, out int engineCapacity))
            {
                throw new FormatException("Invalid input! can only accept integers");
            }

            if (engineCapacity < 0)
            {
                throw new ArgumentException("Engine capacity cannot be negative!");
            }

            m_EngineCapacity = engineCapacity;
        }

        public override string GetSpecialAttributesString()
        {
            return string.Format(
@"---Special Attributes Info---
Motorcycle's License Type: {0}
Motorcycle's Engine Capacity: {1}",
m_LicenseType.ToString(),
m_EngineCapacity.ToString());
        }
    }
}
