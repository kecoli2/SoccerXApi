using AutoMapper;
using SoccerX.Common.Shared.Model;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto;

namespace SoccerX.DTO.Mappers
{
    public class MappingCityProfile: Profile
    {
        #region Field
        #endregion

        #region Constructor

        public MappingCityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<PagedResult<City>, PagedResultDto<CityDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));

            CreateMap(typeof(CursorPagedResult<,>), typeof(CursorPagedResultDto<,>))
                .ForMember("HasMore", opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    // Domain modeldeki LastCursor değerini alıp, null olup olmadığını kontrol ediyoruz.
                    var lastCursorProperty = src.GetType().GetProperty("LastCursor");
                    var lastCursorValue = lastCursorProperty?.GetValue(src);
                    return lastCursorValue != null;
                }));
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
