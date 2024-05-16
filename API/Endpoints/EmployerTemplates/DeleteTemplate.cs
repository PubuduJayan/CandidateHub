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
    public class DeleteTemplate : EndpointBaseAsync.WithRequest<DeleteTemplateRequest>.WithActionResult<DeleteTemplateResponce>
    {
        private readonly IRepository<Template> _templateRepo;
        private readonly IRepository<Information> _informatioRepo;

        public DeleteTemplate(IRepository<Template> templateRepo, IRepository<Information> informatioRepo)
        {
            _templateRepo = templateRepo;
            _informatioRepo = informatioRepo;
        }

        [HttpDelete(DeleteTemplateRequest.Route)]
        [SwaggerOperation(
            Summary = "Delete Existing Template",
            Description = "Delete Existing Template for candidate",
            OperationId = "delete.template",
            Tags = new[] { "TemplateEndPoints" })
        ]
        public override async Task<ActionResult<DeleteTemplateResponce>> HandleAsync([FromRoute] DeleteTemplateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new DeleteTemplateResponce();
            try
            {
                var spec = new GetTemplateByIdSpec(request.Id);
                var template = await _templateRepo.FirstOrDefaultAsync(spec);
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
                await _informatioRepo.DeleteAsync(information);
                await _templateRepo.DeleteAsync(template);
             

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            response.Message = AppResponseMessages.Success;
            return Ok(response);
        }
    }
}