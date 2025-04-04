namespace SoccerX.API.StartUp
{
    public static class SwaggerExtension
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(); // BU MUTLAKA OLMALI
            return services;
        }
        #endregion

        #region Private Method
        #endregion

    }
}
