using System;

namespace SoccerX.DTO.Responses
{
    public class AuthResponseDto
    {
        #region Field
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }      // saniye
        public bool IsNewUser { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;

        #endregion

        #region Constructor

        #endregion

        #region Public Method

        #endregion

        #region Private Method

        #endregion
    }
}
