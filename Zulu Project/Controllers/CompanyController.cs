using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zulu_Project.DTO;
using Zulu_Project.Mapper;
using Zulu_Project.Models;
using Zulu_Project.Repositories.IRepositories;

namespace Zulu_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        #region Get Companies
        [HttpGet("GetCompanies")]
        public async Task<IActionResult> GetAll()
        {
            List<Company> Companies =  await _companyRepository.GetAll();
            List<CompanyDTO> companyDTOs = new();
            foreach(Company company in Companies)
            {
                companyDTOs.Add(_mapper.Map<CompanyDTO>(company));
            }
            return Ok(companyDTOs);
        }
        #endregion

        #region Get Company By ID

        [HttpGet("GetCompanyById/{Id:int}",Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(int Id)
        {
            if (Id <= 0)
                return Ok("Invalid Id");

            Company company = await _companyRepository.GetById(Id);
            if (company is null)
                return BadRequest("Company Not Found");

            CompanyDTO companyDTO = _mapper.Map<CompanyDTO>(company);

            return Ok(companyDTO);
        }

        #endregion

        #region C R E A T E
        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromForm] CompanyDTO companyDTO)
        {
            if (companyDTO is null)
                return BadRequest();
            if (await _companyRepository.IsExsited(companyDTO.Id) || await _companyRepository.IsExsited(companyDTO.CompanyName))
            {
                ModelState.AddModelError("", "Company Already Existed Before");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();
            Company company = _mapper.Map<Company>(companyDTO);
            await _companyRepository.Create(company);

            return CreatedAtRoute("GetCompanyById", new { Id = company.Id},company);
        }
        #endregion

        #region U P D A T E
        [HttpPatch(Name = "UpdateCompany")]
        public async Task<IActionResult> Update([FromForm] CompanyDTO companyDTO)
        {
            if (!ModelState.IsValid || companyDTO is null)
                return BadRequest();
            Company company = _mapper.Map<Company>(companyDTO);
            await _companyRepository.Update(company);
            return NoContent();
        }

        #endregion

        #region D E L E T E
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == 0) 
                return BadRequest();
           Company company = await _companyRepository.GetById(Id);
            if (company is null)
                return BadRequest();
            await _companyRepository.Delete(company);
            return Ok("Done.");
        }
        #endregion
    }
}
