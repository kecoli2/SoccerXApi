using FluentValidation;
using SoccerX.Common.Properties;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Validators.User
{
    public class UserUpdateDtoValidator: AbstractValidator<UserUpdateDto>
    {
        #region Field
        #endregion

        #region Constructor
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(Resources.Address_NotEmpty);

            RuleFor(x => x.Phonenumber)
                .NotEmpty()
                .WithMessage(Resources.Phonenumber_NotEmpty);
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
