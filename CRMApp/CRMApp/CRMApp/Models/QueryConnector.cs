using MySqlConnector;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMApp.Models
{
    public class QueryConnector
    {
        private readonly static MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "buhoddo9mvkgu5t91m1o-mysql.services.clever-cloud.com",
            Port = 3306,
            UserID = "ularvefhmggfkqtj",
            Password = "zmMmylXOYuIf8AW3O8hL",
            Database = "buhoddo9mvkgu5t91m1o",
        };

        public static MySqlDataReader Run(string commandText, string str1 = null, string str2 = null, string str3 = null, string str4 = null, string str5 = null, string str6 = null, string str7 = null, string str8 = null, string str9 = null, string str10 = null, string str11 = null, string str12 = null, int int1 = 0, int int2 = 0, int int3 = 0, int int4 = 0, int int5 = 0, int int6 = 0)
        {
            MySqlConnection connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddWithValue("@str1", str1);
            command.Parameters.AddWithValue("@str2", str2);
            command.Parameters.AddWithValue("@str3", str3);
            command.Parameters.AddWithValue("@str4", str4);
            command.Parameters.AddWithValue("@str5", str5);
            command.Parameters.AddWithValue("@str6", str6);
            command.Parameters.AddWithValue("@str7", str7);
            command.Parameters.AddWithValue("@str8", str8);
            command.Parameters.AddWithValue("@str9", str9);
            command.Parameters.AddWithValue("@str10", str10);
            command.Parameters.AddWithValue("@str11", str11);
            command.Parameters.AddWithValue("@str12", str12);
            command.Parameters.AddWithValue("@int1", int1);
            command.Parameters.AddWithValue("@int2", int2);
            command.Parameters.AddWithValue("@int3", int3);
            command.Parameters.AddWithValue("@int4", int4);
            command.Parameters.AddWithValue("@int5", int5);
            command.Parameters.AddWithValue("@int6", int6);

            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }
        
    }
}
