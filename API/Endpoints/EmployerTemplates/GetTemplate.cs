using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications.Informations;
using Core.Specifications.Templates;
using Microsoft.AspNetCore.Mvc;
using SharedKernal;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.WebSockets;

namespace API.Endpoints.EmployerTemplates
{
    public class GetTemplate : EndpointBaseAsync.WithRequest<GetTemplateRequest>.WithActionResult<GetTemplateResponce>
    {
        private readonly IReadRepository<Template> _templateRepo;
        private readonly IReadRepository<Information> _informatioRepo;

        public GetTemplate(IReadRepository<Template> templateRepo, IReadRepository<Information> informatioRepo)
        {
            _templateRepo = templateRepo;
            _informatioRepo = informatioRepo;
        }

        [HttpGet(GetTemplateRequest.Route)]
        [SwaggerOperation(
            Summary = "Get Existing Template",
            Description = "Get Existing Template for candidate",
            OperationId = "get.template",
            Tags = new[] { "TemplateEndPoints" })
        ]
        public override async Task<ActionResult<GetTemplateResponce>> HandleAsync([FromRoute] GetTemplateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new GetTemplateResponce();
            var responseDto = new TemplateResponseDto();
            try
            {
                var specOne = new GetTemplateByNameSpec(request.Title);
                var template = await _templateRepo.FirstOrDefaultAsync(specOne);
                if (template == null)
                {
                    response.Message = AppResponseMessages.Record_NotExist;
                    return NotFound(response);
                }
                var specTwo = new GetInformationByIdSpec(template.InformationId);
                var information = await _informatioRepo.FirstOrDefaultAsync(specTwo);
                if (information == null)
                {
                    response.Message = AppResponseMessages.Record_NotExist;
                    return NotFound(response);
                }           
              
                responseDto.TemapleId = template.Id;
                responseDto.Title = template.Title;
                responseDto.Description = template.Description;
                responseDto.InformationId = information.Id;
                responseDto.PhoneIsInternal = information.PhoneIsInternal;
                responseDto.PhoneIsHide = information.PhoneIsHide;
                responseDto.NationalityIsInternal = information.NationalityIsInternal;
                responseDto.NationalityIsHide = information.NationalityIsHide;
                responseDto.ResidenceIsInternal = information.ResidenceIsInternal;
                responseDto.ResidenceIsHide = information.ResidenceIsHide;
                responseDto.IDNumberIsInternal = information.IDNumberIsInternal;
                responseDto.IDNumberIsHide = information.IDNumberIsHide;
                responseDto.DOBIsInternal = information.DOBIsInternal;
                responseDto.DOBIsHide = information.DOBIsHide;
                responseDto.GenderIsInternal = information.GenderIsInternal;
                responseDto.GenderIsHide = information.GenderIsHide;
                responseDto.Questions = information.Questions.ToList();

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            response.Template = responseDto;
            response.Message = AppResponseMessages.Success;
            return Ok(response);
        }
    }
}