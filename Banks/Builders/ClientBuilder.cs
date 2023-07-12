namespace Banks.Entities;

public class ClientBuilder
{
    public ClientBuilder()
    {
        Name = null;
        Surname = null;
        Address = null;
        NumberOfPassport = null;
    }

    private string Name { get; set; } = null;
    private string Surname { get; set; } = null;
    private string Address { get; set; }
    private int? NumberOfPassport { get; set; }
    public ClientBuilder AddNameAndSurname(string name, string surname)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);
        Name = name;
        Surname = surname;
        return this;
    }

    public ClientBuilder AddAdress(string adress)
    {
        ArgumentNullException.ThrowIfNull(adress);
        Address = adress;
        return this;
    }

    public ClientBuilder AddPassport(int passport)
    {
        ArgumentNullException.ThrowIfNull(passport);
        NumberOfPassport = passport;
        return this;
    }

    public Client Build()
    {
        return new Client(
            Name ?? throw new Exception(),
            Surname ?? throw new Exception(),
            Address,
            NumberOfPassport);
    }
}