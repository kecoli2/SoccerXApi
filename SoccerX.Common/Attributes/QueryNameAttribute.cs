namespace SoccerX.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QueryNameAttribute: Attribute
    {

        #region Field
        public string Name { get; }
        #endregion

        #region Constructor
        public QueryNameAttribute(string name)
        {
            Name = name;
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion

    }
}
