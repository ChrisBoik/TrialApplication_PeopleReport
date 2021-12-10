using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrialApplication_PeopleReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Person
            {
                First_Name = DateTime.Now.AddDays(index),
                Last_name = rng.Next(-20, 55),
                Date_of_Birth = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
