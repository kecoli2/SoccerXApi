using SoccerX.Application.Interfaces.FootballApiManager;

public class TeamsInformationParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Takımın benzersiz kimlik numarası
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// string Takımın tam adı
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// integer Takımın yer aldığı ligin ID numarası
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// integer (4 haneli YYYY formatında) İlgili sezon bilgisi (örn. 2023)
    /// </summary>
    public string? Season { get; set; }
    /// <summary>
    /// string Takımın bağlı olduğu ülke adı
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// string (3 karakter) Takımın kısa kodu (örn. MUFC, FCB)
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// integer Takımın saha/stadyum bilgisi ID'si
    /// </summary>
    public string? Venue { get; set; }
    /// <summary>
    /// string (minimum 3 karakter) Takım adı veya ülke adına göre arama yapmayı sağlar
    /// </summary>
    public string? Search { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}