using System.Reflection;
using NetArchTest.Rules;

namespace $safeprojectname$.Tests
{
    public class InfrastructureTests
    {
        [Fact]
        public void Infrastructure_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Infrastructure);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.WebHost,
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
