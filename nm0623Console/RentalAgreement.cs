using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RentalAgreement : Tool
    {
        public int RentalDays { get; set; } //Number of days the customer wishes to rent the tool
        public DateTime CheckOutDate { get; set; } //Date the customer wishes to first checkout the tool
        public DateTime DueDate { get; set; } //Date the item must be returned
        public int ChargeDays { get; set; }
        public decimal PreDiscountCharge { get; set; }
        public int DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalCharge { get; set; }
        public string RentalAgreementText { get; set; }

        public RentalAgreement(string toolCode, int rentalDays, int discountPercent, DateTime checkOutDate): base(toolCode)
        {
            RentalDays = rentalDays;
            CheckOutDate = checkOutDate;
            DueDate = checkOutDate.AddDays(rentalDays);
            DiscountPercent = discountPercent;
            ChargeDays = CalculateChargeDays(this, checkOutDate, rentalDays);
            PreDiscountCharge = ChargeDays * this.DailyCharge;
            DiscountAmount = Math.Round((discountPercent == 100 ? 1.00m : Decimal.Parse("0." + discountPercent.ToString())) * PreDiscountCharge, 2);
            FinalCharge = PreDiscountCharge - DiscountAmount;
            RentalAgreementText = String.Format("Tool code: {0}", ToolCode) + Environment.NewLine +
                String.Format("Tool type: {0}", ToolType) + Environment.NewLine +
                String.Format("Brand: {0}", Brand) + Environment.NewLine +
                String.Format("Rental days: {0}", RentalDays) + Environment.NewLine +
                String.Format("Check out date: {0}", CheckOutDate.ToString("MM/dd/yy")) + Environment.NewLine +
                String.Format("Due date: {0}", DueDate.ToString("MM/dd/yy")) + Environment.NewLine +
                String.Format("Daily rental charge: {0:C}", this.DailyCharge) + Environment.NewLine +
                String.Format("Charge days: {0}", ChargeDays) + Environment.NewLine +
                String.Format("Pre-discount charge: {0:C}", PreDiscountCharge) + Environment.NewLine +
                String.Format("Discount percent: {0}%", DiscountPercent) + Environment.NewLine +
                String.Format("Discount amount: {0:C}", DiscountAmount) + Environment.NewLine +
                String.Format("Final charge: {0:C}", FinalCharge);
        }

        private static List<DateTime> GetHolidays(int year)
        {
            var holidayList = new List<DateTime>();
            DateTime independenceDay = new DateTime(year, 7, 4);
            DateTime laborDay = new DateTime(year, 9, 1);

            switch (independenceDay.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    holidayList.Add(independenceDay.AddDays(-1)); break;
                case DayOfWeek.Sunday:
                    holidayList.Add(independenceDay.AddDays(-1)); break;
                default:
                    holidayList.Add(independenceDay); break;
            }

            while (laborDay.DayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
            }

            holidayList.Add(laborDay);

            return holidayList;
        }
        private static int CalculateChargeDays(Tool tool, DateTime checkOutDate, int rentalDays)
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
        public void PrintRentalAgreement()
        {
            Console.WriteLine(RentalAgreementText);
        }
    }
}
