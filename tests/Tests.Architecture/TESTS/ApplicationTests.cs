using System.Reflection;
using NetArchTest.Rules;

namespace $safeprojectname$.Tests
{
    public class ApplicationTests
    {
        [Fact]
        public void Application_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Application);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.WebHost,
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
