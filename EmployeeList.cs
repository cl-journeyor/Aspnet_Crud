using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Aspnet_Crud;

public static class EmployeeList
{
    private record FetchPageResponse(Employee[] Employees);

    public sealed class InsertEmployeeRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("role")]
        public string Role { get; set; } = "";
            
        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }
    }

    private static IResult Ok() => Results.Ok("OK");

    private static IResult InternalServerError(Exception cause)
    {
        Console.WriteLine(cause.Message);
        Console.WriteLine(cause.StackTrace);
        return Results.InternalServerError("Unexpected error");
    }

    public static object FetchPage(string page = "")
    {
        int pageNumber = Validators.GetInt(page, 1);
        int pageSize = 10;

        using CompanyContext ctx = new();

        try
        {
            return Json.Serialize(new FetchPageResponse(
                ctx.employees
                    .Where(e => e.id > (pageNumber - 1) * pageSize)
                    .OrderBy(e => e.id)
                    .Take(pageSize)
                    .ToArray()
            ));
        }
        catch (Exception e)
        {
            return InternalServerError(e);
        }
    }

    public static IResult InsertEmployee(InsertEmployeeRequest req)
    {
        Employee employee = new()
        {
            name = req.Name,
            role = req.Role,
            salary = req.Salary,
            added_date = DateOnly.FromDateTime(DateTime.Now)
        };

        using CompanyContext ctx = new();

        ctx.employees.Add(employee);

        try
        {
            ctx.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return InternalServerError(e);
        }
    }

    public static IResult UpdateEmployee(Employee updated)
    {
        using CompanyContext ctx = new();

        ctx.employees.Update(updated);

        try
        {
            ctx.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return InternalServerError(e);
        }
    }

    public static IResult DeleteEmployee([FromBody] Employee employee)
    {
        using CompanyContext ctx = new();

        ctx.employees.Remove(employee);

        try
        {
            ctx.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return InternalServerError(e);
        }
    }
}
