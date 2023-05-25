using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private static List<Person> people = new List<Person>()
        {
            new Person() { ID = Guid.NewGuid(), Name = "Gica", DateOfBirth = DateTime.Now },
            new Person() { ID = Guid.NewGuid(), Name = "Aurel", DateOfBirth = DateTime.Now },
            new Person() { ID = Guid.NewGuid(), Name = "Maria", DateOfBirth = DateTime.Now },
        };

        // GET: api/<PeopleController>
        [HttpGet]
        public Person[] Get()
        {
            return people.ToArray();
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public Person Get(Guid id)
        {
            return people.FirstOrDefault(x => x.ID == id);
        }

        // POST api/<PeopleController>
        [HttpPost]
        public void Post([FromBody] Person person)
        {
            people.Add(person);
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
