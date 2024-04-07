namespace $safeprojectname$.Common
{
    public static class Extensions
    {
        public static Email ToEmail(this string @string)
        {
            return Email.From(@string);
        }

        public static PhoneNumber ToPhoneNumber(this string @string)
        {
            return PhoneNumber.From(@string);
        }

        public static FileName ToFileName(this string @string)
        {
            return FileName.From(@string);
        }
    }
}
