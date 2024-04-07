using System.Reflection;
using NetArchTest.Rules;

namespace $safeprojectname$.Tests
{
    public class PresentationTests
    {
        [Fact]
        public void Presentation_Should_Not_Have_Dependency_On_Other_Projects()
        {
            // Arrange
            var assembly = Assembly.Load(ProjectNames.Presentation);
            var inaAccessibleProjects = new[]
            {
                ProjectNames.WebHost,
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
