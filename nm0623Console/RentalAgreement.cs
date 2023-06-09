using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace nm0623
{
    /// <summary>
    /// Class <c>RentalAgreement</c> is used at checkout to generate and print the rental agreement for a given tool rental.
    /// 
    /// Constructor Arguments: 
    /// 
    ///     toolCode: An string containing the tool code to create the rental agreement on
    ///     rentalDays: An integer containing the number of days the user plans to rent the tool
    ///         - Must be one or greater
    ///     discountPercent: The percentage of the price to remove as a discount for the customer
    ///         - Must be between zero and 100
    ///     checkOutDate: A DateTime oject containing the date the customer rented the tool
    ///     
    /// </summary>
    public class RentalAgreement
    {
        public Tool Tool { get; set; }
        public int RentalDays { get; set; } //Number of days the customer wishes to rent the tool
        public DateTime CheckOutDate { get; set; } //Date the customer wishes to first checkout the tool
        public DateTime DueDate { get; set; } //Date the item must be returned. Calculated by adding the RentalDays to the CheckOutDate
        public int ChargeDays { get; set; } //Days to charge a rental fee for (total rental days minus the weekends or holidays if they are free for this tool type
        public decimal PreDiscountCharge { get; set; } //Total price of the rental before the discount percent is applied. Calculated as ChargeDays * the daily cost for this tool
        public int DiscountPercent { get; set; } //Whole number between zero and 100 that represents how much of a discount this order receives
        public decimal DiscountAmount { get; set; } //Dollar amount of the discount. Calculated as discount percentage * prediscount charge / 100
        public decimal FinalCharge { get; set; } //Final price of the rental. Calculated as PreDiscountCharge-DiscountAmount
        public string RentalAgreementText { get; set; } //Text of the rental agreement that gets generated

        public RentalAgreement(Tool tool, int rentalDays, int discountPercent, DateTime checkOutDate)
        {
            Tool = tool;
            RentalDays = rentalDays;
            CheckOutDate = checkOutDate;
            DueDate = checkOutDate.AddDays(rentalDays);
            DiscountPercent = discountPercent;
            ChargeDays = CalculateChargeDays(tool, checkOutDate, rentalDays);
            PreDiscountCharge = ChargeDays * tool.DailyCharge;
            DiscountAmount = CalculateDiscount(discountPercent, PreDiscountCharge);
            FinalCharge = PreDiscountCharge - DiscountAmount;
            RentalAgreementText = FormatRentalAgreement();
        }
        private string FormatRentalAgreement() //Gets the rental agreement text in the form of a string
        {
            return String.Format("Tool code: {0}", Tool.ToolCode) + Environment.NewLine +
                String.Format("Tool type: {0}", Tool.ToolType) + Environment.NewLine +
                String.Format("Brand: {0}", Tool.Brand) + Environment.NewLine +
                String.Format("Rental days: {0}", RentalDays) + Environment.NewLine +
                String.Format("Check out date: {0}", CheckOutDate.ToString("MM/dd/yy")) + Environment.NewLine +
                String.Format("Due date: {0}", DueDate.ToString("MM/dd/yy")) + Environment.NewLine +
                String.Format("Daily rental charge: {0:C}", Tool.DailyCharge) + Environment.NewLine +
                String.Format("Charge days: {0}", ChargeDays) + Environment.NewLine +
                String.Format("Pre-discount charge: {0:C}", PreDiscountCharge) + Environment.NewLine +
                String.Format("Discount percent: {0}%", DiscountPercent) + Environment.NewLine +
                String.Format("Discount amount: {0:C}", DiscountAmount) + Environment.NewLine +
                String.Format("Final charge: {0:C}", FinalCharge);
        }
        private decimal CalculateDiscount(int discountPercent, decimal preDiscountCharge) //Calculates the discount amount by multiplying the discount percent by the prediscount charge
        {
            return Math.Round((discountPercent == 100 ? 1.00m : Decimal.Parse("0." + discountPercent.ToString())) * preDiscountCharge, 2);
        }
        private static List<DateTime> GetHolidays(int year) //Returns a list of DateTimes that contains the holidays for the year the tools is checked out. 
        {
            var holidayList = new List<DateTime>();
            DateTime independenceDay = new DateTime(year, 7, 4);
            DateTime laborDay = new DateTime(year, 9, 1);

            //Check if Independence day is on a weekend. If it is, assign the official holiday as the nearest weekday.
            switch (independenceDay.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    holidayList.Add(independenceDay.AddDays(-1)); break;
                case DayOfWeek.Sunday:
                    holidayList.Add(independenceDay.AddDays(-1)); break;
                default:
                    holidayList.Add(independenceDay); break;
            }

            //Find the first monday in September and assign it to Labor Day
            while (laborDay.DayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
            }

            holidayList.Add(laborDay);

            return holidayList;
        }
        private static int CalculateChargeDays(Tool tool, DateTime checkOutDate, int rentalDays) //Used to calculate the actual number of days to charge by subtracting any necessary weekends or holidays.
        {
            int newRentalDays = rentalDays;
            var holidays = GetHolidays(checkOutDate.Year);

            for (int i = 0; i < rentalDays; i++)
            {
                var day = checkOutDate.AddDays(i);
                if ((!tool.WeekendCharge) && (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)) { newRentalDays--; }
                else if ((!tool.HolidayCharge) && (holidays.Contains(day))) { newRentalDays--; }
            }

            return newRentalDays;
        }
        public void PrintRentalAgreement() //Prints the rental agreement to the console
        {
            Console.WriteLine(RentalAgreementText);
        }
    }
}
