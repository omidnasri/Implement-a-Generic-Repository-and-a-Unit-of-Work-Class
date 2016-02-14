using Karin.DataAccess.Interface;
using Karin.DomainService.Karin.Interface;

namespace Karin.DomainService.Karin
{
    public class PersonService : BaseService<Domain.Tables.Person>, IPersonService
    {
        public PersonService(IKarinUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
