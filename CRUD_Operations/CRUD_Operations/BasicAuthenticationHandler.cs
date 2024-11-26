using CRUD_Operations.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ApplicationDbContext _context;
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ApplicationDbContext context) : base(options, logger, encoder, clock)
    {
        _context = context;
    }

    protected override async  Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        try
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var credentials = Encoding.UTF8
                .GetString(Convert.FromBase64String(authHeader.Substring("Basic ".Length)))
                .Split(':');
            var username = credentials[0];
            var password = credentials[1];


            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Name == username && u.Id == password);

            if (user != null)
            {
                Console.WriteLine($"User {username} authenticated successfully.");
                Claim[] claims = new[] { new Claim(ClaimTypes.Name, user.Name) }; // Use user.Name or another unique identifier
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                Console.WriteLine("Authentication failed: Invalid username or password.");
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}