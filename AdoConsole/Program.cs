using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAdoDal;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdoConnect"].ConnectionString;
            var db = new AdoDal();

            /*var users = db.GetAllUsers();    // Get all Users
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}\nName: {user.Name}\nEmail: {user.Email}");
            }*/

            /*var accounts = db.GetAllAccounts();    // Get allAccounts
            foreach (var acc in accounts)
            {
                Console.WriteLine($"Id: {acc.Id}  UserId: {acc.UserId}  AccountNumber: {acc.AccountNumber} Balance: {acc.Balance}");
            }*/

            //var uname = db.LookUpName(12);  Get Name by Id

            //db.KeepingTrack(10, new MyAdoDal.Models.RollCheck { Name = "", Email = "" });

           
            bool throwEx = true;
            Console.Write("Do you want to throw an exception (Y or N): ");
            var userAnswer = Console.ReadLine();
            if (userAnswer?.ToLower() == "n")
            {
                throwEx = false;
            }
            
            // Process customer 1 – enter the id for the customer to move.
            db.ProcessTracking(throwEx, 1000);
            Console.WriteLine("Check RollCheck table for results");

            //Console.ReadLine();
        }
    }
}
