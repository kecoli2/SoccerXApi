using System;

namespace SoccerX.DTO.Dto
{
    public class UserResponseDto
    {
        #region Field
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateTime? Banenddate { get; set; }
        public int Followercount { get; set; }
        public Guid Countryid { get; set; }
        public string? CountryName { get; set; } // Additional for display
        public Guid Cityid { get; set; }
        public string? CityName { get; set; } // Additional for display
        public string? Postalcode { get; set; }
        public required string Address { get; set; }
        public required string Phonenumber { get; set; }
        public string? Avatarurl { get; set; }
        public DateTime Createdate { get; set; }
        public bool Isemailconfirmed { get; set; }
        public string? ReferralUserName { get; set; } // Additional for display
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
