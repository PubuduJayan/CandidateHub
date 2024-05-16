using Core.Models;

namespace API.Endpoints.CandidateProfiles
{
    public class GetRequest
    {
        public const string Route = "/Candidate/{Id}";
        public required Guid Id { get; set; }
    }
}