using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const string k_CarColor = "1";
        private const string k_NumberOfDoors = "2";
        private const int k_MinColorNum = 1;
        private const int k_MaxColorNum = 4;
        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private const int k_NumberOfWheelsInCar = 5;
        private const float k_MaxCarWheelPressure = 31f;
        private const float k_MaxCarFuel = 45f;
        private const float k_MaxCarCharge = 3.5f;
        private int m_NumOfDoors;
        private eCarColor m_CarColor;

        public Car() : base(k_MaxCarFuel, k_MaxCarCharge, k_MaxCarWheelPressure)
        {
            NumOfWheels = k_NumberOfWheelsInCar;
            FuelType = Enums.eFuelType.Octan95;
        }

        public override string GetSpecialAttributePrompt(string i_SpecialAttributeNumber)
        {
            string attributeString = string.Empty;

            switch (i_SpecialAttributeNumber)
            {
                case k_CarColor:
                {
                    attributeString = "Please choose a car color (Yellow=1, White=2, Red=3, Black=4): ";
                    break;
                }
                case k_NumberOfDoors:
                {
                    attributeString = "Please choose number of doors (2 to 5): ";
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
                case k_CarColor:
                    {
                        this.setCarColor(i_AttributeValue);
                        break;
                    }
                case k_NumberOfDoors:
                    {
                        this.setNumOfDoors(i_AttributeValue);
                        break;
                    }
                default:
                    {
                        throw new ValueOutOfRangeException(1, 2);
                    }
            }
        }

        private void setNumOfDoors(string i_NumOfDoorsInput)
        {
            if(!int.TryParse(i_NumOfDoorsInput, out int numOfDoors))
            {
                throw new FormatException("Invalid input! can only accept integers");
            }

            if(numOfDoors < k_MinNumOfDoors || numOfDoors > k_MaxNumOfDoors)
            {
                throw new ValueOutOfRangeException(k_MinNumOfDoors, k_MaxNumOfDoors);
            }

            m_NumOfDoors = numOfDoors;
        }

        private void setCarColor(string i_CarColorInput)
        {
            if (!int.TryParse(i_CarColorInput, out int carColor))
            {
                throw new FormatException("Invalid input! can only accept integers");
            }

            if (carColor < k_MinColorNum || carColor > k_MaxColorNum)
            {
                throw new ValueOutOfRangeException(k_MinColorNum, k_MaxColorNum);
            }

            m_CarColor = (eCarColor)carColor;
        }

        public override string GetSpecialAttributesString()
        {
            return string.Format(
@"---Special Attributes Info---
Car Color: {0}
Number of Doors: {1}",
m_CarColor.ToString(),
m_NumOfDoors.ToString());
        }
    }
}
