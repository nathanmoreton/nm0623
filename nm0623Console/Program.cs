using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace nm0623
{
    internal class Program
    {
        protected internal static void Checkout(string toolCode, int rentalDayCount, int discountPercent, DateTime checkoutDate)
        {
            if (rentalDayCount < 1) { throw new ArgumentException("The provided number of days for the rental was less than one. The customer must rent the tool for a minimum of one day."); }
            if (discountPercent < 0 || discountPercent > 100) { throw new ArgumentException("The discount given was not between zero and 100. A percentage must be greater than or equal to zero and less than or equal to 100."); }

            var tool = new Tool(toolCode);
            var rentalAggrement = new RentalAgreement(tool, rentalDayCount, discountPercent, checkoutDate);

            rentalAggrement.PrintRentalAgreement();
        }

        public static void DisplayInventory()
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
        public static void Transaction()
        {
            string toolCode;
            int rentalDayCount, discountPercent;
            DateTime checkoutDate;

            Console.Clear();
            DisplayInventory();
            Console.WriteLine("Please enter the Tool Code of the tool the customer would like to rent: ");
            var a = Console.ReadLine();
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
            Console.WriteLine("Please enter the number of days the customer wishes to rent the tool for: ");
            a = Console.ReadLine();
            while (!int.TryParse(a.Trim(), out rentalDayCount) || rentalDayCount < 1) 
            {
                Console.WriteLine(String.Format("Input '{0}' is not a valid positive integer. Please enter an integer greater than or equal to 1: ", a));
                a = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(String.Format("{0} rental days approved.{1}", rentalDayCount, Environment.NewLine));
            Console.WriteLine("Please enter the discount to be applied to this order as a whole number percentage between zero and 100: ");
            a = Console.ReadLine();
            while (!int.TryParse(a.Trim(), out discountPercent) || discountPercent < 0 || discountPercent > 100)
            {
                Console.WriteLine(String.Format("Input '{0}' is not a valid integer between zero and 100. Please enter an integer greater than or equal to zero and less than or equal to 100: ", a));
                a = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(String.Format("{0}% discount approved.{1}", discountPercent, Environment.NewLine));
            Console.WriteLine("Please enter the date the item is to be rented: ");
            a = Console.ReadLine();
            while (!DateTime.TryParse(a.Trim(), out checkoutDate))
            {
                Console.WriteLine(String.Format("Input '{0}' cannot be parsed into a valid date variable. Please enter the date in the format 'mm/dd/yyyy': ", a));
                a = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine(String.Format("Checkout date successfully specified as {0}.{1}", checkoutDate.Date, Environment.NewLine));
            Console.WriteLine(String.Format("{0}{0}Transaction created successfully. Below is the rental agreement:{0}{0}", Environment.NewLine));
            Checkout(toolCode, rentalDayCount, discountPercent, checkoutDate);
        }
        static void Main(string[] args)
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
