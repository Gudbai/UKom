using MySqlConnector;
using CRMApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CRMApp.Models.MasterFiles
{
    public class StatusInfo
    {
        private int statusID;
        private string statusCode;
        private string statusDescription;

        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        public string StatusDescription
        {
            get { return statusDescription; }
            set { statusDescription = value; }
        }

        public StatusInfo(int statusID, string statusCode, string statusDescription)
        {
            StatusID = statusID;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }
    }

    public class StatusInfoRepository
    {
        private ObservableCollection<StatusInfo> statusInfo;
        public ObservableCollection<StatusInfo> StatusInfoCollection
        {
            get { return statusInfo; }
            set { statusInfo = value; }
        }

        public StatusInfoRepository()
        {
            statusInfo = new ObservableCollection<StatusInfo>();
            GenerateStatuss();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                statusInfo.Clear();
                GenerateStatuss();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateStatuss()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblStatus");
            while (reader.Read())
            {
                string statusCode = GetString(reader, "statusCode");
                string statusDescription = GetString(reader, "statusDescription");

                statusInfo.Add(new StatusInfo
                (
                    reader.GetInt32("statusID"),
                    statusCode,
                    statusDescription
                ));
            }
        }

        #region Filtering

        #region Fields
        private string filterText = "";
        private string selectedColumn = "All Columns";
        internal delegate void FilterChanged();
        internal FilterChanged filterTextChanged;
        #endregion

        #region Property
        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                filterTextChanged();
            }
        }

        public string SelectedColumn
        {
            get { return selectedColumn; }
            set { selectedColumn = value; }
        }
        #endregion

        #region Methods
        public bool FilterRecords(object o)
        {
            var item = o as StatusInfo;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (!SelectedColumn.Equals("All Columns"))
                    {
                        switch (SelectedColumn)
                        {
                            case "Status Code":
                                if (item.StatusCode.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Status Description":
                                if (item.StatusDescription.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                        }
                        return false;
                    }
                    else
                    {
                        if (item.StatusCode.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.StatusDescription.ToString().ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                }
            }
            return false;
        }
        #endregion

        #endregion
    }
}
