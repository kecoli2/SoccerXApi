namespace SoccerX.Common.Shared.Model
{
    public class CompositeCursorGuid: IComparable<CompositeCursorGuid>, IComparable
    {
        #region Field
        public DateTime CreateDate { get; set; }
        public Guid Id { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public int CompareTo(CompositeCursorGuid? other)
        {
            if (other == null) return 1;

            // Önce CreateDate'e göre karşılaştırma yapalım
            var dateComparison = CreateDate.CompareTo(other.CreateDate);
            return dateComparison != 0 ? dateComparison :
                // Aynı CreateDate'e sahipse, Id'ye göre karşılaştıralım
                Id.CompareTo(other.Id);
        }

        public int CompareTo(object? obj)
        {
            return CompareTo(obj as CompositeCursorGuid);
        }
        #endregion

        #region Private Method
        #endregion


    }
}
