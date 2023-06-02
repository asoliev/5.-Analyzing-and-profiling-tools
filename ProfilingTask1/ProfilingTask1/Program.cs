// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{
    var iterate = 10000;
    var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
    byte[] hash = pbkdf2.GetBytes(20);
    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 20);
    var passwordHash = Convert.ToBase64String(hashBytes);
    return passwordHash;
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