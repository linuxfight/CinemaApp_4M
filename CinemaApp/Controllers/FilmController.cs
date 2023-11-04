using CinemaApp.Models;
using CinemaApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaApp.Controllers
{
	public class FilmController : Controller
	{
		public IActionResult Index()
		{
			return View(DataCinema.Films);
		}

		public IActionResult ConfirmDelete(int id)
		{
			var film = DataCinema.Films.FirstOrDefault(x => x.Id == id);
			return View(film);
		}

		public IActionResult Delete(int id)
		{
            var film = DataCinema.Films.FirstOrDefault(x => x.Id == id);
			DataCinema.Films.Remove(film);
            return RedirectToAction("Index");
		}

		public IActionResult Edit(int id) 
		{
            var film = DataCinema.Films.FirstOrDefault(x => x.Id == id);
            return View(film);
		}

		public IActionResult Update(Film film)
		{
			var filmToChange = DataCinema.Films.FirstOrDefault(x => x.Id == film.Id);
			filmToChange.Name = film.Name;
			filmToChange.Description = film.Description;
			filmToChange.Rating = film.Rating;
            return RedirectToAction("Index");
        }

		public IActionResult Add()
		{
			var film = new Film();
			return View(film);
		}

		public IActionResult AddToList(Film film)
		{
			DataCinema.Films.Add(film);
            return RedirectToAction("Index");
        }
	}
}
