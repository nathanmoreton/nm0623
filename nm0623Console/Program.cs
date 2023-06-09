using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace nm0623
{    
    /// <summary>
    /// 
     /// Class <c>Program</c> is used at as the Main .cs file. It contains the Checkout() function, which is used to generate the final rental agreement based on the input arguments.
     /// 
     /// Checkout Arguments:
     ///    toolCode: String containing the four-letter reference code for the tool in our system
     ///    rentalDayCount: Int containing the number of days the customer wishes to rent the tool. Must be a whole number greater than one
     ///    discountPercent: Int containing the discount to apply to the order as a whole-number percentage. Must be between zero and 100
     ///    checkoutDate: Date the customer first plans to checkout the tool
     ///    
     /// </summary>
    public static class Program
    {
        public static void Checkout(string toolCode, int rentalDayCount, int discountPercent, DateTime checkoutDate) 
        {
            //Throws an error if the user has requested an invalid number of days to rent the item
            if (rentalDayCount < 1) { throw new ArgumentException("The provided number of days for the rental was less than one. The customer must rent the tool for a minimum of one day."); }

            //Throws an error is the discount percent provided by the user is invalid
            if (discountPercent < 0 || discountPercent > 100) { throw new ArgumentException("The discount given was not between zero and 100. A percentage must be greater than or equal to zero and less than or equal to 100."); } 

            //Initializes a Tool object to use in the Rental Agreement
            var tool = new Tool(toolCode); 

            //Declares and initializes a RentalAgreement object using the arguments and the Tool object created above.
            var rentalAggrement = new RentalAgreement(tool, rentalDayCount, discountPercent, checkoutDate); 

            //Prints the generated rental agreement
            rentalAggrement.PrintRentalAgreement();
        }

        public static void DisplayInventory() //Useful for when calling this program from a console. This function lists all of tools available to rent.
        {
            Console.WriteLine(String.Format("Available tools for rent: {0}{0}", Environment.NewLine));
            Console.WriteLine(String.Format("Tool Code:  {1}{0}" +
                "Tool Type:  {2}{0}" +
                "Brand:      {3}{0}" +
                "Daily rate: {4}{0}{0}---------------------------{0}{0}", Environment.NewLine, "CHNS", "Chainsaw", "Stihl", "1.49"));
            Console.WriteLine(String.Format("Tool Code:  {1}{0}" +
                "Tool Type:  {2}{0}" +
                "Brand:      {3}{0}" +
                "Daily rate: {4}{0}{0}---------------------------{0}{0}", Environment.NewLine, "LADW", "Ladder", "Werner", "1.99"));
            Console.WriteLine(String.Format("Tool Code:  {1}{0}" +
                "Tool Type:  {2}{0}" +
                "Brand:      {3}{0}" +
                "Daily rate: {4}{0}{0}---------------------------{0}{0}", Environment.NewLine, "JAKD", "Jackhammer", "DeWalt", "2.99"));
            Console.WriteLine(String.Format("Tool Code:  {1}{0}" +
                "Tool Type:  {2}{0}" +
                "Brand:      {3}{0}" +
                "Daily rate: {4}{0}", Environment.NewLine, "JAKR", "Jackhammer", "Ridgid", "2.99"));
        }
        public static void Transaction() //Useful when calling this program from a console. This function can be used as a terminal GUI for renting out equipment. 
        {
            //Declare variables to be retrieved from the user
            string toolCode;
            int rentalDayCount, discountPercent;
            DateTime checkoutDate;

            //Clean up the console screen and prompt the user for the code of the tool the customer would like to rent
            Console.Clear();
            DisplayInventory();
            Console.WriteLine("Please enter the Tool Code of the tool the customer would like to rent: ");
            var a = Console.ReadLine();

            //Check that the provided code matches one in our system. If it does not, continue to clean up the screen and reprompt user until an acceptable code has been entered. Then assign the acceptable code to the toolCode variable.
            while (!(a.ToUpper().Trim() == "CHNS" || a.ToUpper().Trim() == "LADW" || a.ToUpper().Trim() == "JAKD" || a.ToUpper().Trim() == "JAKR"))
            {
                Console.Clear();
                Console.WriteLine(String.Format("Entry '{0}' invalid.{1}{1}", a, Environment.NewLine));
                DisplayInventory();
                Console.WriteLine("Please enter the Tool Code of the tool the customer would like to rent: ");
                a = Console.ReadLine();
            }
            toolCode = a.ToUpper().Trim();
            Console.Clear();
            Console.WriteLine(String.Format("Tool code {0} chosen successfully.{1}", toolCode, Environment.NewLine));

            // Prompt user for the number of days to rent the tool for
            Console.WriteLine("Please enter the number of days the customer wishes to rent the tool for: ");
            a = Console.ReadLine();

            //Check that the provided value is a positive integer. If it isn't, continue to prompt the user for a positive integer until one is provided and assign it to rentalDayCount
            while (!int.TryParse(a.Trim(), out rentalDayCount) || rentalDayCount < 1) 
            {
                Console.WriteLine(String.Format("Input '{0}' is not a valid positive integer. Please enter an integer greater than or equal to 1: ", a));
                a = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(String.Format("{0} rental days approved.{1}", rentalDayCount, Environment.NewLine));

            //Prompt user for the discount percent
            Console.WriteLine("Please enter the discount to be applied to this order as a whole number percentage between zero and 100: ");
            a = Console.ReadLine();

            //Check that the provided value is an integer between zero and 100. If it isn't continue to prompt the user until they enter a number that fits the criteria and assign it to discountPercent
            while (!int.TryParse(a.Trim(), out discountPercent) || discountPercent < 0 || discountPercent > 100)
            {
                Console.WriteLine(String.Format("Input '{0}' is not a valid integer between zero and 100. Please enter an integer greater than or equal to zero and less than or equal to 100: ", a));
                a = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(String.Format("{0}% discount approved.{1}", discountPercent, Environment.NewLine));

            //Prompt the user for the date the item needs to be checked out on
            Console.WriteLine("Please enter the date the item is to be rented: ");
            a = Console.ReadLine();

            //Check that the provided value is a date. If it isn't continue to prompt the user until they provide a valid date and assign it to the checkoutDate variable
            while (!DateTime.TryParse(a.Trim(), out checkoutDate))
            {
                Console.WriteLine(String.Format("Input '{0}' cannot be parsed into a valid date variable. Please enter the date in the format 'mm/dd/yyyy': ", a));
                a = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(String.Format("Checkout date successfully specified as {0}.{1}", checkoutDate.Date, Environment.NewLine));
            Console.WriteLine(String.Format("{0}{0}Transaction created successfully. Below is the rental agreement:{0}{0}", Environment.NewLine));

            //Call Checkout() to generate the rental agreement
            Checkout(toolCode, rentalDayCount, discountPercent, checkoutDate);
        }
        static void Main(string[] args) //Generates as many rental agreements as the user needs by prompting the user for necessary data
        {
            string answer;
            do
            {
                Transaction();
                Console.WriteLine(Environment.NewLine + "Would you like to rent another tool? Type 'y' or 'Y' for yes, press enter or type any other key for no:");
                answer = Console.ReadLine();
            } while (answer.ToUpper().Trim() == "Y");
        }
    }
}
