namespace Interfaces.Models
{
    public class Person : Base
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"{Id} {FullName} {Description} {IsDeleted}";
        }
    }
        

}
