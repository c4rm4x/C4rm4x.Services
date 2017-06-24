#region Using

using System;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace C4rm4x.Services.Persistence.ADO
{
    internal class InternalSqlCommand : IDisposable
    {
        public SqlCommand InnerCommand { get; private set; }

        public InternalSqlCommand(
            string function,
            InternalSqlConnection connection,
            SqlTransaction transaction = null)
        {
            InnerCommand = new SqlCommand(function, connection.InnerConnection);

            if (transaction != null) InnerCommand.Transaction = transaction;
        }

        public void Dispose()
        {
            InnerCommand.Parameters.Clear();
            InnerCommand.Dispose();
        }

        public int ExecuteNonQuery(
            params SqlParameter[] parameters)
        {
            InnerCommand.Parameters.AddRange(parameters);

            InnerCommand.CommandType = CommandType.StoredProcedure;
            return InnerCommand.ExecuteNonQuery();
        }
    }
}
