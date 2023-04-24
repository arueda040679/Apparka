using System.Data;
using System.Data.Common;
using System.Text;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Apparka.Repository;

public class DbCommonUtil : IDisposable{


    private readonly string SQL_CONNECTION_STRING;

    private readonly ILogger<DbCommonUtil> logger;
    private readonly IConfiguration configuration;
    private readonly  SqlConnection connection;

    public DbCommonUtil(ILogger<DbCommonUtil> logger,IConfiguration configuration)
    {
        this.logger = logger;
        this.configuration = configuration;
        this.SQL_CONNECTION_STRING=configuration.GetConnectionString("DefaultConnection");
        this.connection=new SqlConnection(this.SQL_CONNECTION_STRING);
    }


    public async Task<SqlConnection> OpenAsync()   
    {  
        logger.LogDebug("OpenAsync:try open connection");
        
        if (connection.State != System.Data.ConnectionState.Open)   
            await connection.OpenAsync();  

        logger.LogDebug("OpenAsync:the connection was established");
        return connection;  
    }  
    public async Task<SqlConnection> CloseAsync()   
    {  
        logger.LogDebug("CloseAsync:try close connection");

        if (connection.State != System.Data.ConnectionState.Closed)   
            await connection.CloseAsync();

        logger.LogDebug("OpenAsync:the connection was closed");
        return connection;  
    }  


    public async void Dispose()
    {
         await this.CloseAsync();
    }

    public async Task<SqlDataReader> ExecuteReaderAsync(string SQL){

        SqlCommand command = new SqlCommand(SQL, connection,transaction);
        logCommand(command);

        await this.OpenAsync();
        
        return await command.ExecuteReaderAsync();

    }
    public async Task<SqlDataReader> ExecuteReaderAsync(string SQL,CommandType commandType,params SqlParameter[] parameters){

       
        SqlCommand command = new SqlCommand(SQL, connection, transaction);
        command.CommandType=commandType;
        command.Parameters.Clear();
        parameters.ToList().ForEach(p=>command.Parameters.Add(p));
        logCommand(command);
        await this.OpenAsync();
        return await command.ExecuteReaderAsync();

    }

    private void logCommand(SqlCommand command){
        if(!this.logger.IsEnabled(LogLevel.Debug)) return;

        StringBuilder sb = new StringBuilder();
         sb.AppendLine($"SQL:{command.CommandText}");
         sb.AppendLine("Parameters:");
        foreach (SqlParameter param in command.Parameters)
        {
            sb.AppendLine($"\t{param.ParameterName},{param.Value}");
        }

        this.logger.LogDebug(sb.ToString());
        
       
    }

    public async Task<int> ExecuteQueryAsync(string SQL,CommandType commandType,params SqlParameter[] parameters)  
    {    
       
        SqlCommand command = new SqlCommand(SQL, connection, transaction); 
        command.CommandType=commandType;
        command.Parameters.Clear();
        parameters.ToList().ForEach(p=>command.Parameters.Add(p));
        logCommand(command);

        await this.OpenAsync();

        
        try{
            logger.LogDebug("Try executing");
            int numberOfUpdatedRows =  await command.ExecuteNonQueryAsync();
            logger.LogDebug($"Command executed: {numberOfUpdatedRows}");

            return numberOfUpdatedRows;

        }catch(Exception ex){
            if (transaction != null)
            {
                await this.RollbackAsync();
            }

            logger.LogError(0, ex, "Failed ExecuteQueryAsync");

            throw new Apparka.Repository.Exceptions.DbException();
        }finally{
            logger.LogDebug("Executed");
            await this.CloseAsync();
        }
    }

    public async Task<object> ExecuteScalarAsync(string SQL, CommandType commandType, params SqlParameter[] parameters)
    {

        SqlCommand command = new SqlCommand(SQL, connection,transaction);
        command.CommandType = commandType;
        command.Parameters.Clear();
        parameters.ToList().ForEach(p => command.Parameters.Add(p));
        logCommand(command);

        await this.OpenAsync();


        try
        {
            logger.LogDebug("Try executing");
            object valor = await command.ExecuteScalarAsync();
            logger.LogDebug($"Command executed: {valor}");

            return valor;

        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await this.RollbackAsync();
            }

            logger.LogError(0, ex, "Failed ExecuteScalarAsync");

            throw new Apparka.Repository.Exceptions.DbException();
        }
        finally
        {
            logger.LogDebug("Executed");
            await this.CloseAsync();
        }
    }

    public async Task<DataSet> ExecuteDatasetAsync(string SQL, CommandType commandType, params SqlParameter[] parameters)
    {

        var dataSet = new DataSet();
        var adapter = new SqlDataAdapter();

        var cmdSelect = new SqlCommand(SQL, connection,transaction);
        cmdSelect.CommandType = commandType;
        cmdSelect.Parameters.Clear();
        parameters.ToList().ForEach(p => cmdSelect.Parameters.Add(p));
        logCommand(cmdSelect);

        adapter.SelectCommand = cmdSelect;

        try
        {
            await this.OpenAsync();

            adapter.Fill(dataSet);

            return dataSet;
        }
        catch (Exception ex)
        {
            if (transaction != null) {
                await this.RollbackAsync();
            }

            logger.LogError(0, ex, "Failed ExecuteQueryAsync");

            throw new Apparka.Repository.Exceptions.DbException();
        }
        finally
        {
            logger.LogDebug("Executed");
            await this.CloseAsync();
        }
    }

    private SqlTransaction transaction;

    public async Task<DbTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel) {

        logger.LogDebug("OpenAsync:try open connection");

        if (transaction==null)
            transaction = (await connection.BeginTransactionAsync(isolationLevel)) as SqlTransaction;

        logger.LogDebug("OpenAsync:the connection was established");
        return transaction;

    }

    public async Task CommitAsync() {
        await transaction.CommitAsync();
        transaction = null;
    }

    public async Task RollbackAsync()
    {
        await transaction.RollbackAsync();
        transaction = null;
    }
}