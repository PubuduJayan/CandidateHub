using Core.Entities;
using Core.Models;

namespace API.Endpoints.CandidateProfiles
{
    public class CreateResponce : BaseResponse
    {
       public CandidateProfile Candidate { get; set; }
    }
}