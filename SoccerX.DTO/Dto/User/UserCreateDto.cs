using SoccerX.Domain.Enums;
using System;

namespace SoccerX.DTO.Dto.User
{
    public class UserCreateDto
    {
        #region Field
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public required string Password { get; set; } // Note: Changed from Passwordhash to Password for frontend
        public Guid? Referraluserid { get; set; }
        public Guid Countryid { get; set; }
        public Guid Cityid { get; set; }
        public string? Postalcode { get; set; }
        public required string Address { get; set; }
        public required string Phonenumber { get; set; }
        public UserGender Gender { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
