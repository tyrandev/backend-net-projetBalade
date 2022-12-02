namespace Application.UseCases.Utils
{
    public interface IAuthenticate<out TO>
    {
        TO Execute(string username, string password);
    }
}