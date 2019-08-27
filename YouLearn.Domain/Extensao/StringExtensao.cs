using System.Text;

namespace YouLearn.Domain.Extensao
{
    public static class StringExtensao
    {
        public static string ConvertToMD5(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            var senha = (text += "|2d331cca-f6c0-40c0-bb43-6e32989c2886fd72-fb42d209-f576ca23");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(senha));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
