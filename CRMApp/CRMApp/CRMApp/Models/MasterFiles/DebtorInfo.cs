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
    public class DebtorInfo
    {
        private int debtorID;
        private string debtorCode;
        private string debtorName;
        private string debtorEmail;
        private string debtorAddress;
        private string debtorPhone;

        public int DebtorID
        {
            get { return debtorID; }
            set { debtorID = value; }
        }

        public string DebtorCode
        {
            get { return debtorCode; }
            set { debtorCode = value; }
        }

        public string DebtorName
        {
            get { return debtorName; }
            set { debtorName = value; }
        }

        public string DebtorEmail
        {
            get { return debtorEmail; }
            set { debtorEmail = value; }
        }

        public string DebtorAddress
        {
            get { return debtorAddress; }
            set { debtorAddress = value; }
        }

        public string DebtorPhone
        {
            get { return debtorPhone; }
            set { debtorPhone = value; }
        }

        public DebtorInfo(int debtorID, string debtorCode, string debtorName, string debtorEmail, string debtorAddress, string debtorPhone)
        {
            DebtorID = debtorID;
            DebtorCode = debtorCode;
            DebtorName = debtorName;
            DebtorEmail = debtorEmail;
            DebtorAddress = debtorAddress;
            DebtorPhone = debtorPhone;
        }
    }

    public class DebtorInfoRepository
    {
        private ObservableCollection<DebtorInfo> debtorInfo;
        public ObservableCollection<DebtorInfo> DebtorInfoCollection
        {
            get { return debtorInfo; }
            set { debtorInfo = value; }
        }

        public DebtorInfoRepository()
        {
            debtorInfo = new ObservableCollection<DebtorInfo>();
            GenerateDebtors();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                debtorInfo.Clear();
                GenerateDebtors();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateDebtors()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblDebtor;");
            while (reader.Read())
            {
                string debtorCode = GetString(reader, "debtorCode");
                string debtorName = GetString(reader, "debtorName");
                string debtorEmail = GetString(reader, "debtorEmail");
                string debtorAddress = GetString(reader, "debtorAddress");
                string debtorPhone = GetString(reader, "debtorPhone");

                debtorInfo.Add(new DebtorInfo
                (
                    reader.GetInt32("debtorID"),
                    debtorCode,
                    debtorName,
                    debtorEmail,
                    debtorAddress,
                    debtorPhone
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
            var item = o as DebtorInfo;
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
                            case "Debtor Code":
                                if (item.DebtorCode.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Debtor Name":
                                if (item.DebtorName.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Debtor Email":
                                if (item.DebtorEmail.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Debtor Address":
                                if (item.DebtorAddress.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Debtor Phone":
                                if (item.DebtorPhone.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                        }
                        return false;
                    }
                    else
                    {
                        if (item.DebtorCode.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DebtorName.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DebtorEmail.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DebtorAddress.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DebtorPhone.ToString().ToLower().Contains(FilterText.ToLower()))
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
