using SoccerX.Application.Parameters.FotballApi.Parameters;
using SoccerX.DTO.Responses.FootballApi;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.FootballApiManager.Services
{
    public interface IFootballApiCountriesService
    {
        /// <summary>
        /// Ülke Listesi Alma işlemi için kullanılan endpoint.
        /// Ligler endpoint’i ile birlikte kullanılabilecek ülkelerin listesini sağlar.
        /// </summary>
        /// <remarks>
        /// <b>Filtreleme Özellikleri:</b><br/>
        /// - <c>name</c>: Ülke adı<br/>
        /// - <c>code</c>: Ülke kodu<br/>
        /// Bu alanlar diğer endpoint’lerde filtre olarak kullanılabilir.
        ///
        /// <b>Bayrak Görselleri:</b><br/>
        /// Bir ülkenin bayrağını görüntülemek için aşağıdaki URL yapısı kullanılır:<br/>
        /// <c>https://media.api-sports.io/flags/{country_code}.svg</c><br/>
        /// Örnek: <c>https://media.api-sports.io/flags/tr.svg</c> (Türkiye bayrağı)
        ///
        /// <b>Örnek Kullanımlar:</b><br/>
        /// "Request Samples" bölümündeki "Use Cases" kısmına bakınız.
        ///
        /// <b>Parametre Uyumu:</b><br/>
        /// Bu endpoint’in tüm parametreleri birlikte uyumlu şekilde kullanılabilir.
        ///
        /// <b>Sistem Bilgisi:</b><br/>
        /// - Güncelleme Sıklığı: API kapsamına yeni bir ülke eklendiğinde güncellenir.<br/>
        /// - Önerilen Çağrı Sıklığı: Günde 1 kez sorgulanması tavsiye edilir.
        ///
        /// <b>Önemli Not:</b><br/>
        /// Ülke kodları (örneğin TR, EN, DE) diğer endpoint’lerde filtreleme için kullanılabilir.<br/>
        /// Bayrak görsellerine erişirken <c>küçük harfli</c> ülke kodu kullanılmalıdır (örnek: <c>tr.svg</c>).
        /// </remarks>

        Task<FootBallApiResponse<FootBallApiCountriesResponse>?> GetCountriesAsync(CountriesParameters? parameters = null);
    }
}
