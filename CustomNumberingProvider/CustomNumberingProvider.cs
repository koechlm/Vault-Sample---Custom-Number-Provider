using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.DataManagement.Server.Extensibility.Numbering;
using SQL = System.Data.SqlClient;

namespace CustomNumberingProvider
{
    public class CustomNumberingProvider : INumberProvider
    {
        private static string m_initializationParmPrefix = "";

        public IList<NumberProviderResponse> GenerateNumbers(NumberProviderRequest request, int num = 1)
        {
            // We can generate our number responses here:
            List<NumberProviderResponse> responses = new List<NumberProviderResponse>();

            //ADSK Technical Sales EMEA Sample Numbering; requires SQL DB "MyNumberingDB" and table, to be created via SQL script:
            //use[MyNumberingDB]
            //Create table autonumber(id int identity, number as (right('000000' + convert([varchar], [id], 0), (6))), value varchar(10))

            SQL.SqlConnection mSqlConn = new SQL.SqlConnection();
            mSqlConn.ConnectionString =
            "Data Source=localhost\\AUTODESKVAULT;" +
            "Initial Catalog=MyNumberingDB;" +
            "User ID=sa;" +
            "Password=AutodeskVault@26200";
            mSqlConn.Open();

            int newID;
            var cmd = "INSERT MyNumberingDB.dbo.autonumber (value) values (@var);SELECT CAST(scope_identity() AS int)";
            SQL.SqlCommand insertCommand = new SQL.SqlCommand(cmd, mSqlConn);
            insertCommand.Parameters.AddWithValue("@var", "temp");
            //con.Open();
            newID = (int)insertCommand.ExecuteScalar();
            //update the value according the new Number + Prefix

            cmd = "SELECT number from[MyNumberingDB].[dbo].[autonumber] WHERE id = " + newID;
            SQL.SqlCommand mSelectCmd = new SQL.SqlCommand(cmd, mSqlConn);
            string newNumber = (string)mSelectCmd.ExecuteScalar();

            mSqlConn.Close();

            // If we can't generate a number, we can return a general failure code:
            if (num <= 0 || newNumber.Length < 6) // original: || !this.IsRequestSupported(request)
            {
                responses.Add(new NumberProviderResponse(ErrorCodes.Failure, ""));
                return responses;
            }

            while (num-- > 0)
            {
                // This is where we create the response with our newly generated number
                //                                                           ---------Below is the newly generated number----------
                //responses.Add(new NumberProviderResponse(ErrorCodes.Success, m_initializationParmPrefix + Guid.NewGuid().ToString()));
                responses.Add(new NumberProviderResponse(ErrorCodes.Success, request.Fields[0].Value + request.Fields[1].Value + newNumber));
            }

            return responses;
        }

        public string GetDisplayName()
        {
            // We can set what name the client sees here:
            return "Custom Numbering Provider";
        }

        public void Init(object[] parameters)
        {
            // If our server-side web.config defines an 
            // initializationParm node, we can cast and
            // assign it here:
            if (parameters.Count() == 0)
                return;

            m_initializationParmPrefix = parameters.First().ToString();
        }

        // Note: IsRequestSupported is not called from the server, 
        // it is internal only as of this time
        public bool IsRequestSupported(NumberProviderRequest request)
        {
            // We can restrict what kind of requests are supported by our number provider here:
            bool HasRestrictedField = request.Fields.Any(field => field.Type == FieldType.PredefinedList);

            return !HasRestrictedField;
        }
    }
}
