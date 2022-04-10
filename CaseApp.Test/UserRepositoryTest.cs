using CaseApp.DataLayer.Concrete;
using CaseApp.Entities;
using Newtonsoft.Json;
using Xunit;

namespace CaseApp.Test
{
    public class UserRepositoryTest
    {
        [Fact]
        public void FindUserTest()
        {
            //Arrange
            string userName = "admin";
            var findUser = new UserRepository();
            User user = new User()
            {
                Id = 1,
                Username = "admin",
                Password = "password"
            };

            // Act
            var result = findUser.FindUser(userName);

            //Assert
            var userJson = JsonConvert.SerializeObject(user);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.Equal(userJson, resultJson);

        }
    }
}
