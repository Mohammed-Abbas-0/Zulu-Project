using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zulu_Project.DTO;
using Zulu_Project.Models;

namespace Zulu_Project.Mapper
{
    public class ObjectMapper:Profile
    {
        public ObjectMapper()
        {
            CreateMap<Company,CompanyDTO>().ReverseMap();
            CreateMap<Branch,BranchDTO>().ReverseMap();
            CreateMap<Branch, BranchCreatedDTO>().ReverseMap();
            CreateMap<Branch, BranchUpdateDTO>().ReverseMap();
        }
    }
}
