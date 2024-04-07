namespace $safeprojectname$.Common.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        static PhoneNumber()
        {
        }

        private PhoneNumber()
        {
        }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public string Value { get; } = null!;

        public static PhoneNumber From(string phoneNumber)
        {
            if (!IsValid(phoneNumber))
            {
                throw CustomException.WithBadRequest($"Invalid phone number: {phoneNumber}");
            }

            var @object = new PhoneNumber(phoneNumber);
            return @object;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static bool IsValid(string value)
        {
            return ulong.TryParse(value, out var _) && value.Length >= 10;
        }
    }
}
