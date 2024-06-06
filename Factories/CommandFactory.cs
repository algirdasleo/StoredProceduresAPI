using System.Data;
using Dapper;

namespace API.Factories
{
    public class CommandFactory
    {
        public CommandDefinition CreateStoredProcedureCommand(string procedureName, DynamicParameters parameters)
        {
            return new CommandDefinition(
                procedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        
    }
}