

namespace PedrosCantina.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone  { get; set; }
        public string Role { get; set; } 

        public Employee()
        {
        }

    public Employee(int employeeId, string name, string email, string phone, string role)
        {
            EmployeeId = employeeId;
            Name = name;
            Email = email;
            Phone = phone;
            Role = role;
        }
        public override string ToString()
        {
            return $"EmployeeId: {EmployeeId}, Name: {Name}, Email: {Email}, Phone: {Phone}, Role: {Role}"; 
        }
    }
}
