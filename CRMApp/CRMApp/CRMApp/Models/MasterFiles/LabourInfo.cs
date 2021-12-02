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
    public class LabourInfo
    {
        private int labourID;
        private string labourCode;
        private string labourDescription;

        public int LabourID
        {
            get { return labourID; }
            set { labourID = value; }
        }

        public string LabourCode
        {
            get { return labourCode; }
            set { labourCode = value; }
        }

        public string LabourDescription
        {
            get { return labourDescription; }
            set { labourDescription = value; }
        }

        public LabourInfo(int labourID, string labourCode, string labourDescription)
        {
            LabourID = labourID;
            LabourCode = labourCode;
            LabourDescription = labourDescription;
        }
    }

    public class LabourInfoRepository
    {
        private ObservableCollection<LabourInfo> labourInfo;
        public ObservableCollection<LabourInfo> LabourInfoCollection
        {
            get { return labourInfo; }
            set { labourInfo = value; }
        }

        public LabourInfoRepository()
        {
            labourInfo = new ObservableCollection<LabourInfo>();
            GenerateLabours();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                labourInfo.Clear();
                GenerateLabours();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateLabours()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblLabour");
            while (reader.Read())
            {
                string labourCode = GetString(reader, "labourCode");
                string labourDescription = GetString(reader, "labourDescription");

                labourInfo.Add(new LabourInfo
                (
                    reader.GetInt32("labourID"),
                    labourCode,
                    labourDescription
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
            var item = o as LabourInfo;
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
                            case "Labour Code":
                                if (item.LabourCode.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Labour Description":
                                if (item.LabourDescription.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                        }
                        return false;
                    }
                    else
                    {
                        if (item.LabourCode.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.LabourDescription.ToString().ToLower().Contains(FilterText.ToLower()))
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
