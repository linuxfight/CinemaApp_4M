using CinemaApp.Data;
using CinemaApp.Models;
using CinemaApp.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaApp.Controllers
{
    public class PersonController : Controller
    {
		private IWebHostEnvironment webHostEnvironment;

		public PersonController(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}

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

		public IActionResult Gallery()
		{
			var picturesFolder = Path.Combine(webHostEnvironment.WebRootPath, "actors");
			var photos = Directory.GetFiles(picturesFolder);
			return View(photos);
		}

		public IActionResult UploadPicture()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Upload(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				var extension = Path.GetExtension(file.FileName);
				var fileName = Guid.NewGuid().ToString() + extension;
				var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "actors");
				var path = Path.Combine(uploadFolder, fileName);
				var stream = new FileStream(path, FileMode.Create);
				file.CopyTo(stream);
				stream.Close();
			}

			return RedirectToAction("UploadPicture");
		}

		[HttpPost]
		public IActionResult DeleteImage(string name)
		{
			var path = Path.Combine(webHostEnvironment.WebRootPath, "actors", name);
			if (Path.Exists(path))
				System.IO.File.Delete(path);
			return RedirectToAction("Gallery");
		}
	}
}
