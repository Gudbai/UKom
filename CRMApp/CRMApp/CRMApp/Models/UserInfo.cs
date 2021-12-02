using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Data;
using Xamarin.Forms;

namespace CRMApp.Models
{
    public class UserInfo
    {
        private int userID;
        private string userName;
        private string userEmail;
        private string userPassword;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public UserInfo(int userID, string userName, string userEmail, string userPassword)
        {
            UserID = userID;
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
    }

    public class UserInfoRepository
    {
        private static ObservableCollection<UserInfo> userInfo;
        public ObservableCollection<UserInfo> UserInfoCollection
        {
            get { return userInfo; }
            set { userInfo = value; }
        }

        public UserInfoRepository()
        {
            userInfo = new ObservableCollection<UserInfo>();
        }

        public static ObservableCollection<UserInfo> ResetPassword(string email)
        {
            ObservableCollection<UserInfo> data = new ObservableCollection<UserInfo>();

            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblUser WHERE userEmail = @str1;", str1: email);
            while (reader.Read())
            {
                string userName = GetString(reader, "userName");
                string userEmail = GetString(reader, "userEmail");
                string userPassword = GetString(reader, "userPassword");

                data.Add(new UserInfo
                (
                    reader.GetInt32("userID"),
                    userName,
                    userEmail,
                    userPassword
                ));
            }

            return data;
        }

        public static ObservableCollection<UserInfo> LoginCheck(string username, string password)
        {
            ObservableCollection<UserInfo> data = new ObservableCollection<UserInfo>();

            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblUser WHERE (userName = @str1 OR userEmail = @str1) AND userPassword = @str2;", str1: username, str2: password);
            while (reader.Read())
            {
                string userName = GetString(reader, "userName");
                string userEmail = GetString(reader, "userEmail");
                string userPassword = GetString(reader, "userPassword");

                data.Add(new UserInfo
                (
                    reader.GetInt32("userID"),
                    userName,
                    userEmail,
                    userPassword
                ));
            }

            return data;
        }

        private static string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        //private void GenerateUsers()
        //{
        //    MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblUser");
        //    while (reader.Read())
        //    {
        //        string userName = GetString(reader, "userName");
        //        string userEmail = GetString(reader, "userEmail");
        //        string userPassword = GetString(reader, "userPassword");

        //        userInfo.Add(new UserInfo
        //        (
        //            reader.GetInt32("userID"),
        //            userName,
        //            userEmail,
        //            userPassword
        //        ));
        //    }
        //}
    }
}
