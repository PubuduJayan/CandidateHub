using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications.Candidates
{
    public class GetCandidateByIdSpec : Specification<CandidateProfile>
    {
        public GetCandidateByIdSpec(Guid id)
        {
            Query.Where(a => a.Id.Equals(id));
        }
    }
}
