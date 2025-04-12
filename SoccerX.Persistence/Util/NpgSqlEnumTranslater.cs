using Npgsql;

namespace SoccerX.Persistence.Util
{
    public class NpgSqlEnumTranslater : INpgsqlNameTranslator
    {
        public string TranslateMemberName(string clrName) => clrName;
        public string TranslateTypeName(string clrName) => clrName;        
    }
}
