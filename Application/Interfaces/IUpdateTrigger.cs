namespace Application.Interfaces;

public interface IUpdateTrigger
{
    void SubscribeToUpdate(Action updateView);
}