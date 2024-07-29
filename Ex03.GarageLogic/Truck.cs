using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const string k_DangerousMaterials = "1";
        private const string k_CargoCapacity = "2";
        private const int k_NumberOfWheelsInTruck = 12;
        private const float k_MaxTruckWheelPressure = 28f;
        private const float k_MaxTruckFuel = 120f;
        private const float k_MaxTrucCharge = 3.5f;
        private bool m_IsDangerousMaterials;
        private float m_CargoCapacity;

        public Truck() : base(k_MaxTruckFuel, k_MaxTrucCharge, k_MaxTruckWheelPressure)
        {
            NumOfWheels = k_NumberOfWheelsInTruck;
            FuelType = Enums.eFuelType.Soler;
        }

        public override string GetSpecialAttributePrompt(string i_SpecialAttributeNumber)
        {
            string attributeString = string.Empty;

            switch (i_SpecialAttributeNumber)
            {
                case k_DangerousMaterials:
                    {
                        attributeString = "Please enter if the truck is carrying dangerous materials: (1 = Yes, 0 = No)";
                        break;
                    }
                case k_CargoCapacity:
                    {
                        attributeString = "Please enter the truck's cargo capacity: ";
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
                case k_DangerousMaterials:
                    {
                        this.setDangerousMaterials(i_AttributeValue);
                        break;
                    }
                case k_CargoCapacity:
                    {
                        this.setCargoCapacity(i_AttributeValue);
                        break;
                    }
                default:
                    {
                        throw new ValueOutOfRangeException(1, 2);
                    }
            }
        }

        private void setDangerousMaterials(string i_DangerousMaterialsInput)
        {
            if (!int.TryParse(i_DangerousMaterialsInput, out int dangerousMaterials))
            {
                throw new FormatException("Invalid input! can only accept integers");
            }

            switch (dangerousMaterials)
            {
                case 0:
                    m_IsDangerousMaterials = false;
                    break;
                case 1:
                    m_IsDangerousMaterials = true;
                    break;
                default:
                    throw new ValueOutOfRangeException(0, 1);
            }
        }

        private void setCargoCapacity(string i_CargoCapacityInput)
        {
            if (!float.TryParse(i_CargoCapacityInput, out float cargoCapacity))
            {
                throw new FormatException("Invalid input! can only accept integers");
            }

            if (cargoCapacity < 0)
            {
                throw new ArgumentException("Cargo capacity cannot be negative!");
            }

            m_CargoCapacity = cargoCapacity;
        }

        public override string GetSpecialAttributesString()
        {
            return string.Format(
@"---Special Attributes Info---
Is The Truck Carrying Dangerous Materials: {0}
Truck's Cargo Capacity: {1}",
m_IsDangerousMaterials.ToString(),
m_CargoCapacity.ToString());
        }
    }
}
