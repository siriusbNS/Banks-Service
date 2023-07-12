namespace Banks.Entities;

public interface ICommand
{
    void Execute();
    void Undo();
    int GetId();
}