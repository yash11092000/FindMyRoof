using System.Data;
using Microsoft.Data.SqlClient;

namespace PhysioWeb.Data
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // SELECT – return DataSet (TVP/multiple result set support)
        public async Task<DataSet> ExecuteDataSetAsync(string storedProc, string[]? paramNames = null, object[]? paramValues = null, SqlDbType[]? paramTypes = null, string[]? tvpTypeNames = null)
        {
            var dataSet = new DataSet();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProc, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddParameters(cmd, paramNames, paramValues, paramTypes, tvpTypeNames);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    await conn.OpenAsync();
                    adapter.Fill(dataSet);
                }
            }

            return dataSet;
        }


        // SELECT – return Reader (TVP/multiple result set support)
        public async Task<IDataReader> GetDataReaderAsync(string storedProc, string[]? paramNames = null, object[]? paramValues = null, SqlDbType[]? paramTypes = null, string[]? tvpTypeNames = null)
        {
            var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(storedProc, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            AddParameters(cmd, paramNames, paramValues, paramTypes, tvpTypeNames);

            await conn.OpenAsync();
            return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }


        // INSERT/UPDATE/DELETE – return affected rows
        public async Task<int> ExecuteNonQueryAsync(string storedProc, string[]? paramNames = null, object[]? paramValues = null, SqlDbType[]? paramTypes = null, string[]? tvpTypeNames = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProc, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddParameters(cmd, paramNames, paramValues, paramTypes, tvpTypeNames);

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        // SCALAR – return a single value (e.g., int, string, DateTime)
        public async Task<object?> ExecuteScalarAsync(string storedProc, string[]? paramNames = null, object[]? paramValues = null, SqlDbType[]? paramTypes = null, string[]? tvpTypeNames = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(storedProc, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddParameters(cmd, paramNames, paramValues, paramTypes, tvpTypeNames);

                await conn.OpenAsync();
                return await cmd.ExecuteScalarAsync();
            }
        }

        private void AddParameters(SqlCommand cmd, string[]? names, object[]? values, SqlDbType[]? types, string[]? tvpTypeNames)
        {
            if (names == null || values == null) return;

            for (int i = 0; i < names.Length; i++)
            {
                var param = new SqlParameter
                {
                    ParameterName = names[i],
                    SqlDbType = types != null && i < types.Length ? types[i] : SqlDbType.NVarChar,
                    Value = values[i] ?? DBNull.Value
                };

                // TVP support
                if (param.Value is DataTable)
                {
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = tvpTypeNames?[i] ?? throw new ArgumentException("TVP type name is missing for structured param.");
                }

                cmd.Parameters.Add(param);
            }
        }
    }
}
