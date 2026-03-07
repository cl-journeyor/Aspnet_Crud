namespace Aspnet_Crud;

public static class Validators
{
    public static int GetInt(string raw, int defaul)
    {
        bool success = int.TryParse(raw, out int parsed);

        return success ? parsed : defaul;
    }
}
