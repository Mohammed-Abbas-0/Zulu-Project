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
    [Route("api/Branch")]
    [ApiExplorerSettings(GroupName = "Branch_V1")]
    //[ApiExplorerSettings(GroupName = "Company_V1")]

    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        public BranchController(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        #region Get Branches
        [HttpGet("GetBranches")]
        [ProducesResponseType(200, Type = typeof(List<BranchDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            List<Branch> Branches =  await _branchRepository.GetAll();
            List<BranchDTO> companyDTOs = new();
            foreach(Branch company in Branches)
            {
                companyDTOs.Add(_mapper.Map<BranchDTO>(company));
            }
            return Ok(companyDTOs);
        }
        #endregion

        #region Get Branch By ID

        [HttpGet("GetBranchById/{Id:int}",Name = "GetBranchById")]
        [ProducesResponseType(200,Type=typeof(BranchDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBranchById(int Id)
        {
            if (Id <= 0)
                return Ok("Invalid Id");

            Branch company = await _branchRepository.GetById(Id);
            if (company is null)
                return BadRequest("Branch Not Found");

            BranchDTO companyDTO = _mapper.Map<BranchDTO>(company);

            return Ok(companyDTO);
        }

        #endregion

        #region C R E A T E
        [HttpPost("CreateBranch")]
        public async Task<IActionResult> CreateBranch([FromForm] BranchCreatedDTO branchDTO)
        {
            if (branchDTO is null)
                return BadRequest();
            
            if (await _branchRepository.IsExsited(branchDTO.Name))
            {
                ModelState.AddModelError("", "Branch Already Existed Before");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest();
            Branch branch = _mapper.Map<Branch>(branchDTO);
            await _branchRepository.Create(branch);

            return CreatedAtRoute("GetBranchById", new { Id = branch.Id}, branch);
        }
        #endregion

        #region U P D A T E
        [HttpPatch(Name = "UpdateBranch")]
        public async Task<IActionResult> Update(int Id,[FromForm] BranchUpdateDTO branchDTO)
        {
            if (branchDTO.Id == 0 || Id == 0 || Id != branchDTO.Id)
                return BadRequest();
            if (!ModelState.IsValid || branchDTO is null)
                return BadRequest();
            Branch branch = _mapper.Map<Branch>(branchDTO);
            await _branchRepository.Update(branch);
            return NoContent();
        }

        #endregion

        #region D E L E T E
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == 0) 
                return BadRequest();
           Branch company = await _branchRepository.GetById(Id);
            if (company is null)
                return BadRequest();
            await _branchRepository.Delete(company);
            return Ok("Done.");
        }
        #endregion
    }
}
