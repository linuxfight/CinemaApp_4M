namespace CinemaApp.Models
{
	public class Film
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public float Rating { get; set; }
		public List<Person> Persons = new List<Person>();
		private static int count = 0;

		public Film() 
		{
			count++;
			Id = count;
		}
	}
}
