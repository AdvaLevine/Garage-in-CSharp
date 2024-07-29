using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxCharge) : base(i_MaxCharge) { }

        public void AddCharge(float i_HoursToAdd)
        {
            AddFuelOrCharge(i_HoursToAdd);
        }

        public override string ToString()
        {
            return string.Format(
@"---Electric Engine Info---
Current Engine Charge: {0}
Max Charge: {1}",
CurrentEnergy,
MaxAmountOfEnergy);
        }
    }
}
