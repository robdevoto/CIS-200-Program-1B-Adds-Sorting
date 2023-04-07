// Program 1B
// CIS 200-75
// Fall 2022
// Due: 10/12/2022
// By: 1001001

// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy.
// It creates several different Parcels and prints them.
// This program also explores the use of LINQ to produce simple reports.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Prog1
{
    class TestParcels
    {
        // Precondition:  None
        // Postcondition: Parcels have been created and displayed
        static void Main(string[] args)
        {
            // Test Data - Magic Numbers OK
            Address a1 = new Address("  John Smith  ", "   123 Any St.   ", "  Apt. 45 ",
                "  Louisville   ", "  KY   ", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.",
                "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321",
                "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7",
                "Portland", "ME", 14101); // Test Address 4
            Address a5 = new Address("  Ben Hogan  ", "   9123 Charles St.   ",
                "  Duluth   ", "  MN   ", 47130); // Test Address 5
            Address a6 = new Address("Michael Wright", "7987 Rambo St.",
                "Denver", "CO", 90212); // Test Address 6
            Address a7 = new Address("Dale Hanes", "1654 Curry Way", "Suite 198",
                "Las Vegas", "NV", 20810); // Test Address 7
            Address a8 = new Address("Terry Jenkins", "6778 Reno Place", "Apt. 17",
                "Seattle", "WAE", 37210); // Test Address 8


            Letter letter1 = new Letter(a1, a2, 3.95M);                            // Letter test object
            Letter letter2 = new Letter(a6, a4, 4.25M);                            // Letter test object
            GroundPackage gp1 = new GroundPackage(a3, a4, 14, 10, 5, 12.5);        // Ground test object
            GroundPackage gp2 = new GroundPackage(a7, a8, 12, 20, 15, 2.5);        // Ground test object
            NextDayAirPackage ndap1 = new NextDayAirPackage(a1, a3, 25, 15, 15,    // Next Day test object
                85, 7.50M);
            NextDayAirPackage ndap2 = new NextDayAirPackage(a5, a6, 35, 35, 20,    // Next Day test object
                5.25, 5.25M);
            NextDayAirPackage ndap3 = new NextDayAirPackage(a8, a5, 25, 15, 10,    // Next Day test object
                15.5, 5.00M);
            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a4, a1, 46.5, 39.5, 28.0, // Two Day test object
                80.5, TwoDayAirPackage.Delivery.Saver);
            TwoDayAirPackage tdap2 = new TwoDayAirPackage(a6, a2, 36.5, 19.5, 60.0, // Two Day test object
                90.5, TwoDayAirPackage.Delivery.Early);
            TwoDayAirPackage tdap3 = new TwoDayAirPackage(a2, a7, 16.5, 9.5, 38.0, // Two Day test object
                40.5, TwoDayAirPackage.Delivery.Saver);

            List<Parcel> parcels;      // List of test parcels

            parcels = new List<Parcel>();

            parcels.Add(letter1); // Populate list
            parcels.Add(letter2);
            parcels.Add(gp1);
            parcels.Add(gp2);
            parcels.Add(ndap1);
            parcels.Add(ndap2);
            parcels.Add(ndap3);
            parcels.Add(tdap1);
            parcels.Add(tdap2);
            parcels.Add(tdap3);

            WriteLine("Original List:");
            WriteLine("====================");
            foreach (Parcel p in parcels)
            {
                WriteLine(p);
                WriteLine("====================");
            }
            Pause();


            // Order parcels by descending destination zip code
            var sortedDestZip =
                from p in parcels
                orderby p.DestinationAddress.Zip descending
                select p;

            // Print report
            WriteLine("Parcels sorted by descending destination address zip code:");
            WriteLine("----------------------------------------------------------");
            foreach (Parcel p in sortedDestZip)
            {
                WriteLine($"{p.DestinationAddress.Zip:D5}");
            }
            

            Pause();


            // Order parcels by cost
            var sortedCost =
                from p in parcels
                orderby p.CalcCost()
                select p;

            // Print report
            WriteLine("Parcels sorted by ascending cost:");
            WriteLine("---------------------------------");
            foreach (Parcel p in sortedCost)
            {
                WriteLine($"{p.CalcCost(),10:C}");
            }


            Pause();


            // Order parcels first by  parcel type then by descending cost
            var sortedTypeCost =
                from p in parcels
                orderby p.GetType().ToString(), p.CalcCost() descending
                select p;

            // Print Report
            WriteLine("Parcels sorted first by parcel type, then by descending cost:");
            WriteLine("-------------------------------------------------------------");
            foreach (Parcel p in sortedTypeCost)
            {
                WriteLine($"{p.GetType().ToString(),-20} {p.CalcCost(),10:C}");
            }


            Pause();


            // Select all Air Package objects that are heavy then order by descending weight
            var heavyAirSortedWeight =
                from p in parcels
                where (p is AirPackage) && ((AirPackage)p).IsHeavy()
                orderby ((AirPackage)p).Weight descending
                select p;

            // Print Report
            WriteLine("Heavy Air Packages sorted by descending weight:");
            WriteLine("-----------------------------------------------");
            foreach (AirPackage p in heavyAirSortedWeight)
            {
                WriteLine($"{p.GetType().ToString(),-20}{p.Weight,5}");
            }
        }


        // Precondition:  None
        // Postcondition: Pauses program execution until user presses Enter and
        //                then clears the screen
        public static void Pause()
        {
            WriteLine("Press Enter to Continue...");
            ReadLine();

            Console.Clear(); // Clear screen
        }
    }
}
