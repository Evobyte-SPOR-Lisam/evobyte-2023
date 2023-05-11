using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PeopleController : Controller
    {
        private static List<Person> people = new List<Person>();

        // GET: People
        public IActionResult Index()
        {
            return View(people);
        }

        // GET: People/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = people.FirstOrDefault(m => m.ID == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Person person)
        {
            if (ModelState.IsValid)
            {
                person.ID = Guid.NewGuid();
                people.Add(person);

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = people.FirstOrDefault(m => m.ID == id);

            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [FromForm] Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var personToUpdate = people.FirstOrDefault(m => m.ID == id);

                if (personToUpdate == null)
                {
                    return NotFound();
                }

                personToUpdate.Name = person.Name;

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = people.FirstOrDefault(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var person = people.FirstOrDefault(m => m.ID == id);
            if (person != null)
            {
                people.Remove(person);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
