using System.Text.Json;

namespace Aspnet_Crud;

public static class EmployeeList
{
    private record FetchPageResponse(Employee[] Employees, bool Success);

    public static string FetchPage(string page = "")
    {
        int pageNumber = Validators.GetInt(page, 1);
        int pageSize = 10;

        using (CompanyContext ctx = new())
        {
            try
            {
                return JsonSerializer.Serialize(new FetchPageResponse(
                    ctx.employees
                        .Where(e => e.id > (pageNumber - 1) * pageSize)
                        .OrderBy(e => e.id)
                        .Take(pageSize)
                        .ToArray(),
                    true
                ));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return JsonSerializer.Serialize(new FetchPageResponse([], false));
            }
        }
    }
}
