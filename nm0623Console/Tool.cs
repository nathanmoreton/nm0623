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
    internal class Tool
    {
        protected internal string ToolCode { get; set; } //Reference name of the tool in our system
        protected internal string ToolType { get; set; } //English name for the tool
        protected internal string Brand { get; set; } //Maker/Brand of the tool
        protected internal decimal DailyCharge { get; set; } //Cost per day to rent the tool
        protected internal bool WeekdayCharge { get; set; } //Do we charge for this tool on weekdays?
        protected internal bool WeekendCharge { get; set; } //Do we charge for this tool on weekends?
        protected internal bool HolidayCharge { get; set; } //Do we charge for this tool on holidays?


        protected internal Tool(string toolCode)
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
                    Brand = "Rigid";
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
