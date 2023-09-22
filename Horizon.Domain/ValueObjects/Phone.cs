namespace Horizon.Domain.ValueObjects;

public class Phone
{
    public string Number { get; private set; }

    public Phone(string number)
    {
        Number = RemoveSpecialCharacters(number);
    }

    private string RemoveSpecialCharacters(string value)
    {
        string newValue = value
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace(" ", "")
            .Trim();

        return newValue;
    }
}
