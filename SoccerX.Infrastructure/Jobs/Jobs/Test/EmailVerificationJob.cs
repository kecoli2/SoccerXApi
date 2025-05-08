using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Services.Email;
using SoccerX.Common.Attributes;
using SoccerX.Common.Base.Quartz.Criteria;
using SoccerX.Common.Enums;
using SoccerX.Domain.Entities;
using SoccerX.Infrastructure.Jobs.Base;

namespace SoccerX.Infrastructure.Jobs.Jobs.Test
{
    [JobAttributes(JobKeyEnum.SendVerificationMail, JobCategoryEnum.PublicJob,"SendMail","SendMailDesc",typeof(SendEmailVerifcationCriteria), false)]
    public class EmailVerificationJob: BaseJob<SendEmailVerifcationCriteria>
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public EmailVerificationJob(IUnitOfWork unitOfWork, IEmailService emailService) : base()
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public override async Task Executing()
        {
            var code = new Random().Next(100000, 999999).ToString();
            var eMailVerification = new Emailverification
            {
                Code = code,
                Createdate = DateTime.Now,
                Expiresat = DateTime.Now.AddMinutes(5),
                Isused = false,
                Userid = Guid.Parse(JobCriteria!.UserId)
            };
            _unitOfWork.EmailVerificationRepository.Update(eMailVerification);
            await _unitOfWork.EmailVerificationRepository.AddAsync(eMailVerification);
            await _unitOfWork.CommitAsync();
            var result = await _emailService.SendEmailAsync(JobCriteria.ToMailAddress, "EmailVerification", code);                        
            if (1 == 1)
            {

            }
        }
        #endregion

        #region Private Method
        #endregion
    }
}
