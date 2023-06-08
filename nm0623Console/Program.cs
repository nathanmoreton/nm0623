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
        static void Main(string[] args)
        {
            Checkout("LADW", 3, 10, Convert.ToDateTime("07/02/20"));
            Console.WriteLine();
            Checkout("CHNS", 5, 25, Convert.ToDateTime("07/02/15"));
            Console.WriteLine();
            Checkout("JAKD", 6, 0, Convert.ToDateTime("09/03/15"));
            Console.WriteLine();
            Checkout("JAKR", 9, 0, Convert.ToDateTime("07/02/15"));
            Console.WriteLine();
            Checkout("JAKR", 4, 50, Convert.ToDateTime("07/02/20"));
            Console.WriteLine();
        }
    }
}
