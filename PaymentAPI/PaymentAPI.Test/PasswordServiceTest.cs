using PaymentAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.Test
{
    public class PasswordServiceTest
    {
        [Fact]
        public void VerifyPasswordHash_WithMatchingHash_ReturnsTrue()
        {
            // Arrange
            var passwordService = new PasswordService();
            var password = "testPassword";
            // Hash hesaplama
            var hashedPassword = passwordService.CreateMD5Hash(password);

            // Act
            // Doğrudan VerifyPasswordHash metodunu çağırıyoruz
            var result = passwordService.VerifyPasswordHash(password, hashedPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_WithNonMatchingHash_ReturnsFalse()
        {
            // Arrange
            var passwordService = new PasswordService();
            var password = "testPassword";
            var wrongHash = "WrongMD5HashValue";

            // Act
            var result = passwordService.VerifyPasswordHash(password, wrongHash);

            // Assert
            Assert.False(result);
        }
    }
}
