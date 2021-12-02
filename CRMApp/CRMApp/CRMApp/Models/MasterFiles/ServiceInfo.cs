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
    public class ServiceInfo
    {
        private int serviceID;
        private string serviceCode;
        private string serviceDescription;
        private string serviceCost;

        public int ServiceID
        {
            get { return serviceID; }
            set { serviceID = value; }
        }

        public string ServiceCode
        {
            get { return serviceCode; }
            set { serviceCode = value; }
        }

        public string ServiceDescription
        {
            get { return serviceDescription; }
            set { serviceDescription = value; }
        }

        public string ServiceCost
        {
            get { return serviceCost; }
            set { serviceCost = value; }
        }

        public ServiceInfo(int serviceID, string serviceCode, string serviceDescription, string serviceCost)
        {
            ServiceID = serviceID;
            ServiceCode = serviceCode;
            ServiceDescription = serviceDescription;
            ServiceCost = serviceCost;
        }
    }

    public class ServiceInfoRepository
    {
        private ObservableCollection<ServiceInfo> serviceInfo;
        public ObservableCollection<ServiceInfo> ServiceInfoCollection
        {
            get { return serviceInfo; }
            set { serviceInfo = value; }
        }

        public ServiceInfoRepository()
        {
            serviceInfo = new ObservableCollection<ServiceInfo>();
            GenerateServices();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                serviceInfo.Clear();
                GenerateServices();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateServices()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblService");
            while (reader.Read())
            {
                string serviceCode = GetString(reader, "serviceCode");
                string serviceDescription = GetString(reader, "serviceDescription");
                string serviceCost = GetString(reader, "serviceCost");

                serviceInfo.Add(new ServiceInfo
                (
                    reader.GetInt32("serviceID"),
                    serviceCode,
                    serviceDescription,
                    serviceCost
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
            var item = o as ServiceInfo;
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
                            case "Service Code":
                                if (item.ServiceCode.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Service Description":
                                if (item.ServiceDescription.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                            case "Service Cost":
                                if (item.ServiceCost.ToString().ToLower().Contains(FilterText.ToLower()))
                                    return true;
                                break;
                        }
                        return false;
                    }
                    else
                    {
                        if (item.ServiceCode.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.ServiceDescription.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.ServiceCost.ToString().ToLower().Contains(FilterText.ToLower()))
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
