using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karin.DataAccess.Interface;
using Karin.DomainService.Karin.Interface;

namespace Karin.DomainService.Karin
{
    class MobileService : BaseService<Domain.Tables.Mobile>, IMobileService
    {
        public MobileService(IKarinUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
