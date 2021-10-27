using System;
using System.Collections.Generic;
using System.Text;

namespace MyAdoDal.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public bool AddBalance(decimal bal)
        {
            try
            {
                decimal newAccountBalance = this.Balance + bal;
                this.Balance = newAccountBalance;
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
