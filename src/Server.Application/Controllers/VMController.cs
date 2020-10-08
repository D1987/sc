using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Dal;
using Server.Dtos;
using Server.Entities.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VMController : Controller
    {
        ServerContext db;
        private readonly IMapper _mapper;

        public VMController(ServerContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VM>>> Get()
        {
            return await db.VMs.OrderBy(x => x.Name).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VM>> Get(int id)
        {
            VM VM = await db.VMs.Include(a => a.Apps).Include(h => h.Host).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(VM);
        }

        [HttpPost]
        public async Task<ActionResult<VMModel>> Post([FromBody] VMModel VMModel)
        {
            if (ModelState.IsValid)
            {
                var vm = _mapper.Map<VM>(VMModel);
                db.VMs.Add(vm);
                await db.SaveChangesAsync();
                return Ok(vm);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] VMModel VMModel)
        {
            if (ModelState.IsValid)
            {
                var vm = _mapper.Map<VM>(VMModel);
                db.Update(vm);
                await db.SaveChangesAsync();
                return Ok(vm);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VM>> Delete(int id)
        {
            VM VM = db.VMs.FirstOrDefault(x => x.Id == id);
            if (VM != null)
            {
                db.VMs.Remove(VM);
                await db.SaveChangesAsync();
            }
            return Ok(VM);
        }
    }
}
