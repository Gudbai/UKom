using MySqlConnector;
using CRMApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CRMApp.Models.Forms
{
    public class FormInfo
    {
        #region Declaration

        private int formID;
        private string debtorCode;
        private string formName;
        private string sourceCode;
        private string billingType;
        private string workRequest;
        private string areaCode;
        private string priorityLevel;
        private string assignTo;
        private string requesterName;
        private string requesterPhone;
        private DateTime formCreation;

        public int FormID
        {
            get { return formID; }
            set { formID = value; }
        }

        public string DebtorCode
        {
            get { return debtorCode; }
            set { debtorCode = value; }
        }

        public string FormName
        {
            get { return formName; }
            set { formName = value; }
        }

        public string SourceCode
        {
            get { return sourceCode; }
            set { sourceCode = value; }
        }

        public string BillingType
        {
            get { return billingType; }
            set { billingType = value; }
        }

        public string WorkRequest
        {
            get { return workRequest; }
            set { workRequest = value; }
        }

        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        public string PriorityLevel
        {
            get { return priorityLevel; }
            set { priorityLevel = value; }
        }

        public string AssignTo
        {
            get { return assignTo; }
            set { assignTo = value; }
        }

        public string RequesterName
        {
            get { return requesterName; }
            set { requesterName = value; }
        }

        public string RequesterPhone
        {
            get { return requesterPhone; }
            set { requesterPhone = value; }
        }

        public DateTime FormCreation
        {
            get { return formCreation; }
            set { formCreation = value; }
        }

        public FormInfo(int formID, string debtorCode, string formName, string sourceCode, string billingType, string workRequest, string areaCode, string priorityLevel, string assignTo, string requesterName, string requesterPhone, DateTime formCreation)
        {
            FormID = formID;
            DebtorCode = debtorCode;
            FormName = formName;
            SourceCode = sourceCode;
            BillingType = billingType;
            WorkRequest = workRequest;
            AreaCode = areaCode;
            PriorityLevel = priorityLevel;
            AssignTo = assignTo;
            RequesterName = requesterName;
            RequesterPhone = requesterPhone;
            FormCreation = formCreation;
        }

        #endregion
    }

    public class FormInfoRepository
    {
        private ObservableCollection<FormInfo> formInfo;
        public ObservableCollection<FormInfo> FormInfoCollection
        {
            get { return formInfo; }
            set { formInfo = value; }
        }

        public FormInfoRepository()
        {
            formInfo = new ObservableCollection<FormInfo>();
            GenerateForms();

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                formInfo.Clear();
                GenerateForms();
            });
        }

        private string GetString(MySqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return string.Empty;
            else
                return reader[column].ToString();
        }

        private void GenerateForms()
        {
            MySqlDataReader reader = QueryConnector.Run("SELECT * FROM tblForm");
            while (reader.Read())
            {
                string debtorCode = GetString(reader, "debtorCode");
                string formName = GetString(reader, "formName");
                string sourceCode = GetString(reader, "sourceCode");
                string billingType = GetString(reader, "billingType");
                string workRequest = GetString(reader, "workRequest");
                string areaCode = GetString(reader, "areaCode");
                string priorityLevel = GetString(reader, "priorityLevel");
                string assignTo = GetString(reader, "assignTo");
                string requesterName = GetString(reader, "requesterName");
                string requesterPhone = GetString(reader, "requesterPhone");

                formInfo.Add(new FormInfo
                (
                    reader.GetInt32("formID"),
                    debtorCode,
                    formName,
                    sourceCode,
                    billingType,
                    workRequest,
                    areaCode,
                    priorityLevel,
                    assignTo,
                    requesterName,
                    requesterPhone,
                    reader.GetDateTime("formCreation")
                ));
            }
        }
    }
}