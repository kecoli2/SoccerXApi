using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SoccerX.Common.Shared.Model;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto;

namespace SoccerX.DTO.Mappers
{
    public class MappingUserProfile : Profile
    {
        #region Field
        #endregion

        #region Constructor
        public MappingUserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<PagedResult<User>, PagedResultDto<UserDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));


        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
