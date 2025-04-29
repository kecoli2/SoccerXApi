using System;
using AutoMapper;
using SoccerX.Common.Helpers;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto.User;

namespace SoccerX.DTO.Mappers
{
    public class UserProfile: Profile
    {
        #region Field
        #endregion

        #region Constructor
        public UserProfile()
        {
            // Create DTO mappings
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Passwordhash, opt => opt.MapFrom(src => HashPassword(src.Password))) // You'll need to implement HashPassword
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Createdate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Isdeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Isemailconfirmed, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Followercount, opt => opt.MapFrom(_ => 0));

            CreateMap<UserUpdateDto, User>();
            CreateMap<UserUpdateAdminDto, User>();

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.ReferralUserName, opt => opt.MapFrom(src => src.Referraluser != null ? src.Referraluser.Username : null));
        }
        #endregion

        #region Public Method
        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            return password.Decrypt();
        }
        #endregion

        #region Private Method
        #endregion
    }
}
