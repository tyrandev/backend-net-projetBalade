namespace Application.UseCases.Utils
{
    public interface IUpdate<out TO, in TI>
    {
        TO Execute(int id, TI dto);
    }
}