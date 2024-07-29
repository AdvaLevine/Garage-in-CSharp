using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private const int k_ToPercent = 100;
        private readonly float r_MaxAmountOfEnergy;
        protected float m_CurrentEnergy;

        public Engine(float i_MaxAmountOfEnergy)
        {
            r_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
        }

        public float PercentageOfEnergy
        {
            get { return ((m_CurrentEnergy) / (r_MaxAmountOfEnergy) * k_ToPercent); }
        }

        public float MaxAmountOfEnergy
        {
            get { return r_MaxAmountOfEnergy; }
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
            set { m_CurrentEnergy = value; }
        }

        protected void AddFuelOrCharge(float i_EnergyToAdd)
        {
            float newEnergy = m_CurrentEnergy + i_EnergyToAdd;

            if (newEnergy > r_MaxAmountOfEnergy || newEnergy < m_CurrentEnergy)
            {
                throw new ValueOutOfRangeException(0, r_MaxAmountOfEnergy - m_CurrentEnergy);
            }

            m_CurrentEnergy = newEnergy;
        }
    }
}
