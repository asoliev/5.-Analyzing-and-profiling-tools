// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{
    var iterate = 10000;
    const int hashSize = 20;  //Instead of hardcoded values - better for code readability and maintainability

    using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
    {
        var passwordHashBytes = salt.Concat(pbkdf2.GetBytes(hashSize)).ToArray();
        return Convert.ToBase64String(passwordHashBytes);
    }
}
string NewGeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{
    var iterate = 10000;
    byte[] hash;
    using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate))
    {
        hash = pbkdf2.GetBytes(20);
    }
    var hashBuilder = new StringBuilder();

    hashBuilder.Append(Convert.ToBase64String(salt));
    hashBuilder.Append(Convert.ToBase64String(hash));

    return hashBuilder.ToString();
}

string _pwdTxt = "Hello";
var _salt = new byte[16];
Stopwatch stopwatch = new Stopwatch();

stopwatch.Start();
string pwdHash = GeneratePasswordHashUsingSalt(_pwdTxt, _salt);
stopwatch.Stop();
Console.WriteLine("Old generator work time: {0}ms", stopwatch.ElapsedMilliseconds);
Console.WriteLine(pwdHash);
Console.WriteLine();

stopwatch.Restart();
pwdHash = NewGeneratePasswordHashUsingSalt(_pwdTxt, _salt);
stopwatch.Stop();
Console.WriteLine("New generator work time: {0}ms", stopwatch.ElapsedMilliseconds);
Console.WriteLine(pwdHash);

Console.ReadKey();