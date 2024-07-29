using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public FuelEngine(float i_MaxFuel, eFuelType i_FuelType) : base(i_MaxFuel)
        {
            m_FuelType = i_FuelType;
        }

        public void AddFuel(float i_AmountOfFuelToAdd, eFuelType i_FuelType)
        {
            if(i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Unmatching Fuel Type!");
            }

            AddFuelOrCharge(i_AmountOfFuelToAdd);
        }

        public override string ToString()
        {
            return string.Format(
@"---Fuel Engine Info---
Current Amount Of Fuel: {0}
Max Amount Of Fuel: {1}
Fuel Type: {2}",
CurrentEnergy,
MaxAmountOfEnergy,
m_FuelType.ToString());
        }
    }
}
