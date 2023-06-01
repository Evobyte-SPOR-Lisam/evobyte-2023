using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PeopleController : ControllerBase
	{
		private readonly WebApplication1Context dbContext;

		public PeopleController(WebApplication1Context dbContext)
		{
			this.dbContext = dbContext;
		}

		// GET: api/People
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
		{
			if (dbContext.Person == null)
			{
				return NotFound();
			}
			return await dbContext.Person
				.OrderByDescending(x => x.Name)
				.ToListAsync();
		}

		// GET: api/People/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Person>> GetPerson(Guid id)
		{
			if (dbContext.Person == null)
			{
				return NotFound();
			}
			var person = await dbContext.Person.FindAsync(id);

			if (person == null)
			{
				return NotFound();
			}

			return person;
		}

		// PUT: api/People/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPerson(Guid id, Person person)
		{
			if (id != person.ID2)
			{
				return BadRequest();
			}

			dbContext.Entry(person).State = EntityState.Modified;

			try
			{
				await dbContext.SaveChangesAsync();
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

		// POST: api/People
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Person>> PostPerson(Person person)
		{
			if (dbContext.Person == null)
			{
				return Problem("Entity set 'WebApplication1Context.Person'  is null.");
			}
			dbContext.Person.Add(person);
			await dbContext.SaveChangesAsync();

			return CreatedAtAction("GetPerson", new { id = person.ID2 }, person);
		}

		// DELETE: api/People/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePerson(Guid id)
		{
			if (dbContext.Person == null)
			{
				return NotFound();
			}
			var person = await dbContext.Person.FindAsync(id);
			if (person == null)
			{
				return NotFound();
			}

			dbContext.Person.Remove(person);
			await dbContext.SaveChangesAsync();

			return NoContent();
		}

		private bool PersonExists(Guid id)
		{
			return (dbContext.Person?.Any(e => e.ID2 == id)).GetValueOrDefault();
		}
	}
}
