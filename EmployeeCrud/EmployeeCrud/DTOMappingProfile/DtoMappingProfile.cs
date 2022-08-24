using AutoMapper;
using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.DTOMappingProfile
{
    public class DtoMappingProfile:Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Employee, EmployeeListDto>().ReverseMap();        

        }
    }
}
