using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SoccerX.Tests.Base.IntegrationFactory
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.UseContentRoot("../../../SoccerX.API"); // test projesine göre göreli yol
        //    builder.ConfigureServices(services =>
        //    {
        //        // test setup...
        //    });
        //}
        #endregion

        #region Private Method
        #endregion
    }
}
