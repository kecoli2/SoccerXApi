using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Verification
{
    public interface IEmailVerificationService
    {
        public Task<bool> ConfirmCodeAsync(string code);
    }
}
