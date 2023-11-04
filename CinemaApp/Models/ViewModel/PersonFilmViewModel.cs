using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaApp.Models.ViewModel
{
    public class PersonFilmViewModel
    {
        public int PersonId { get; set; }
        public int FilmId { get; set; }

        public List<SelectListItem> Persons { get; set; }
        public List<SelectListItem> Films { get; set; }
    }
}
