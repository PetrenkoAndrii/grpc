using AutoMapper;
using GrpcService.MappingProfiles;

namespace GrpcServiceTests.MappingProfiles;

[TestClass]
public class UserProfileTests
{
    [TestMethod]
    public void User_ShouldValidateMappingProfile()
    {
        // Arrange
        var config = new MapperConfiguration(configure =>
        {
            configure.AddProfile(new UserProfile());
        });
        var mapper = config.CreateMapper();

        // Act
        // Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
