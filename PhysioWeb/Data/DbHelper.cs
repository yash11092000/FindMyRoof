using System.Data;
using System.Net;
using System.Net.NetworkInformation;
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
            cmd.Parameters.AddWithValue("DeviceId", GetMacAddress());
            cmd.Parameters.AddWithValue("IPAddress", GetIPAddress());
            cmd.Parameters.AddWithValue("DeviceName", GetDeviceName());
        }
        public static string GetIPAddress()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string html = client.DownloadString("http://checkip.dyndns.org/");
                    int first = html.IndexOf("Address: ") + 9;
                    int last = html.LastIndexOf("</body>");
                    return html.Substring(first, last - first);
                }
            }
            catch { return "0.0.0.0"; }
        }

        public static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed && !string.IsNullOrEmpty(tempMac) && tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }
            return macAddress;
        }
        public static string GetDeviceName()
        {
            return $"{Environment.UserName}[{Environment.MachineName}]";
        }
    }
}
