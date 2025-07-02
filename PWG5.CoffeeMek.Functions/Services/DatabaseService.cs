namespace PWG5.CoffeeMek.Functions.Services;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;

public class DatabaseService
{
    private readonly string _connString;
    public DatabaseService(string connString)
    {
        _connString = connString;
    }

    public async Task<IEnumerable<T>> GetIEnumerableAsync<T>(string name, CancellationToken cancellationToken= default)
    {
        using var sqlConn = await GetConnectionAsync(cancellationToken);
        var values = await sqlConn.QueryAsync<T>(
            "coffee_mek."+name,
            commandType: CommandType.StoredProcedure);
        return values.ToList();
    }

    public async Task<T> GetScalarAsync<T>(string name, object? parameters = null, CancellationToken cancellationToken=default)
    {
        using var sqlConn = await GetConnectionAsync(cancellationToken);
        var value = await sqlConn.ExecuteScalarAsync<T>(
            "coffee_mek."+name,
            parameters,
            commandType: CommandType.StoredProcedure);
        return value;
    }

    public async Task ExecuteAsync(string name, object? parameters = null, CancellationToken cancellationToken = default)
    {
        using var sqlConn = await GetConnectionAsync(cancellationToken);
        await sqlConn.ExecuteAsync(
            "coffee_mek."+name,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    private async Task<SqlConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new SqlConnection(_connString);
        await connection.OpenAsync();
        return connection;
    }
}
