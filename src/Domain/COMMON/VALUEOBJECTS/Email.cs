using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Common.ValueObjects;

public class Email : ValueObject
{
    static Email()
    {
    }

    private Email()
    {
    }

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; } = null!;

    public static Email From(string emailId)
    {
        if (!IsValid(emailId))
        {
            throw CustomException.WithBadRequest($"Invalid email id: {emailId}");
        }

        var @object = new Email(emailId);
        return @object;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValid(string value)
    {
        return new EmailAddressAttribute().IsValid(value);
    }
}
