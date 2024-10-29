namespace UserApi
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Make Name nullable
        public int HoursWorked { get; set; }
    }
}
