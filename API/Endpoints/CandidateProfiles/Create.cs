using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications.Candidates;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.CandidateProfiles
{
    public class Create : EndpointBaseAsync.WithRequest<CreateRequest>.WithActionResult<CreateResponce>
    {
        private readonly IRepository<CandidateProfile> _candidateRepo;

        public Create(IRepository<CandidateProfile> candidateRepo)
        {
            _candidateRepo = candidateRepo;   
        }

        [HttpPost(CreateRequest.Route)]
        [SwaggerOperation(
            Summary = "Create New Candiate",
            Description = "Create New Candiate",
            OperationId = "create.candidate",
            Tags = new[] { "CandidateEndPoints" })
        ]
        public override async Task<ActionResult<CreateResponce>> HandleAsync(CreateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new CreateResponce();
            try
            {
                var spec = new GetCandidateByNameSpec(request.RequestDto.Candidate.FirstName);
                var candidate = await _candidateRepo.FirstOrDefaultAsync(spec);
                if (candidate != null)
                {
                    response.Message = "Candidate already exist";
                    return BadRequest(response);
                }
                var newCandidate = new CandidateProfile(request.RequestDto.Candidate.TemapleId, request.RequestDto.Candidate.TemapleName
                    , request.RequestDto.Candidate.FirstName, request.RequestDto.Candidate.LastName, request.RequestDto.Candidate.Email);
                newCandidate.Phone = request.RequestDto.Candidate.Phone;
                newCandidate.Nationality = request.RequestDto.Candidate.Nationality;
                newCandidate.Residence = request.RequestDto.Candidate.Residence;
                newCandidate.IDNumber = request.RequestDto.Candidate.IDNumber;
                newCandidate.DOB = request.RequestDto.Candidate.DOB;
                newCandidate.Gender = request.RequestDto.Candidate.Gender;
                foreach(var item in request.RequestDto.Candidate.Questions)
                {
                    newCandidate.AddQuestion(item);
                }
                await _candidateRepo.AddAsync(newCandidate);

                response.Candidate = newCandidate;
                response.Message = "Candidate saved successfully";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
          
            return Ok(response);
        }
    }
}
