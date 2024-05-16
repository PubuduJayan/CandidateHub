using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications.Informations
{
    public class GetInformationByIdSpec : Specification<Information>
    {
        public GetInformationByIdSpec(Guid id)
        {
            Query.Where(a => a.Id.Equals(id))
                .Include(a => a.Questions);
               
        }
    }
}
