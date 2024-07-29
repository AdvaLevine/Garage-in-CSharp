using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure;}
            set { m_CurrentAirPressure = value;}
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        private void addAir(float i_AirToAdd)
        {
            float newAirPressure = m_CurrentAirPressure + i_AirToAdd;

            if(newAirPressure > r_MaxAirPressure || newAirPressure < m_CurrentAirPressure)
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - m_CurrentAirPressure);
            }

            m_CurrentAirPressure = newAirPressure;
        }

        public void InflateToMax()
        {
            float newAirPressure = MaxAirPressure - m_CurrentAirPressure;

            addAir(newAirPressure);
        }

        public override string ToString()
        {
            return string.Format(
@"---Wheels Info---
Current Wheel Air Pressure: {0}
Max Wheel Air Pressure: {1}
Wheel's Manufacturer Name: {2}",
CurrentAirPressure,
MaxAirPressure,
Manufacturer);
        }
    }
}
