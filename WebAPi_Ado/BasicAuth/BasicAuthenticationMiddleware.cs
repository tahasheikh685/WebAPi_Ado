using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

public class BasicAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            // Extract credentials from the Authorization header
            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
            byte[] decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
            string decodedUsernamePassword = Encoding.UTF8.GetString(decodedBytes);

            // Split the username and password
            string[] usernamePasswordArray = decodedUsernamePassword.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            // Replace the following with your actual username and hashed password
            string storedUsername = "Admin";
            string storedHashedPassword = "Pass";

            // Compare the provided credentials with the stored username and hashed password
            if (username == storedUsername && BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
            {
                await _next.Invoke(context);
                return;
            }
        }

        // If credentials are not valid, return 401 Unauthorized
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = 401;
    }
}
