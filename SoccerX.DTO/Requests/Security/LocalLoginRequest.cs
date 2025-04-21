namespace SoccerX.DTO.Requests.Security
{
    public class LocalLoginRequest
    {
        #region Field
        public string EmailOrUserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        #endregion
    }
}
