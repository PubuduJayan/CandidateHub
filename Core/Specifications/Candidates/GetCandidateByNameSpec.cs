using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications.Candidates
{
    public class GetCandidateByNameSpec : Specification<CandidateProfile>
    {
        public GetCandidateByNameSpec(string name)
        {
            Query.Where(a => a.FirstName.Equals(name));
        }
    }
}
