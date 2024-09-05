using System.Data;
using System.Data.SqlClient;

public class DatabaseContext
{
    private readonly string _connectionString;

    public DatabaseContext()
    {
        _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_ALURA", EnvironmentVariableTarget.Machine)!;

    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
