using SAPbobsCOM;
using SAPDIExamples.Extensions;
using System.Collections.Generic;

namespace SAPDIExamples.Handlers
{

    /// <summary>
    /// Employees handler
    /// </summary>
    public class Employees
    {

        /// <summary>
        /// Load first 2 employees
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <param name="prefix">Prefix first name</param>
        /// <returns>Employee list data</returns>
        public List<Dictionary<string, string>> LoadByPrefix(DBConnection db, string prefix)
        {
            db.SQLCommand.CommandText =
                @"SELECT [empID], [firstName], [lastName], [Active]
                  FROM [OHEM]
                  WHERE [firstName] like @prefix
                  ORDER BY [empID]";
            db.SQLCommand.Parameters.Add("@prefix", System.Data.SqlDbType.VarChar, 50).Value = prefix + "%";
            return db.CreateDataTable().ToList();
        }

        /// <summary>
        /// Load single record to load
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <param name="empId">Employee to load</param>
        /// <returns>Attributes for employee loaded</returns>
        public Dictionary<string, string> LoadByEmpId(DBConnection db, int empId)
        {
            db.SQLCommand.CommandText =
                @"SELECT [empID], [firstName], [lastName], [Active]
                  FROM [OHEM]
                  WHERE [empID] = @empId";
            db.SQLCommand.Parameters.Add("@empId", System.Data.SqlDbType.Int).Value = empId;
            return db.CreateDataTable().ToList().First();
        }


        /// <summary>
        /// Create a new business partner
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="active">Employee activation</param>
        /// <returns>Retrieve new object key</returns>
        public int Create(
            SAPConnection sap,
            string firstName,
            string lastName,
            bool active)
        {
            IEmployeesInfo partner = sap.Company.GetBusinessObject(BoObjectTypes.oEmployeesInfo);
            partner.FirstName = firstName;
            partner.LastName = lastName;
            partner.Active = active ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;

            sap.CheckResponse(partner.Add());
            return int.Parse(sap.Company.GetNewObjectKey());
        }

        /// <summary>
        /// Delete SAP record if exist
        /// </summary>
        /// <param name="sap">SAP connection</param>
        /// <param name="key">Key to delete</param>
        /// <returns></returns>
        public bool Delete(SAPConnection sap, int key)
        {
            IEmployeesInfo partner = sap.Company.GetBusinessObject(BoObjectTypes.oEmployeesInfo);
            if (partner.GetByKey(key))
            {
                sap.CheckResponse(partner.Remove());
                return true;
            }
            return false;
        }

    }

}
