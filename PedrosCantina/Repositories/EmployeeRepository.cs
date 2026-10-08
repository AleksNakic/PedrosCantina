using PedrosCantina.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PedrosCantina.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Opretter en ny medarbejder i databasen
        public void Create(Employee employee)
        {
            string sql = @" INSERT INTO Medarbejder (Navn, Email, Telefon, Rolle)
            VALUES (@Navn, @Email, @Telefon, @Rolle)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Navn", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Telefon", employee.Phone);
                    command.Parameters.AddWithValue("@Rolle", employee.Role);

                    command.ExecuteNonQuery();

                }
            }
        }

        // Henter alle medarbejdere fra databasen
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            string sql = @"
        SELECT MedarbejderID, Navn, Email, Telefon, Rolle
        FROM Medarbejder";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Role = reader.GetString(4)
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        // Henter en medarbejder fra databasen
        public Employee GetById(int employeeId)
        {
            string sql = @"
                SELECT MedarbejderID, Navn, Email, Telefon, Rolle
                FROM Medarbejder
                WHERE MedarbejderID = @MedarbejderID";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MedarbejderID", employeeId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Role = reader.GetString(4)
                            };
                        }
                    }
                }
            }
            return null;
        }
        //Opdaterer en eksisterende medarbejder 
        public void Update(Employee employee)
        {
            string sql = @" 
                UPDATE Medarbejder 
                SET Navn = @Navn, 
                    Email = @Email, 
                    Telefon = @Telefon, 
                    Rolle = @Rolle 
                WHERE MedarbejderID = @MedarbejderID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Navn", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Telefon", employee.Phone);
                    command.Parameters.AddWithValue("@Rolle", employee.Role);
                    command.Parameters.AddWithValue("@MedarbejderID", employee.EmployeeId);
                    command.ExecuteNonQuery();
                }
            }
        }
        // Sletter en medarbejder ud fra ID
        public void Delete(int employeeId)
        {
            string sql = @" 
                DELETE FROM Medarbejder 
                WHERE MedarbejderID = @MedarbejderID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MedarbejderID", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}