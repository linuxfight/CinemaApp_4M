using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaApp.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View(DataCinema.Persons);
        }

        public IActionResult ConfirmDelete(int id)
        {
            var person = DataCinema.Persons.FirstOrDefault(x => x.Id == id);
            return View(person);
        }

        public IActionResult Delete(int id)
        {
            var person = DataCinema.Persons.FirstOrDefault(x => x.Id == id);
            DataCinema.Persons.Remove(person);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var person = DataCinema.Persons.FirstOrDefault(x => x.Id == id);
            return View(person);
        }

        public IActionResult Update(Person person)
        {
            var personToChange = DataCinema.Persons.FirstOrDefault(x => x.Id == person.Id);
            personToChange.Name = person.Name;
            personToChange.Role = person.Role;
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            var person = new Person();
            return View(person);
        }

        public IActionResult AddToList(Person person)
        {
            DataCinema.Persons.Add(person);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddFilmToPerson()
        {
            var films = new List<SelectListItem>();
            var persons = new List<SelectListItem>();
            foreach (var person in DataCinema.Persons)
                persons.Add(new SelectListItem(person.Name, person.Id.ToString()));
            foreach (var film in DataCinema.Films)
                films.Add(new SelectListItem(film.Name, film.Id.ToString()));

            var viewModel = new PersonFilmViewModel()
            {
                Films = films,
                Persons = persons
            };

			return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddFilmToPerson(PersonFilmViewModel viewModel)
        {
            var idPerson = viewModel.PersonId;
            var idFilm = viewModel.FilmId;
            var person = DataCinema.Persons.FirstOrDefault(x => x.Id == idPerson);
            var film = DataCinema.Films.FirstOrDefault(x => x.Id == idFilm);
            person.Films.Add(film);
            film.Persons.Add(person);

            return RedirectToAction("LinkedCinema");
        }

        public IActionResult LinkedCinema()
        {
            return View(DataCinema.Persons);
        }
    }
}
