using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MoviesApi.UnitTests;

public class UnitTest1
{
    [Fact]
    public void TestJWTDeserialization()
    {
        var token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImlzcyI6InNldm8iLCJuZXciOiJhZGFzIn0.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.jAjSBuXG2bKMe_K91KwXuLxrUbyvG0rsjxB_YvGDWLE";
        var result = Utils.ValidateJwtToken(token);


        var z = "";
    }
}