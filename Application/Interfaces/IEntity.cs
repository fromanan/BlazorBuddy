namespace Application.Interfaces;

public interface IEntity<out T>
{
    public int Id { get; set; }

    public T Copy();
}