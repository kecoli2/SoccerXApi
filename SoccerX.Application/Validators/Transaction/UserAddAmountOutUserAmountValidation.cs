using FluentValidation;
using SoccerX.DTO.Requests.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Application.Validators.Transaction
{
    public class UserAddAmountOutUserAmountValidation : AbstractValidator<RequestUserAddAmountOutUserAmount>
    {
        #region Field
        #endregion

        #region Constructor
        public UserAddAmountOutUserAmountValidation()
        {
            RuleFor(x => x.FromUser)
                .NotEmpty().WithMessage("FromUser is required.")
                .NotEqual(x => x.ToUser).WithMessage("FromUser and ToUser cannot be the same.");
            RuleFor(x => x.ToUser).NotEmpty().WithMessage("ToUser is required.");
            RuleFor(x => x.TransactionType).IsInEnum().WithMessage("TransactionType is invalid.");
            RuleFor(x => x.Amout).GreaterThan(0).WithMessage("Amount must be greater than or equal to 0.");                
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion        
    }
}
