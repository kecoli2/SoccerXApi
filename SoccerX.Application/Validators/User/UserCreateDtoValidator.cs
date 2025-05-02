using FluentValidation;
using SoccerX.Common.Properties;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        #region Field
        #endregion

        #region Constructor
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(Resources.Username_NotEmpty)
                .MinimumLength(3).WithMessage(Resources.Username_MinimumLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resources.Email_NotEmpty)
                .EmailAddress().WithMessage(Resources.Email_EmailAddress);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(Resources.Address_NotEmpty);

            RuleFor(x => x.Phonenumber)
                .NotEmpty().WithMessage(Resources.Phonenumber_NotEmpty);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Resources.Password_NotEmpty)
                .MinimumLength(8).WithMessage(Resources.Password_MinimumLength)
                .Matches(@"[A-Z]").WithMessage(Resources.Password_UppercaseLetter)
                .Matches(@"[0-9]").WithMessage(Resources.Password_Digit);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Resources.Name_NotEmpty);

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage(Resources.Surname_NotEmpty);
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
