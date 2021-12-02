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
    public class SourceInfo
    {
        private int sourceID;
        private string sourceCode;
        private string sourceDescription;

        public int SourceID
        {
            get { return sourceID; }
            set { sourceID = value; }
        }

        public string SourceCode
        {
            get { return sourceCode; }
            set { sourceCode = value; }
        }

        public string SourceDescription
        {
            get { return sourceDescription; }
            set { sourceDescription = value; }
        }

        public SourceInfo(int sourceID, string sourceCode, string sourceDescription)
        {
            SourceID = sourceID;
            SourceCode = sourceCode;
            SourceDescription = sourceDescription;
        }
    }

    public class SourceInfoRepository
    {
        private ObservableCollection<SourceInfo> sourceInfo;
        public ObservableCollection<SourceInfo> SourceInfoCollection
        {
            get { return sourceInfo; }
            set { sourceInfo = value; }
        }

        public SourceInfoRepository()
        {
            sourceInfo = new ObservableCollection<SourceInfo>();
            GenerateSources();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                sourceInfo.Clear();
                GenerateSources();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateSources()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblSource");
            while (reader.Read())
            {
                string sourceCode = GetString(reader, "sourceCode");
                string sourceDescription = GetString(reader, "sourceDescription");

                sourceInfo.Add(new SourceInfo
                (
                    reader.GetInt32("sourceID"),
                    sourceCode,
                    sourceDescription
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
            var item = o as SourceInfo;
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
                            case "Source Code":
                                if (item.SourceCode.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Source Description":
                                if (item.SourceDescription.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                        }
                        return false;
                    }
                    else
                    {
                        if (item.SourceCode.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.SourceDescription.ToString().ToLower().Contains(FilterText.ToLower()))
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
