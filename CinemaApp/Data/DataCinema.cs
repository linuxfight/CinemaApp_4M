using CinemaApp.Models;

namespace CinemaApp.Data
{
	public static class DataCinema
	{
		public static List<Film> Films { get; set; }
        public static List<Person> Persons { get; set; }

        static DataCinema() 
		{
			Films = new List<Film>() 
			{
				new Film()
				{
					Name = "HP 1",
					Description = "new part of H P",
					Rating = 1.5f
				},
				new Film()
				{
					Name = "Shrack",
					Description = "Somebody once...",
					Rating = 10f
				}
			};
			Persons = new List<Person>()
			{
				new Person()
				{
					Name = "Ilya",
					Role = Role.Director
				},
				new Person()
				{
					Name = "Dany",
					Role = Role.Editor
				},
				new Person()
				{
					Name = "Vlad",
					Role = Role.Actor
				},
				new Person()
				{
					Name = "Boris",
					Role = Role.Actor
				}
			};
		}
	}
}
