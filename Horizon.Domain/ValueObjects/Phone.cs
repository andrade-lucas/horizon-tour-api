namespace Horizon.Domain.ValueObjects;

public class Phone
{
    public string Number { get; private set; }

    public Phone(string number)
    {
        Number = number;
    }
}
