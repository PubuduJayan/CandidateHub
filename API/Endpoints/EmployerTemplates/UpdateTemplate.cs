using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications.Informations;
using Core.Specifications.Templates;
using Microsoft.AspNetCore.Mvc;
using SharedKernal;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.EmployerTemplates
{
    public class UpdateTemplate : EndpointBaseAsync.WithRequest<UpdateTemplateRequest>.WithActionResult<UpdateTemplateResponce>
    {
        private readonly IRepository<Template> _templateRepo;
        private readonly IRepository<Information> _informatioRepo;

        public UpdateTemplate(IRepository<Template> templateRepo, IRepository<Information> informatioRepo)
        {
            _templateRepo = templateRepo;
            _informatioRepo = informatioRepo;
        }

        [HttpPut(UpdateTemplateRequest.Route)]
        [SwaggerOperation(
            Summary = "Update Existing Template",
            Description = "Update Existing Template for candidate",
            OperationId = "update.template",
            Tags = new[] { "TemplateEndPoints" })
        ]
        public override async Task<ActionResult<UpdateTemplateResponce>> HandleAsync(UpdateTemplateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new UpdateTemplateResponce();
            var responseDto = new TemplateResponseDto();
            try
            {
                var spec = new GetTemplateByIdSpec(request.Id);
                var template = await _templateRepo.FirstOrDefaultAsync(spec);
                if (template == null)
                {
                    response.Message = AppResponseMessages.Record_NotExist;
                    return NotFound(response);
                }
                template.Title = request.Temaplate?.Title?? template.Title;
                template.Description = request.Temaplate?.Description ?? template.Description;
                await _templateRepo.UpdateAsync(template);

                if (request.Temaplate != null)
                {
                    var specTwo = new GetInformationByIdSpec(template.InformationId);
                    var getInformation = await _informatioRepo.FirstOrDefaultAsync(specTwo);
                    if (getInformation == null)
                    {
                        response.Message = AppResponseMessages.Record_NotExist;
                        return NotFound(response);
                    }
                    getInformation.PhoneIsInternal = request.Temaplate.PhoneIsInternal;
                    getInformation.PhoneIsHide = request.Temaplate.PhoneIsHide;
                    getInformation.NationalityIsInternal = request.Temaplate.NationalityIsInternal;
                    getInformation.NationalityIsHide = request.Temaplate.NationalityIsHide;
                    getInformation.ResidenceIsInternal = request.Temaplate.ResidenceIsInternal;
                    getInformation.ResidenceIsHide = request.Temaplate.ResidenceIsHide;
                    getInformation.IDNumberIsInternal = request.Temaplate.IDNumberIsInternal;
                    getInformation.IDNumberIsHide = request.Temaplate.IDNumberIsHide;
                    getInformation.DOBIsInternal = request.Temaplate.DOBIsInternal;
                    getInformation.DOBIsHide = request.Temaplate.DOBIsHide;
                    getInformation.GenderIsInternal = request.Temaplate.GenderIsInternal;
                    getInformation.GenderIsHide = request.Temaplate.GenderIsHide;
                    if(request.QuestionUpdate.Count > 0)
                    {
                        getInformation.RemoveAllQuestions();
                        foreach (var item in request.QuestionUpdate)
                        {
                            var createQuestion = new Question(item.Type, item.Question)
                            {
                                Choices = item.Choices,
                                IsOtherOption = item.IsOtherOption,
                                MaxChoice = item.MaxChoice,
                            };
                            getInformation.AddQuestion(createQuestion);
                        }
                    }
                    await _informatioRepo.SaveChangesAsync();
                    responseDto.TemapleId = template.Id;
                    responseDto.Title = template.Title;
                    responseDto.Description = template.Description;
                    responseDto.PhoneIsInternal = getInformation.PhoneIsInternal;
                    responseDto.PhoneIsHide = getInformation.PhoneIsHide;
                    responseDto.NationalityIsInternal = getInformation.NationalityIsInternal;
                    responseDto.NationalityIsHide = getInformation.NationalityIsHide;
                    responseDto.ResidenceIsInternal = getInformation.ResidenceIsInternal;
                    responseDto.ResidenceIsHide = getInformation.ResidenceIsHide;
                    responseDto.IDNumberIsInternal = getInformation.IDNumberIsInternal;
                    responseDto.IDNumberIsHide = getInformation.IDNumberIsHide;
                    responseDto.DOBIsInternal = getInformation.DOBIsInternal;
                    responseDto.DOBIsHide = getInformation.DOBIsHide;
                    responseDto.GenderIsInternal = getInformation.GenderIsInternal;
                    responseDto.GenderIsHide = getInformation.GenderIsHide;
                    responseDto.Questions = getInformation.Questions.ToList();

                }                             
                             

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }
            response.Template = responseDto;
            response.Message = "Template update successfully";
            return Ok(response);
        }
    }
}