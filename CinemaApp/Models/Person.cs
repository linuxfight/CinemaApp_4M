namespace CinemaApp.Models
{
	public enum Role
	{
		Director,
		Actor,
		Editor
	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Role Role { get; set; }
		public List<Film> Films = new List<Film>();

		private static int count = 0;

		public Person()
		{
			count++;
			Id = count;
		}
	}
}
