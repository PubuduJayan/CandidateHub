using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.Candidates;
using Microsoft.AspNetCore.Mvc;
using SharedKernal;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.CandidateProfiles
{
    public class Get : EndpointBaseAsync.WithRequest<GetRequest>.WithActionResult<GetResponce>
    {
        private readonly IRepository<CandidateProfile> _candidateRepo;

        public Get(IRepository<CandidateProfile> candidateRepo)
        {
            _candidateRepo = candidateRepo;   
        }

        [HttpGet(GetRequest.Route)]
        [SwaggerOperation(
            Summary = "Get a Candiate",
            Description = "Get a Candiate",
            OperationId = "get.candidate",
            Tags = new[] { "CandidateEndPoints" })
        ]
        public override async Task<ActionResult<GetResponce>> HandleAsync([FromRoute]GetRequest request, CancellationToken cancellationToken = default)
        {
            var response = new GetResponce();
            try
            {
                var spec = new GetCandidateByIdSpec(request.Id);
                var candidate = await _candidateRepo.FirstOrDefaultAsync(spec);
                if (candidate == null)
                {
                    response.Message = AppResponseMessages.Record_NotExist;
                    return NotFound(response);
                }              

                response.Candidate = candidate;
                response.Message = AppResponseMessages.Success;

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
