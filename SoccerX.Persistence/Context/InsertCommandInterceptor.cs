using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Persistence.Context
{
    public class InsertCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<int> NonQueryExecuting(
            DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            if (command.CommandText.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("INSERT SQL: " + command.CommandText);
            }
            return base.NonQueryExecuting(command, eventData, result);
        }
    }

}
