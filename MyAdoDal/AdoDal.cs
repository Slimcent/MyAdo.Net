using System;
using System.Data;
using System.Text;
using MyAdoDal.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MyAdoDal
{
    public class AdoDal
    {
        private readonly string _connectionString;

        public AdoDal() :
            this(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BEZAOPay;Integrated Security=True")
        {

        }

        public AdoDal(string connectionString)
        {
            _connectionString = connectionString;
        }


        private SqlConnection _sqlConnection = null;
        private void OpenConnection()
        {
            _sqlConnection = new SqlConnection { ConnectionString = _connectionString };
            _sqlConnection.Open();
        }

        private void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
                _sqlConnection?.Close();
        }


        public IEnumerable<User> GetAllUsers()  // Get all users
        {
            OpenConnection();

            var users = new List<User>();

            var query = @"SELECT * FROM USERS";

            using (var command = new SqlCommand(query, _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return users;
        }

        public IEnumerable<Account> GetAllAccounts()  // Get all Accounts
        {
            OpenConnection();

            var accounts = new List<Account>();

            var query = "SELECT * FROM Accounts";

            using (var command = new SqlCommand(query, _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            AccountNumber = (int)reader["Account_Number"],
                            Balance = (decimal)reader["Balance"]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return accounts;
        }

        public IEnumerable<Transaction> GetAllTransactions()  // Get all Transactions
        {
            OpenConnection();

            var transacts = new List<Transaction>();

            var query = "SELECT * FROM Transactions";

            using (var command = new SqlCommand(query, _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                       transacts.Add(new Transaction
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            Mode = (string)reader["Mode"],
                            Amount = (decimal)reader["Amount"],
                            Time = (DateTime)reader["Time"]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return transacts;
        }

        public IEnumerable<RollCheck> GetAllRollCheck()  // Get all Roll Check
        {
            OpenConnection();

            var roll = new List<RollCheck>();

            var query = @"SELECT * FROM RollCcheck";

            using (var command = new SqlCommand(query, _sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        roll.Add(new RollCheck
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            return roll;
        }


        public void InsertNewUser(User user)  // Insert new user
        {
            try
            {
                OpenConnection();
                string query = "Insert Into Users (Name, Email) Values " +
                $"('{user.Name}', '{user.Email}')";
                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteUser(int id) // Delete a user
        {
            try
            {
                OpenConnection();
                string sql = $"Delete from Users where Id = '{id}'";
                using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        int deletedUser = command.ExecuteNonQuery();
                        if (deletedUser > 0)
                        {
                            Console.WriteLine($"User deleted successully");
                        }
                        else
                        {
                            Console.WriteLine("Delete failed");

                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            CloseConnection();
        }

        public void UpdateUser(User user)  // Update users table
        {
            OpenConnection();
            string sql = $"Update Users Set Name = '{user.Name}' Where Id = '{user.Id}'";
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                try
                {
                    int comm = command.ExecuteNonQuery();
                    if (comm > 0)
                    {
                        Console.WriteLine("Update Successful");
                        Console.WriteLine($"Name updated to {user.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Update Failed! Id not found");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            CloseConnection();
        }


        public void InsertUser(User user)  // Insert a new user using Stored Procedure
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("InsertIntoUsers", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = user.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@email",
                        Value = user.Email,
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void InsertAccount(Account account) // Insert into Accounts table using Stored Procedure
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("InsertIntoAccounts", _sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@userID",
                        Value = account.UserId,
                        SqlDbType = SqlDbType.Int,
                        Size = 50,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@accountNumber",
                        Value = account.AccountNumber,
                        SqlDbType = SqlDbType.Int,
                        Size = 50,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "@balance",
                        Value = account.Balance,
                        SqlDbType = SqlDbType.Decimal,
                        Size = 50,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


        public string LookUpName(int id)
        {
            OpenConnection();
            string userName = null;

            using (SqlCommand command = new SqlCommand("GetName", _sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                // Input param.
                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Value = id,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(param);

                // Output param.
                param = new SqlParameter
                {
                    ParameterName = "@Name",
                    SqlDbType = SqlDbType.Char,
                    Size = 50,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(param);
                try
                {
                    command.ExecuteNonQuery();
                    userName = (string)command.Parameters["@Name"].Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
            return userName;

        }



        public void SendMoney(Account account, Transaction transact) // Sending money using SqlTrnsactions
        {
            OpenConnection();
            string sql = $"Update Accounts Set Balance = '{account.Balance}' Where UserId = '{account.UserId}'";
            string query = "Insert Into Transactions (UserId, Mode, Amount, Time) Values " +
                $"('{transact.UserId}', '{transact.Mode}', '{transact.Amount}', '{transact.Time}')";

            var cmdSql = new SqlCommand(sql, _sqlConnection);
            var cmdQuery = new SqlCommand(query, _sqlConnection);

            SqlTransaction tx = null;

            try
            {
                tx = _sqlConnection.BeginTransaction();
                cmdSql.Transaction = tx;
                cmdQuery.Transaction = tx;

                cmdSql.ExecuteNonQuery();
                cmdQuery.ExecuteNonQuery();

                tx.Commit();
                Console.WriteLine("Transaction was successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tx?.Rollback();
            }
            finally
            {
                CloseConnection();
            }
        }

        public void KeepingTrack(int id, RollCheck roll) // Deleting a User and then, adding the user to the RollCheck Table using SqlTrnsactions
        {
            OpenConnection();
            var query1 = $"Delete from Users where Id = '{id}'";
            var query2 = "Insert Into RollCheck ( Name, Email) Values " +
                $"('{roll.Name}', '{roll.Email}')";

            var cmdSql = new SqlCommand(query1, _sqlConnection);
            var cmdQuery = new SqlCommand(query2, _sqlConnection);

            SqlTransaction tx = null;

            try
            {
                tx = _sqlConnection.BeginTransaction();
                cmdSql.Transaction = tx;
                cmdQuery.Transaction = tx;

                cmdSql.ExecuteNonQuery();
                cmdQuery.ExecuteNonQuery();

                tx.Commit();
                Console.WriteLine("Transaction was successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tx?.Rollback();
            }
            finally
            {
                CloseConnection();
            }
        }


        public void ProcessTracking(bool throwEx, int id)
        {
            OpenConnection();
            // First, look up current name based on customer ID.
            string name;
            string email;
            var cmdSelect = new SqlCommand($"Select * from Users where Id = {id}",
            _sqlConnection);
            using (var dataReader = cmdSelect.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    name = (string)dataReader["Name"];
                    email = (string)dataReader["Email"];
                }
                else
                {
                    CloseConnection();
                    return;
                }
            }

            // Create command objects that represent each step of the operation.
            var cmdRemove = new SqlCommand($"Delete from Users where Id = {id}", _sqlConnection);
            var cmdInsert = new SqlCommand("Insert Into RollCheck" +
                            $"(Name, Email) Values('{name}', '{email}')",_sqlConnection);
            // We will get this from the connection object.

            SqlTransaction tx = null;

            try
            {
                tx = _sqlConnection.BeginTransaction();
                // Enlist the commands into this transaction.
                cmdInsert.Transaction = tx;
                cmdRemove.Transaction = tx;

                // Execute the commands.
                cmdInsert.ExecuteNonQuery();
                cmdRemove.ExecuteNonQuery();
                // Simulate error.
                if (throwEx)
                {
                    throw new Exception("Sorry! Database error! Tx failed...");
                }

                // Commit it!
                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Any error will roll back transaction. Using the new conditional access operator to
               // check for null.
                tx?.Rollback();
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
