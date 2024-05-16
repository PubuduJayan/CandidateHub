using Core.Models;

namespace API.Endpoints.CandidateProfiles
{
    public class CreateRequest
    {
        public const string Route = "/Candidate";
        public required CandidateRequestDto RequestDto { get; set; }
    }
}