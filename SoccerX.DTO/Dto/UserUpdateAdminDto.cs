using System;

namespace SoccerX.DTO.Dto
{
    public class UserUpdateAdminDto
    {
        #region Field
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateTime? Banenddate { get; set; }
        public Guid Countryid { get; set; }
        public Guid Cityid { get; set; }
        public bool Isdeleted { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
