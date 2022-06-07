namespace Blog.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullOrEmpty(string str) // rekao bih da fali 'this' pre 'string'
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
