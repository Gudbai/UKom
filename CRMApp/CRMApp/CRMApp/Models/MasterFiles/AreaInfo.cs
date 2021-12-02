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
    public class AreaInfo
    {
        private int areaID;
        private string areaCode;
        private string areaDescription;

        public int AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        public string AreaDescription
        {
            get { return areaDescription; }
            set { areaDescription = value; }
        }

        public AreaInfo(int areaID, string areaCode, string areaDescription)
        {
            AreaID = areaID;
            AreaCode = areaCode;
            AreaDescription = areaDescription;
        }
    }

    public class AreaInfoRepository
    {
        private ObservableCollection<AreaInfo> areaInfo;
        public ObservableCollection<AreaInfo> AreaInfoCollection
        {
            get { return areaInfo; }
            set { areaInfo = value; }
        }

        public AreaInfoRepository()
        {
            areaInfo = new ObservableCollection<AreaInfo>();
            GenerateAreas();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                areaInfo.Clear();
                GenerateAreas();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateAreas()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblArea");
            while (reader.Read())
            {
                string areaCode = GetString(reader, "areaCode");
                string areaDescription = GetString(reader, "areaDescription");

                areaInfo.Add(new AreaInfo
                (
                    reader.GetInt32("areaID"),
                    areaCode,
                    areaDescription
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
            var item = o as AreaInfo;
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
                            case "Area Code":
                                if (item.AreaCode.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Area Description":
                                if (item.AreaDescription.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                        }
                        return false;
                    }
                    else
                    {
                        if (item.AreaCode.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.AreaDescription.ToString().ToLower().Contains(FilterText.ToLower()))
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
