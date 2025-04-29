using System;

namespace SoccerX.DTO.Dto
{
    public class UserUpdateDto
    {
        #region Field
        public Guid Id { get; set; }
        public DateOnly Birthdate { get; set; }
        public Guid Countryid { get; set; }
        public Guid Cityid { get; set; }
        public string? Postalcode { get; set; }
        public required string Address { get; set; }
        public required string Phonenumber { get; set; }
        public string? Avatarurl { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
