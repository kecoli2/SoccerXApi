using FluentValidation;
using SoccerX.Common.Properties;
using SoccerX.Domain.Enums;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Validators.User
{
    public class UserUpdateAdminDtoValidator : AbstractValidator<UserUpdateAdminDto>
    {
        #region Field
        #endregion

        #region Constructor
        public UserUpdateAdminDtoValidator()
        {
            RuleFor(x => x.Banenddate)
                .NotEmpty()
                .When(x => x.Status == UserStatus.Banned)
                .WithMessage(Resources.BanEndDate_Required);                
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
