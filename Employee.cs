namespace Aspnet_Crud;

public sealed class Employee
{
    public int id { get; set; }
    public string name { get; set; } = "";
    public string role { get; set; } = "";
    public decimal salary { get; set; }
    public DateOnly added_date { get; set; }
}
