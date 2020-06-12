using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrotSpotAPI.Models;
using TrotSpotAPI.Models.Repo;

namespace TrotSpotAPI.Controllers
{
    [Route("api/Classes")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IDataRepository<Class> _dataRepository;
        private readonly TrotSpotContext _context;

        public ClassesController(IDataRepository<Class> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<IActionResult> GetClasses()
        {
            return Ok(await _dataRepository.GetAll());
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var @class = await _dataRepository.Get(id);

            if (@class == null)
            {
                return NotFound("Could Not find Class");
            }

            return @class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.ID)
            {
                return BadRequest();
            }
            try
            {
               await _dataRepository.Update(id, @class);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Classes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            if (@class == null)
            {
                return BadRequest();
            }

            await _dataRepository.Add(@class);

            return CreatedAtAction(nameof(GetClass), new { id = @class.ID }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Class>> DeleteClass(int id)
        {
            var @class = await _dataRepository.Get(id);
            if (@class == null)
            {
                return NotFound();
            }
            await _dataRepository.Delete(@class);
            return @class;
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ID == id);
        }
    }
}
