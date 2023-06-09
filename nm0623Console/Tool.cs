using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nm0623
{
    /// <summary>
    /// Class <c>Tool</c> contains the base data for the tools that can be rented.
    /// 
    /// Constructor Arguments:
    ///     
    ///     toolCode: The four-letter reference code for the tool in our system
    ///     
    /// </summary>
    public class Tool
    {
        public string ToolCode { get; set; } //Reference name of the tool in our system
        public string ToolType { get; set; } //English name for the tool
        public string Brand { get; set; } //Maker/Brand of the tool
        public decimal DailyCharge { get; set; } //Cost per day to rent the tool
        public bool WeekdayCharge { get; set; } //Do we charge for this tool on weekdays?
        public bool WeekendCharge { get; set; } //Do we charge for this tool on weekends?
        public bool HolidayCharge { get; set; } //Do we charge for this tool on holidays?


        public Tool(string toolCode)  //Uses a switch case to assign default values to the properties based on the provided tool code
        {
            switch (toolCode.ToUpper().Trim())
            {
                case "LADW":
                    ToolType = "Ladder";
                    Brand = "Werner";
                    DailyCharge = decimal.Parse("1.99");
                    WeekdayCharge = true;
                    WeekendCharge = true;
                    HolidayCharge = false;
                    break;
                case "CHNS":
                    ToolType = "Chainsaw";
                    Brand = "Stihl";
                    DailyCharge = decimal.Parse("1.49");
                    WeekdayCharge = true;
                    WeekendCharge = false;
                    HolidayCharge = true;
                    break;
                case "JAKD":
                    ToolType = "Jackhammer";
                    Brand = "DeWalt";
                    DailyCharge = decimal.Parse("2.99");
                    WeekdayCharge = true;
                    WeekendCharge = false;
                    HolidayCharge = false;
                    break;
                case "JAKR":
                    ToolType = "Jackhammer";
                    Brand = "Ridgid";
                    DailyCharge = decimal.Parse("2.99");
                    WeekdayCharge = true;
                    WeekendCharge = false;
                    HolidayCharge = false;
                    break;
                default: throw new ArgumentException("Tool code provided was not contained in registry.");
            }

            ToolCode = toolCode;
        }
    }
}
