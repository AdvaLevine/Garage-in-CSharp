using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly Garage r_Garage = new Garage();
        private const int k_ConvertMinutesToHours = 60;
        private const int k_MinMenuOption = 1;
        private const int k_MaxMenuOption = 7;
        private const int k_MaxAmoutOfDoors = 5;
        private const int k_MinAmoutOfDoors = 2;
        private const int k_GetAllVehicles = -1;
        private const int k_MinInputValue = 1;
        private const int k_NumOfFuelOptions = 4;
        private const int k_NumOfVehicleTypes = 4;
        private const char k_LowercaseThreshold = 'a';
        private const string k_FirstSpecialAttribute = "1";
        private const string k_SecondSpecialAttribute = "2";
        private const string k_EmptyString = "";


        private eUserAction displayMenuAndGetUserInput()
        {
            string menuToPrint;
            int userChoice;

            menuToPrint = string.Format(
                @"The possible choices are:
1) Insert a vehicle
2) Display a list of all licenses numbers currently avaliable in the garage
3) Change the vehicle's state in garage
4) Inflate wheels to the maximum 
5) Fuel vehicle (for fuel based vehicles)
6) Charge vehicle (for electric vehicles)
7) Display vehicle info

Please choose an option: ({0} to {1})", k_MinMenuOption, k_MaxMenuOption);
            userChoice = getAndValidateIntInRange(k_MinMenuOption, k_MaxMenuOption, menuToPrint);
            Console.Clear();

            return (eUserAction)userChoice;
        }

        private int getAndValidateIntInRange(int i_MinValue, int i_MaxValue, string i_MessageForUser = k_EmptyString)
        {
            int inputNumber = -1;
            bool isValidInputNumber = false;
            bool isNumberInRange;
            string inputString;
            bool isInputANumber;

            if (!string.IsNullOrEmpty(i_MessageForUser))
            {
                Console.WriteLine(i_MessageForUser);
            }

            while (!isValidInputNumber)
            {
                inputString = getNonEmptyUserInput();
                isInputANumber = int.TryParse(inputString, out inputNumber);
                if (isInputANumber)
                {
                    isNumberInRange = (inputNumber >= i_MinValue) && (inputNumber <= i_MaxValue);
                    isValidInputNumber = isNumberInRange;
                }

                if (!isValidInputNumber)
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine(@"Invalid input,
Please enter a number from {0} to {1}", i_MinValue, i_MaxValue);
                }
            }

            return inputNumber;
        }

        private float getAndValidateFloatInRange(float i_MinNumber, float i_MaxNumber, string i_Message)
        {
            float inputNumber = -1;
            bool isValidInputNumber = false;
            bool isANumber;
            bool isNumberInRange;
            string inputString;

            Console.WriteLine(i_Message);
            while (!isValidInputNumber)
            {
                inputString = getNonEmptyUserInput();
                isANumber = float.TryParse(inputString, out inputNumber);
                if (isANumber)
                {
                    isNumberInRange = inputNumber >= i_MinNumber && inputNumber <= i_MaxNumber;
                    isValidInputNumber = isNumberInRange;
                }

                if (!isValidInputNumber)
                {
                    Console.WriteLine(string.Format(@"Invalid input
Please enter a number from {0} to {1}: ", i_MinNumber, i_MaxNumber));
                }
            }

            return inputNumber;
        }

        private string getNonEmptyUserInput(string i_MessageForUser = k_EmptyString)
        {
            string userInput;

            if (!string.IsNullOrEmpty(i_MessageForUser))
            {
                Console.WriteLine(i_MessageForUser);
            }

            userInput = Console.ReadLine();
            while (userInput == string.Empty)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine(@"Invalid input, Input can't be empty, 
Please insert a new input");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private string addSpacesBetweenStrings(string i_StringToSpace)
        {
            string spacedString = i_StringToSpace[0].ToString();

            for (int i = 1; i < i_StringToSpace.Length; i++)
            {
                if (i_StringToSpace[i] >= k_LowercaseThreshold)
                {
                    spacedString += string.Format("{0}", i_StringToSpace[i]);
                }
                else
                {
                    spacedString += string.Format(" {0}", i_StringToSpace[i]);
                }
            }

            return spacedString;
        }

        private string getOnlyDigitsString(string i_MessageForUser = k_EmptyString)
        {
            string userInput = getNonEmptyUserInput(i_MessageForUser);

            while (!userInput.All(Char.IsDigit))
            {
                Console.WriteLine(@"Invalid input
The input should only contain digits, please try again: ");
                userInput = getNonEmptyUserInput();
            }

            return userInput;
        }

        private void promptUserToPressEnterToContinue()
        {
            Console.WriteLine("Press 'Enter' to continue");
            Console.ReadLine();
            Console.Clear();
        }

        private string licenseNumberToString(List<string> io_LicenseNumbers)
        {
            StringBuilder licenseNumbersToPrint = new StringBuilder();

            if (io_LicenseNumbers.Count != 0)
            {
                licenseNumbersToPrint.AppendLine("The current license numbers in the garage are: ");
                foreach (string licenseNumber in io_LicenseNumbers)
                {
                    licenseNumbersToPrint.AppendLine(licenseNumber);
                }
            }
            else
            {
                licenseNumbersToPrint.Append("There are no license numbers to display currently.");
            }

            return licenseNumbersToPrint.ToString();
        }

        private float getValidFloatNumber()
        {
            float inputNumberFromUser;
            bool isANumberInput;
            string inputFromUser;

            do
            {
                inputFromUser = getNonEmptyUserInput();
                isANumberInput = float.TryParse(inputFromUser, out inputNumberFromUser);
                if (!isANumberInput)
                {
                    Console.WriteLine("Invalid input, please try again: ");
                }
            }
            while (!isANumberInput);

            return inputNumberFromUser;
        }

        private void insertVehicle()
        {
            string licenseNumber;
            string MessageToUser;

            Console.WriteLine(@"Please enter the license number of the required vehicle: ");
            licenseNumber = getNonEmptyUserInput();
            if (r_Garage.IsVehicleInGarage(licenseNumber))
            {
                MessageToUser = string.Format(
@"Vehicle with license number '{0}' has now changed to state: In Repairing", licenseNumber);
                Console.WriteLine(MessageToUser);
                r_Garage.ChangeVehicleState(licenseNumber, eVehicleStatus.InRepair);
                promptUserToPressEnterToContinue();
            }
            else
            {
                Vehicle newVehicle = createNewVehicle(licenseNumber);
                CustomerInfo newCustomer = createNewCustomer();

                r_Garage.InsertVehicleToGarage(newVehicle, newCustomer);
            }
        }

        private Vehicle createNewVehicle(string i_LicenseNumber)
        {
            int inputTypeOfVehicleNumber;
            string modelName;
            string wheelsManufacturer;
            float wheelsCurrentAirPressure;
            float currentEnergyLeft;
            List<Wheel> wheelsList;
            Vehicle newVehicle;

            inputTypeOfVehicleNumber = getTypeOfVehicleNumber();
            newVehicle = VehicleFactory.CreateVehicle((eVehicleType)inputTypeOfVehicleNumber);
            newVehicle.LicenseNumber = i_LicenseNumber;
            modelName = getNonEmptyUserInput("Please enter the vehicle's model name: ");
            newVehicle.ModelName = modelName;
            wheelsManufacturer = getNonEmptyUserInput("Please enter the wheels manufacturer's name: ");
            wheelsCurrentAirPressure = getAndValidateFloatInRange(0, newVehicle.MaxWheelPressure, "Please enter the current air pressure of the wheels: ");
            wheelsList = VehicleFactory.CreateWheels(newVehicle.NumOfWheels, wheelsManufacturer, wheelsCurrentAirPressure, newVehicle.MaxWheelPressure);
            newVehicle.Wheels = wheelsList;
            currentEnergyLeft = getAndValidateFloatInRange(0, newVehicle.Engine.MaxAmountOfEnergy, "Please enter the amount of fuel/charging hours left for the vehicle: ");
            newVehicle.Engine.CurrentEnergy = currentEnergyLeft;
            updateExtraVehicleFeatures(newVehicle);

            return newVehicle;
        }

        private CustomerInfo createNewCustomer()
        {
            string clientName = getNonEmptyUserInput("Please enter your name: ");
            string clientPhoneNumber = getOnlyDigitsString("Please enter your phone number: ");
            CustomerInfo clientInfo = new CustomerInfo(clientName, clientPhoneNumber);

            return clientInfo;
        }

        private int getTypeOfVehicleNumber()
        {
            StringBuilder messageToPrint = new StringBuilder();
            string finalMessage;
            int currentLocation = 1;
            int maxInputValue = Enum.GetValues(typeof(eVehicleType)).Length;

            Console.WriteLine("Please choose a vehicle type: ");
            foreach (eVehicleType currentType in Enum.GetValues(typeof(eVehicleType)))
            {
                messageToPrint.Append(string.Format(
@"  {0}) {1}{2}", currentLocation, addSpacesBetweenStrings(currentType.ToString()), Environment.NewLine));
                currentLocation++;
            }

            finalMessage = messageToPrint.ToString().TrimEnd();
            Console.WriteLine(finalMessage);

            return getAndValidateIntInRange(k_MinInputValue, maxInputValue);
        }

        private void updateExtraVehicleFeatures(Vehicle io_Vehicle)
        {
            bool success = false;

            while (!success)
            {
                try
                {
                    getSpecialAttributeFromUser(io_Vehicle, k_FirstSpecialAttribute);
                    getSpecialAttributeFromUser(io_Vehicle, k_SecondSpecialAttribute);
                    success = true;
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                    promptUserToPressEnterToContinue();
                }
                catch (ValueOutOfRangeException valueOutOfRange)
                {
                    Console.WriteLine(valueOutOfRange.Message);
                    promptUserToPressEnterToContinue();
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    promptUserToPressEnterToContinue();
                }
            }

            promptUserToPressEnterToContinue();
        }

        private void getSpecialAttributeFromUser(Vehicle io_Vehicle, string i_AttributeNumber)
        {
            bool isValidInput = false;
            string messageForUser = string.Empty;
            string usersAnswer = string.Empty;

            while (!isValidInput)
            {
                try
                {
                    messageForUser = io_Vehicle.GetSpecialAttributePrompt(i_AttributeNumber);
                    Console.WriteLine(messageForUser);
                    usersAnswer = Console.ReadLine();
                    io_Vehicle.SetAttribute(i_AttributeNumber, usersAnswer);
                    isValidInput = true;
                }
                catch (ValueOutOfRangeException valueOutOfRange)
                {
                    Console.WriteLine(valueOutOfRange.Message);
                    Console.WriteLine("Please try again.");
                }
            }
        }

        private void currentLicenseNumbersInGarage()
        {
            List<string> licenseNumbers;
            int filterNumber;
            int maxInputValue = 4;
            int minInputValue = 1;
            string licenseNumbersAsString = string.Empty;
            string messageToPrint =
@"How would you like to filter the results:
1) Being repaired
2) Repair is done
3) Paid
4) No filter

Please choose an option (1 to 4):";

            filterNumber = getAndValidateIntInRange(minInputValue, maxInputValue, messageToPrint);
            if (filterNumber >= maxInputValue)
            {
                licenseNumbers = r_Garage.GetLicenseListByVehicleState(k_GetAllVehicles);
            }
            else
            {
                licenseNumbers = r_Garage.GetLicenseListByVehicleState(filterNumber);
            }

            licenseNumbersAsString = licenseNumberToString(licenseNumbers);
            Console.Clear();
            Console.WriteLine(licenseNumbersAsString);
            promptUserToPressEnterToContinue();
        }

        private bool isUserWantToCancel()
        {
            bool isUserNotDone;
            int userInputNumber;
            int maxInputValue = 2;
            int minInputValue = 1;
            string messageToPrint =
@"Would you like to try again or go back to the main menu:
1) Try again
2) Go back";

            userInputNumber = getAndValidateIntInRange(minInputValue, maxInputValue, messageToPrint);
            isUserNotDone = userInputNumber == minInputValue;

            return isUserNotDone;
        }

        private string getLicenseNumberInGarage()
        {
            string licenserNumber;
            bool isUserNotDone = true;
            bool isLicenseNumberInGarage;

            Console.WriteLine("Please enter the vehicle's license number: ");
            licenserNumber = getNonEmptyUserInput();
            isLicenseNumberInGarage = r_Garage.IsVehicleInGarage(licenserNumber);
            if (!r_Garage.IsGarageEmpty())
            {
                while (isUserNotDone && (!isLicenseNumberInGarage))
                {
                    Console.WriteLine("This vehicle is not in the garage currently");
                    isUserNotDone = isUserWantToCancel();
                    if (isUserNotDone == false)
                    {
                        licenserNumber = string.Empty;
                        break;
                    }

                    Console.WriteLine("Try again: ");
                    licenserNumber = getNonEmptyUserInput();
                    isLicenseNumberInGarage = r_Garage.IsVehicleInGarage(licenserNumber);
                }
            }
            else
            {
                Console.WriteLine("The grarge is currently empty.");
                licenserNumber = string.Empty;
            }

            return licenserNumber;
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getLicenseNumberInGarage();
            bool isVaildLicenseNumber = licenseNumber != string.Empty;

            if (isVaildLicenseNumber)
            {
                string messageToPrint =
          @"The possible new states for the vehicle:
1) The vehicle is being repaired
2) The vehicle repair is done
3) Paid

Please choose an option (1 to 3): ";
                int userVehicleState = getAndValidateIntInRange(k_MinInputValue, k_NumOfVehicleTypes, messageToPrint);

                r_Garage.ChangeVehicleState(licenseNumber, (eVehicleStatus)userVehicleState);
            }
            else
            {
                promptUserToPressEnterToContinue();
            }
        }

        private void inflateWheelsToMax()
        {
            string licenseNumber = getLicenseNumberInGarage();
            bool userWantToQuit = licenseNumber == string.Empty;

            if (!userWantToQuit && !r_Garage.IsGarageEmpty())
            {
                r_Garage.InflateWheelsToMax(licenseNumber);
            }
            else
            {
                promptUserToPressEnterToContinue();
            }
        }

        private void fuelVehicle()
        {
            string licenseNumber = getLicenseNumberInGarage();
            bool userWantToQuit = licenseNumber == string.Empty;

            if (!userWantToQuit)
            {
                bool isEngineFuel = r_Garage.GetVehicleByLicenseNumber(licenseNumber).Engine is FuelEngine;

                if (!isEngineFuel)
                {
                    Console.WriteLine("The vehicle is an electric vehicle, you can't fuel it");
                    promptUserToPressEnterToContinue();
                }

                if (!r_Garage.IsGarageEmpty() && isEngineFuel)
                {
                    bool success = false;
                    string messageToPrint =
                @"Please choose fuel type:
1) Soler
2) Octan 95
3) Octan 96
4) Octan 98

Please choose an option: ";

                    while (!success)
                    {
                        try
                        {
                            float amountOfFuel;
                            int fuelType = getAndValidateIntInRange(k_MinInputValue, k_NumOfFuelOptions, messageToPrint);

                            Console.WriteLine("How many liters of fuel would you want to add?");
                            amountOfFuel = getValidFloatNumber();
                            r_Garage.FuelVehicle(licenseNumber, (eFuelType)fuelType, amountOfFuel);
                            success = true;
                        }
                        catch (ArgumentException argumentException)
                        {
                            Console.WriteLine(argumentException.Message);
                            Console.WriteLine("Please try again.");
                            promptUserToPressEnterToContinue();
                        }
                        catch (ValueOutOfRangeException valueOutOfRange)
                        {
                            Console.WriteLine(valueOutOfRange.Message);
                            Console.WriteLine("Please try again.");
                            promptUserToPressEnterToContinue();
                        }
                    }
                }
            }
            else
            {
                promptUserToPressEnterToContinue();
            }
        }

        private void chargeVehicle()
        {
            string licenseNumber = getLicenseNumberInGarage();
            bool userWantToQuit = licenseNumber == string.Empty;

            if (!userWantToQuit)
            {
                bool isEngineElectric = r_Garage.GetVehicleByLicenseNumber(licenseNumber).Engine is ElectricEngine;

                if (!isEngineElectric)
                {
                    Console.WriteLine("The vehicle is a fuel based vehicle, you can't charge it");
                    promptUserToPressEnterToContinue();
                }

                if (!r_Garage.IsGarageEmpty() && isEngineElectric)
                {
                    bool success = false;

                    while (!success)
                    {
                        Console.WriteLine("How many minutes would you want to add? ");
                        try
                        {
                            float amountOfMinutesToCharge = getValidFloatNumber();

                            r_Garage.ChargeVehicle(licenseNumber, amountOfMinutesToCharge / k_ConvertMinutesToHours);
                            success = true;
                        }
                        catch (ArgumentException argumentException)
                        {
                            Console.WriteLine(argumentException.Message);
                            Console.WriteLine("Please try again.");
                            promptUserToPressEnterToContinue();
                        }
                        catch (ValueOutOfRangeException valueOutOfRange)
                        {
                            Console.WriteLine(valueOutOfRange.Message);
                            Console.WriteLine("Please try again.");
                            promptUserToPressEnterToContinue();
                        }
                    }
                }
            }
            else
            {
                promptUserToPressEnterToContinue();
            }
        }

        private void showVehicleInfo()
        {
            string licenseNumber = getLicenseNumberInGarage();
            bool isValidLicense = licenseNumber != string.Empty;

            if (!r_Garage.IsGarageEmpty() && isValidLicense)
            {
                Console.WriteLine(r_Garage.VehicleInfoToString(licenseNumber));
            }

            promptUserToPressEnterToContinue();
        }

        public void ManageGarage()
        {
            bool isExit = false;

            Console.WriteLine("Welcome To Our Garage!");
            while (!isExit)
            {
                switch (displayMenuAndGetUserInput())
                {
                    case eUserAction.InsertVehicle:
                        {
                            Console.Clear();
                            insertVehicle();
                            break;
                        }
                    case eUserAction.DisplayAllLicenseNumbers:
                        {
                            Console.Clear();
                            currentLicenseNumbersInGarage();
                            break;
                        }
                    case eUserAction.ChangeVehicleState:
                        {
                            Console.Clear();
                            changeVehicleStatus();
                            break;
                        }
                    case eUserAction.InflateTyersToMax:
                        {
                            Console.Clear();
                            inflateWheelsToMax();
                            break;
                        }
                    case eUserAction.FuelVehicle:
                        {
                            Console.Clear();
                            fuelVehicle();
                            break;
                        }
                    case eUserAction.ChargeVehicle:
                        {
                            Console.Clear();
                            chargeVehicle();
                            break;
                        }
                    case eUserAction.DisplayVehicle:
                        {
                            Console.Clear();
                            showVehicleInfo();
                            break;
                        }
                }

                Console.Clear();
            }
        }
    }
}