using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TrialApplication_PeopleReport.AppDbContext;
using TrialApplication_PeopleReport.Models;

namespace TrialApplication_PeopleReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _context;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, PersonContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _context.People.ToArray();
        }

        [HttpGet]
        [Route("birthdays")]
        public List<MonthBirthdays> GetBirthdays()
        {
            return _context.People.GroupBy(p => p.Date_of_Birth.Month).Select(group =>
            new MonthBirthdays(group.Key, group.Count())).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Add(person);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPerson), new { id = person.Id }, person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }

}