namespace Application.Services.UseCases.Utils
{
    public interface IDelete <out TO>
    {
        TO Execute(int id);
    }
}