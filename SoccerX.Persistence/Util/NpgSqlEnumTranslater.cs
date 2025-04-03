using Npgsql;

namespace SoccerX.Infrastructure.Util
{
    public class NpgSqlEnumTranslater : INpgsqlNameTranslator
    {
        public string TranslateMemberName(string clrName) => clrName;
        public string TranslateTypeName(string clrName) => clrName;        
    }
}
