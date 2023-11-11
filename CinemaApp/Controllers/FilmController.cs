using CinemaApp.Models;
using CinemaApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaApp.Controllers
{
	public class FilmController : Controller
	{
		private IWebHostEnvironment webHostEnvironment;

		public FilmController(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}

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

		public IActionResult Gallery() 
		{
			var picturesFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
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
				var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
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
			var path = Path.Combine(webHostEnvironment.WebRootPath, "images", name);
			if (Path.Exists(path))
				System.IO.File.Delete(path);
			return RedirectToAction("Gallery");
		}
	}
}
