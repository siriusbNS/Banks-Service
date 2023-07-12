namespace Banks.Entities;

public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserever(IObserver observer);
    void NotifyObservers();
}