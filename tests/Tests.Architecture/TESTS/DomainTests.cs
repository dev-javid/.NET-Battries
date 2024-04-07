using System.Reflection;
using NetArchTest.Rules;

namespace $safeprojectname$.Tests
{
    public class DomainTests
    {
        [Fact]
        public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Domain);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.WebHost,
                ProjectNames.Application,
                ProjectNames.Infrastructure,
                ProjectNames.Presentation,
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(inaAccessibleProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
