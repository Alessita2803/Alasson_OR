namespace Alasson.Models
{
    public class Employee
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Charge { get; set; }
        public float Salary { get; set; }

        public Employee(string fullName, string email, string charge, float salary)
        {
            FullName = fullName;
            Email = email;
            Charge = charge;
            Salary = salary;
        }
    }
}
