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
    public class AppController : Controller
    {
        ServerContext db;
        private readonly IMapper _mapper;

        public AppController(ServerContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<App>>> Get()
        {
            return await db.Apps.OrderBy(x => x.Name).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<App>> Get(int id)
        {
            App App = await db.Apps.Include(v => v.Vm).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(App);
        }

        [HttpPost]
        public async Task<ActionResult<AppModel>> Post([FromBody]AppModel AppModel)
        {      
            if (ModelState.IsValid)
            {
                var app = _mapper.Map<App>(AppModel);
                db.Apps.Add(app);
                await db.SaveChangesAsync();
                return Ok(app);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]AppModel AppModel)
        {
            if (ModelState.IsValid)
            {
                var app = _mapper.Map<App>(AppModel);
                db.Update(app);
                await db.SaveChangesAsync();
                return Ok(app);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<App>> Delete(int id)
        {
            App App = db.Apps.FirstOrDefault(x => x.Id == id);
            if (App != null)
            {
                db.Apps.Remove(App);
                await db.SaveChangesAsync();
            }
            return Ok(App);
        }
    }
}