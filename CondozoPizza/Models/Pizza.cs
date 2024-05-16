namespace CondozoPizza.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
