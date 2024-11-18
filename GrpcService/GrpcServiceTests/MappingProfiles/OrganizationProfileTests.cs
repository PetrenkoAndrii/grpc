using AutoMapper;
using GrpcService.MappingProfiles;

namespace GrpcServiceTests.MappingProfiles;

[TestClass]
public class OrganizationProfileTests
{
    [TestMethod]
    public void Organization_ShouldValidateMappingProfile()
    {
        // Arrange
        var config = new MapperConfiguration(configure =>
        {
            configure.AddProfile(new OrganizationProfile());
        });
        var mapper = config.CreateMapper();

        // Act
        // Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
