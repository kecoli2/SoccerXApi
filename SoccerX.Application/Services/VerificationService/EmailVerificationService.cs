using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Interfaces.Verification;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Services.VerificationService
{
    public class EmailVerificationService: IEmailVerificationService
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public EmailVerificationService(IEmailVerificationRepository repository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Method
        public async Task<bool> ConfirmCodeAsync(string code)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var record = await _unitOfWork.EmailVerificationRepository.GetByCodeAsync(code);
                if (record is null) return false;

                Expression<Func<User, User>> selector = user1 => new User
                    { Isemailconfirmed = user1.Isemailconfirmed };

                var user = await _unitOfWork.UserRepository.GetByIdAsync(record.Userid, selector);
                if (user is null) return false;

                user.Isemailconfirmed = true;
                record.Isused = true;

                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.EmailVerificationRepository.Update(record);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        #endregion

        #region Private Method
        #endregion
    }
}
