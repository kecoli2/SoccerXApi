using FluentValidation;
using SoccerX.Common.Properties;
using SoccerX.DTO.Requests.Security;

namespace SoccerX.Application.Validators.Security
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        #region Field
        #endregion

        #region Constructor
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(Resources.Password_NotEmpty)
                .MinimumLength(8).WithMessage(Resources.Password_MinimumLength)
                .Matches(@"[A-Z]").WithMessage(Resources.Password_UppercaseLetter)
                .Matches(@"[0-9]").WithMessage(Resources.Password_Digit);
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion        
    }
}
