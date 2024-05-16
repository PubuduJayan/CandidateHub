using Core.Entities;
using Core.Models;

namespace API.Endpoints.CandidateProfiles
{
    public class GetResponce : BaseResponse
    {
       public CandidateProfile Candidate { get; set; }
    }
}