using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Infrastructure.Util
{
    public class NpgSqlEnumTranslater : INpgsqlNameTranslator
    {
        public string TranslateMemberName(string clrName) => clrName;
        public string TranslateTypeName(string clrName) => clrName;        
    }
}
