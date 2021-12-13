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

        /*
         * Get People
         * 
         * Returns an array of all the people stored in the database
         * 
         */
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _context.People.ToArray();
        }

        /*
         * Get Birthdays in Months
         * 
         * Returns an array of months and number of birthdays in respective months
         * 
         */
        [HttpGet]
        [Route("birthdays")]
        public List<MonthBirthdays> GetBirthdays()
        {
            return _context.People.GroupBy(p => p.Date_of_Birth.Month).Select(group =>
            new MonthBirthdays(group.Key, group.Count())).ToList();
        }

        /*
         * Create Person
         * 
         * Accepts a qualified person object (sans-Id) and adds a new person with those
         *  details to the database.
         *  
         *  Returns the fully qualified person and the persons Id.
         * 
         */
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Add(person);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPerson), new { id = person.Id }, person);
        }

        /*
         * Delete Person
         * 
         * Accepts an existing person's Id and removes that person's
         *      details from the database.
         *  
         *  Returns no content or a 404-not-found error if no person
         *      with provided Id exists.
         * 
         */
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

        /*
         * Edit Person
         * 
         * Accepts a qualified person object (sans-Id) and updates
         *      an existing person with the new details provided.
         *  
         *  Returns no content (204) or a 400-bad-request error if no person
         *      with provided Id exists.
         * 
         */
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

        /*
         * (internal)
         * Person Exists
         * 
         * Checks if a person with the provided Id exists in the database.
         *  
         *  Returns a boolean response indicating the existence of the user in the database.
         * 
         */
        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }

}