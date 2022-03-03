using MTCG_SWEN1.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.DB.DAL
{
    class UserDAL
    {
        private readonly IDbConnection _db;

        public UserDAL()
        {
            _db = DataBaseConnection.Connect();
            try
            {
                _db.Open();
                if (_db.State != ConnectionState.Open)
                    throw new Exception("DB could not be opened.");
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }            
        }

        public void InsertUser(Credentials credentials)
        {
            string insert = "INSERT INTO users (u_username, u_password) VALUES (@u_username, @u_password)";
            IDbCommand commandGeneral = _db.CreateCommand();
            commandGeneral.CommandText = insert;

            NpgsqlCommand commandInsert = commandGeneral as NpgsqlCommand;
            commandInsert.Parameters.Add("@u_id", NpgsqlDbType.Char, 36);
            commandInsert.Parameters.Add("u_username", NpgsqlDbType.Varchar, 20);
            commandInsert.Parameters.Add("u_password", NpgsqlDbType.Varchar, 36);
            commandInsert.Prepare();

            commandInsert.Parameters["u_id"].Value = Guid.NewGuid();
            commandInsert.Parameters["u_username"].Value = credentials.Username;
            commandInsert.Parameters["u_password"].Value = credentials.Password;

            commandGeneral.ExecuteNonQuery();

        }

        public User ReadUserName(Credentials credentials)
        {
            string read = "SELECT u_id, u_username, u_password, u_coins, u_deck, u_elo FROM users WHERE u_username = @UserName";

            IDbCommand commandGeneral = _db.CreateCommand();
            commandGeneral.CommandText = read;

            var userName = commandGeneral.CreateParameter();
            userName.ParameterName = "UserName";
            userName.DbType = DbType.String;
            userName.Value = credentials.Username;
            commandGeneral.Parameters.Add(userName);

            var commandReader = commandGeneral.ExecuteReader();
            if (commandReader.Read())
            {
                var id = Guid.NewGuid();
                User user = new();
                user.ID = id;
                user.UserName = commandReader.GetString(1);
                user.Password = commandReader.GetString(2);
                user.Coins = commandReader.GetInt32(3);
                user.ELO = commandReader.GetInt32(5);

                commandReader.Close();
                return user;
            }
            else
            {
                commandReader.Close();
                throw new Exception("User not found");
            }
        }

        public string CreateToken(Guid id)
        {
            string insert = "INSERT INTO sessions (s_token, s_user, s_timestamp) VALUES (@s_token, @s_user, @s_timestamp)";
            IDbCommand commandGeneral = _db.CreateCommand();
            commandGeneral.CommandText = insert;

            NpgsqlCommand commandInsert = commandGeneral as NpgsqlCommand;
            commandInsert.Parameters.Add("s_token", NpgsqlDbType.Varchar, 36);
            commandInsert.Parameters.Add("s_user", NpgsqlDbType.Integer);
            commandInsert.Parameters.Add("s_timestamp", NpgsqlDbType.Timestamp, 50);

            commandInsert.Prepare();
            string token = new string(Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Take(36).ToArray());
            commandInsert.Parameters["s_token"].Value = token;
            commandInsert.Parameters["s_user"].Value = id;
            commandInsert.Parameters["s_timestamp"].Value = DateTime.Now;

            commandGeneral.ExecuteNonQuery();
            return token;
        }

        public bool UserIsLoggedIn(string token)
        {
            string select = "SELECT s_user FROM sessions WHERE s_token = @Token";
            IDbCommand commandGeneral = _db.CreateCommand();
            commandGeneral.CommandText = select;

            var userToken = commandGeneral.CreateParameter();
            userToken.ParameterName = "Token";
            userToken.DbType = DbType.String;
            userToken.Value = token;
            commandGeneral.Parameters.Add(userToken);

            var commandReader = commandGeneral.ExecuteReader();
            if(commandReader.Read())
            {
                commandReader.Close();
                return true;
            }
            else
            {
                commandReader.Close();
                return false;
            }
        }


    }
}
