namespace Banks.Entities;

public class TimeManager : IObservable
{
    public TimeManager()
    {
        DateTime = DateTime.Now;
    }

    private DateTime DateTime { get; set; }
    private IObserver Observer { get; set; }

    public void AddObserver(IObserver observer)
    {
        Observer = observer;
    }

    public void RemoveObserever(IObserver observer)
    {
        Observer = null;
    }

    public void NotifyObservers()
    {
        Observer.Update(DateTime);
    }

    public void PlusDays(int value)
    {
        DateTime.AddDays(value);
        Observer.Update(DateTime);
    }

    public void PlusMonths(int value)
    {
        DateTime = DateTime.AddMonths(value);
        Observer.Update(DateTime);
    }

    public void PlusYears(int value)
    {
        DateTime.AddYears(value);
        Observer.Update(DateTime);
    }
}