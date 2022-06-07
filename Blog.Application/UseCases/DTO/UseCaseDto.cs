using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class UseCaseDto : BaseDto
    {
        public string UseCaseName { get; set; }
        public string Description { get; set; }
    }
}
