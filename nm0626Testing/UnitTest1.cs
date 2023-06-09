using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using nm0623;

namespace nm0626Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string toolCode = "JAKR";
            int rentalDays = 5;
            int discountPercent = 101;
            DateTime date = Convert.ToDateTime("09/03/15");

            //Assert
            Assert.ThrowsException<System.ArgumentException>(() => Program.Checkout(toolCode, rentalDays, discountPercent, date));
        }
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            string toolCode = "LADW";
            int rentalDays = 3;
            int discountPercent = 10;
            DateTime date = Convert.ToDateTime("07/02/20");
            var expected = "Tool code: LADW" + Environment.NewLine +
                "Tool type: Ladder" + Environment.NewLine +
                "Brand: Werner" + Environment.NewLine +
                "Rental days: 3" + Environment.NewLine +
                "Check out date: 07/02/20" + Environment.NewLine +
                "Due date: 07/05/20" + Environment.NewLine +
                "Daily rental charge: $1.99" + Environment.NewLine +
                "Charge days: 2" + Environment.NewLine +
                "Pre-discount charge: $3.98" + Environment.NewLine +
                "Discount percent: 10%" + Environment.NewLine +
                "Discount amount: $0.40" + Environment.NewLine +
                "Final charge: $3.58";

            //Act
            var rentalAgreement1 = new RentalAgreement(toolCode, rentalDays, discountPercent, date);

            //Assert
            Assert.AreEqual(expected, rentalAgreement1.RentalAgreementText);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            string toolCode = "CHNS";
            int rentalDays = 5;
            int discountPercent = 25;
            DateTime date = Convert.ToDateTime("07/02/15");
            var expected = "Tool code: CHNS" + Environment.NewLine +
                "Tool type: Chainsaw" + Environment.NewLine +
                "Brand: Stihl" + Environment.NewLine +
                "Rental days: 5" + Environment.NewLine +
                "Check out date: 07/02/15" + Environment.NewLine +
                "Due date: 07/07/15" + Environment.NewLine +
                "Daily rental charge: $1.49" + Environment.NewLine +
                "Charge days: 3" + Environment.NewLine +
                "Pre-discount charge: $4.47" + Environment.NewLine +
                "Discount percent: 25%" + Environment.NewLine +
                "Discount amount: $1.12" + Environment.NewLine +
                "Final charge: $3.35";

            //Act
            var rentalAgreement1 = new RentalAgreement(toolCode, rentalDays, discountPercent, date);

            //Assert
            Assert.AreEqual(expected, rentalAgreement1.RentalAgreementText);
        }
        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            string toolCode = "JAKD";
            int rentalDays = 6;
            int discountPercent = 0;
            DateTime date = Convert.ToDateTime("09/03/15");
            var expected = "Tool code: JAKD" + Environment.NewLine +
                "Tool type: Jackhammer" + Environment.NewLine +
                "Brand: DeWalt" + Environment.NewLine +
                "Rental days: 6" + Environment.NewLine +
                "Check out date: 09/03/15" + Environment.NewLine +
                "Due date: 09/09/15" + Environment.NewLine +
                "Daily rental charge: $2.99" + Environment.NewLine +
                "Charge days: 3" + Environment.NewLine +
                "Pre-discount charge: $8.97" + Environment.NewLine +
                "Discount percent: 0%" + Environment.NewLine +
                "Discount amount: $0.00" + Environment.NewLine +
                "Final charge: $8.97";

            //Act
            var rentalAgreement1 = new RentalAgreement(toolCode, rentalDays, discountPercent, date);

            //Assert
            Assert.AreEqual(expected, rentalAgreement1.RentalAgreementText);
        }
        [TestMethod]
        public void TestMethod5()
        {
            //Arrange
            string toolCode = "JAKR";
            int rentalDays = 9;
            int discountPercent = 0;
            DateTime date = Convert.ToDateTime("07/02/15");
            var expected = "Tool code: JAKR" + Environment.NewLine +
                "Tool type: Jackhammer" + Environment.NewLine +
                "Brand: Ridgid" + Environment.NewLine +
                "Rental days: 9" + Environment.NewLine +
                "Check out date: 07/02/15" + Environment.NewLine +
                "Due date: 07/11/15" + Environment.NewLine +
                "Daily rental charge: $2.99" + Environment.NewLine +
                "Charge days: 6" + Environment.NewLine +
                "Pre-discount charge: $17.94" + Environment.NewLine +
                "Discount percent: 0%" + Environment.NewLine +
                "Discount amount: $0.00" + Environment.NewLine +
                "Final charge: $17.94";

            //Act
            var rentalAgreement1 = new RentalAgreement(toolCode, rentalDays, discountPercent, date);

            //Assert
            Assert.AreEqual(expected, rentalAgreement1.RentalAgreementText);
        }
        [TestMethod]
        public void TestMethod6()
        {
            //Arrange
            string toolCode = "JAKR";
            int rentalDays = 4;
            int discountPercent = 50;
            DateTime date = Convert.ToDateTime("07/02/20");
            var expected = "Tool code: JAKR" + Environment.NewLine +
                "Tool type: Jackhammer" + Environment.NewLine +
                "Brand: Ridgid" + Environment.NewLine +
                "Rental days: 4" + Environment.NewLine +
                "Check out date: 07/02/20" + Environment.NewLine +
                "Due date: 07/06/20" + Environment.NewLine +
                "Daily rental charge: $2.99" + Environment.NewLine +
                "Charge days: 1" + Environment.NewLine +
                "Pre-discount charge: $2.99" + Environment.NewLine +
                "Discount percent: 50%" + Environment.NewLine +
                "Discount amount: $1.50" + Environment.NewLine +
                "Final charge: $1.49";

            //Act
            var rentalAgreement1 = new RentalAgreement(toolCode, rentalDays, discountPercent, date);

            //Assert
            Assert.AreEqual(expected, rentalAgreement1.RentalAgreementText);
        }
        [TestMethod]
        public void TestInvalidRentalDays()
        {
            //Arrange
            string toolCode = "JAKR";
            int rentalDays = -1;
            int discountPercent = 10;
            DateTime date = Convert.ToDateTime("09/03/15");

            //Assert
            Assert.ThrowsException<System.ArgumentException>(() => Program.Checkout(toolCode, rentalDays, discountPercent, date));
        }

        [TestMethod]
        public void TestInvalidToolCode()
        {
            //Arrange
            string toolCode = "ABCD";
            int rentalDays = 10;
            int discountPercent = 10;
            DateTime date = Convert.ToDateTime("09/03/15");

            //Assert
            Assert.ThrowsException<System.ArgumentException>(() => Program.Checkout(toolCode, rentalDays, discountPercent, date));
        }


    }
}
