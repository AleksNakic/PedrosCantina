using Microsoft.Data.SqlClient;
using PedrosCantina.Models;
using PedrosCantina.Repositories;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();
string connectionString = configuration["AleksConnectionString"];

EmployeeRepository repository = new EmployeeRepository(connectionString);


Console.WriteLine("=== Medarbejder ===");
Console.WriteLine();

//Hent alle medarbejder 
List<Employee> employees = repository.GetAll();

foreach (Employee employee in employees)
{
    Console.WriteLine(
        employee.EmployeeId + " - " +
        employee.Name + " - " +
        employee.Email + " - " +
        employee.Phone + " - " +
        employee.Role);
}
//Opret en ny medarbejder 
Employee newEmployee = new Employee
{
    Name = "Test Person",
    Email = "test@cantina.dk",
    Phone = "12345678",
    Role = "Medarbejder"
};

repository.Create(newEmployee);

Console.WriteLine();
Console.WriteLine("Ny medarbejder er oprettet");

//Opdater en medarbejder 
Employee employeeToUpdate = repository.GetById(3);

if (employeeToUpdate != null)
{
    employeeToUpdate.Phone = "99999999";

    repository.Update(employeeToUpdate);

    Console.WriteLine("Medarbejder er opdateret.");
}

// Slet en medarbejder
repository.Delete(7);
Console.WriteLine("Medarbejder er slettet");

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    // Vis en månedsplan
    Console.WriteLine();
    Console.WriteLine("=== Månedsplan ===");

    string monthlyScheduleSql = @"
    SELECT
        Vagt.Dato,
        Vagt.StartTid,
        Vagt.SlutTid,
        Medarbejder.Navn,
        Medarbejder.Rolle
    FROM Vagt
    INNER JOIN VagtMedarbejder
        ON Vagt.VagtID = VagtMedarbejder.VagtID
    INNER JOIN Medarbejder
        ON VagtMedarbejder.MedarbejderId = Medarbejder.MedarbejderID
    ORDER BY Vagt.Dato, Vagt.StartTid";

    using (SqlCommand command = new SqlCommand(monthlyScheduleSql, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(
                    reader.GetDateTime(0).ToShortDateString() + " - " +
                    reader.GetTimeSpan(1) + " - " +
                    reader.GetTimeSpan(2) + " - " +
                    reader.GetString(3) + " - " +
                    reader.GetString(4));
            }
        }
    }


    // Vis belastning for medarbejdere i en måned
    Console.WriteLine();
    Console.WriteLine("=== Belastning for oktober 2026 ===");

    string monthlyWorkloadSql = @"
    SELECT
        Medarbejder.Navn,
        COUNT(Vagt.VagtID) AS AntalVagter
    FROM Medarbejder
    INNER JOIN VagtMedarbejder
        ON Medarbejder.MedarbejderID = VagtMedarbejder.MedarbejderID
    INNER JOIN Vagt
        ON VagtMedarbejder.VagtID = Vagt.VagtID
    INNER JOIN Månedsplan
        ON Vagt.MånedsplanID = Månedsplan.MånedsplanID
    WHERE Månedsplan.År = 2026
    AND Månedsplan.Måned = 10
    GROUP BY Medarbejder.Navn
    ORDER BY AntalVagter DESC";

    using (SqlCommand command = new SqlCommand(monthlyWorkloadSql, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(
                    reader.GetString(0) + " - " +
                    reader.GetInt32(1) + " vagter");
            }
        }
    }


    // Vis kontaktinformation
    Console.WriteLine();
    Console.WriteLine("=== Kontaktinformation ===");

    string contactSql = @"
    SELECT
        Navn,
        Email,
        Telefon
    FROM Medarbejder
    WHERE Navn <> 'Anna'";

    using (SqlCommand command = new SqlCommand(contactSql, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(
                    reader.GetString(0) + " - " +
                    reader.GetString(1) + " - " +
                    reader.GetString(2));
            }
        }
    }


    // Vis belastning hen over året
    Console.WriteLine();
    Console.WriteLine("=== Belastning i 2026 ===");

    string yearlyWorkloadSql = @"
    SELECT
        Medarbejder.Navn,
        COUNT(Vagt.VagtID) AS AntalVagter
    FROM Medarbejder
    INNER JOIN VagtMedarbejder
        ON Medarbejder.MedarbejderID = VagtMedarbejder.MedarbejderID
    INNER JOIN Vagt
        ON VagtMedarbejder.VagtID = Vagt.VagtID
    INNER JOIN Månedsplan
        ON Vagt.MånedsplanID = Månedsplan.MånedsplanID
    WHERE Månedsplan.År = 2026
    GROUP BY Medarbejder.Navn
    ORDER BY AntalVagter DESC";

    using (SqlCommand command = new SqlCommand(yearlyWorkloadSql, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(
                    reader.GetString(0) + " - " +
                    reader.GetInt32(1) + " vagter");
            }
        }
    }
    // Opret en ny månedsplan
    Console.WriteLine();
    Console.WriteLine("=== Ny månedsplan ===");

    string createMonthlyPlanSql = @"
    INSERT INTO Månedsplan (År, Måned)
    VALUES (2026, 11)";

    using (SqlCommand command = new SqlCommand(createMonthlyPlanSql, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("Ny månedsplan for november 2026 er oprettet.");
    }
    // Juster en eksisterende vagt
    Console.WriteLine();
    Console.WriteLine("=== Justering af vagt ===");

    string updateShiftSql = @"
    UPDATE Vagt
    SET StartTid = '10:00',
        SlutTid = '15:00'
    WHERE VagtID = 3";

    using (SqlCommand command = new SqlCommand(updateShiftSql, connection))
    {
        command.ExecuteNonQuery();
        Console.WriteLine("Vagt 3 er ændret til 10:00 - 15:00.");
    }
    // Vis den ændrede vagt
    string showChangedShiftSql = @"
    SELECT *
    FROM Vagt
    WHERE VagtID = 3";

    using (SqlCommand command = new SqlCommand(showChangedShiftSql, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(
                    "VagtID: " + reader.GetInt32(0) +
                    " | MånedsplanID: " + reader.GetInt32(1) +
                    " | Dato: " + reader.GetDateTime(2).ToShortDateString() +
                    " | Start: " + reader.GetTimeSpan(3) +
                    " | Slut: " + reader.GetTimeSpan(4));
            }
        }
    }

}