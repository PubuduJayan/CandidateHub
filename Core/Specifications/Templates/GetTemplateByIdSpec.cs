using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications.Templates
{
    public class GetTemplateByIdSpec : Specification<Template>
    {
        public GetTemplateByIdSpec(Guid id)
        {
            Query.Where(a => a.Id.Equals(id));
        }
    }
}
