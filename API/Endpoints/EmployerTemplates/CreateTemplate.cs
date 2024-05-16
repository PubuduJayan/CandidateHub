using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications.Templates;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.EmployerTemplates
{
    public class CreateTemplate : EndpointBaseAsync.WithRequest<CreateTemplateRequest>.WithActionResult<CreateTemplateResponce>
    {
        private readonly IRepository<Template> _templateRepo;
        private readonly IRepository<Information> _informatioRepo;

        public CreateTemplate(IRepository<Template> templateRepo, IRepository<Information> informatioRepo)
        {
            _templateRepo = templateRepo;
            _informatioRepo = informatioRepo;
        }

        [HttpPost(CreateTemplateRequest.Route)]
        [SwaggerOperation(
            Summary = "Create New Template",
            Description = "Create New Template for candidate",
            OperationId = "create.template",
            Tags = new[] { "TemplateEndPoints" })
        ]
        public override async Task<ActionResult<CreateTemplateResponce>> HandleAsync([FromBody] CreateTemplateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new CreateTemplateResponce();
            var responseDto = new TemplateResponseDto();
            try
            {
                var spec = new GetTemplateByNameSpec(request.Temaplate.Title);
                var template = await _templateRepo.FirstOrDefaultAsync(spec);
                if (template != null)
                {
                    response.Message = "Template already exist";
                    return BadRequest(response);
                }
                var createInformation = new Information();
                createInformation.PhoneIsInternal = request.Temaplate.PhoneIsInternal;
                createInformation.PhoneIsHide = request.Temaplate.PhoneIsHide;
                createInformation.NationalityIsInternal = request.Temaplate.NationalityIsInternal;
                createInformation.NationalityIsHide = request.Temaplate.NationalityIsHide;
                createInformation.ResidenceIsInternal = request.Temaplate.ResidenceIsInternal;
                createInformation.ResidenceIsHide = request.Temaplate.ResidenceIsHide;
                createInformation.IDNumberIsInternal = request.Temaplate.IDNumberIsInternal;
                createInformation.IDNumberIsHide = request.Temaplate.IDNumberIsHide;
                createInformation.DOBIsInternal = request.Temaplate.DOBIsInternal;
                createInformation.DOBIsHide = request.Temaplate.DOBIsHide;
                createInformation.GenderIsInternal = request.Temaplate.GenderIsInternal;
                createInformation.GenderIsHide = request.Temaplate.GenderIsHide;
                foreach ( var item in request.QuestionCreate)
                {
                    var createQuestion = new Question(item.Type, item.Question)
                    {
                        Choices = item.Choices,
                        IsOtherOption = item.IsOtherOption,
                        MaxChoice = item.MaxChoice,
                    };
                    createInformation.AddQuestion(createQuestion);
                }
                var newInformation = await _informatioRepo.AddAsync(createInformation);               
                var createTemplate = new Template(request.Temaplate.Title, request.Temaplate.Description, newInformation.Id);
                var newTemplate = await _templateRepo.AddAsync(createTemplate);
                
                responseDto.TemapleId = newTemplate.Id;
                responseDto.Title = newTemplate.Title;
                responseDto.Description = newTemplate.Description;
                responseDto.InformationId = createInformation.Id;
                responseDto.PhoneIsInternal = createInformation.PhoneIsInternal;
                responseDto.PhoneIsHide = createInformation.PhoneIsHide;
                responseDto.NationalityIsInternal = createInformation.NationalityIsInternal;
                responseDto.NationalityIsHide = createInformation.NationalityIsHide;
                responseDto.ResidenceIsInternal = createInformation.ResidenceIsInternal;
                responseDto.ResidenceIsHide = createInformation.ResidenceIsHide;
                responseDto.IDNumberIsInternal = createInformation.IDNumberIsInternal;
                responseDto.IDNumberIsHide = createInformation.IDNumberIsHide;
                responseDto.DOBIsInternal = createInformation.DOBIsInternal;
                responseDto.DOBIsHide = createInformation.DOBIsHide;
                responseDto.GenderIsInternal = createInformation.GenderIsInternal;
                responseDto.GenderIsHide = createInformation.GenderIsHide;
                responseDto.Questions = createInformation.Questions.ToList();

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            response.Template = responseDto;
            response.Message = "Template saved successfully";
            return Ok(response);
        }
    }
}