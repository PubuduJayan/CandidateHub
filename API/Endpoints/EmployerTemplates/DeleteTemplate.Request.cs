
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.EmployerTemplates
{
    public class DeleteTemplateRequest
    {
        public const string Route = "/Template/{Id}";
        public required Guid Id { get; set; }
    }

   
}