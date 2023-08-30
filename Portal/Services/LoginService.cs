using Dapper;
using Microsoft.Data.SqlClient;
using Portal.Model;
using ExtensionLoginService = Portal.Extension.ExtensionLoginService;

namespace Portal.Services
{
    public class LoginService : ILoginService
    {
        public async Task<UserDetails> Authenticate(Login login)
        {
            using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Portal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();
            return await connection.QuerySingleAsync<UserDetails>("SELECT FIRSTNAME as Firstname, LASTNAME as Lastname, EMAIL as Email from dbo.Users where email = @Email and password = @Password", new { login.Email, Password = ExtensionLoginService.EncodePasswordToBase64(login.Password ?? string.Empty) });
        }

        public async Task SignUp(SignUp userDetails)
        {
            using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Portal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();
            var sql = "INSERT INTO Users (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, @Password)";
            var anonymousCustomer = new { FirstName = userDetails.Firstname, LastName = userDetails.Lastname, userDetails.Email, Password = ExtensionLoginService.EncodePasswordToBase64(userDetails.Password) };
            await connection.ExecuteAsync(sql, anonymousCustomer);
        }
    }
}
