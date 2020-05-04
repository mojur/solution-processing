// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.Unity.SolutionProcessing.Internal;
using NUnit.Framework;
using System.Reflection;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestFixture]
    [TestOf(typeof(OmniscientSolutionProcessorRepository))]
    public class OmniscientSolutionProcessorRepositoryTests
    {
        [Test]
        public void FindsOnlyValidSolutionProcessorTypes()
        {
            var typesFound =
                OmniscientSolutionProcessorRepository.FindProcessorTypesWithin(
                    new[] { Assembly.GetExecutingAssembly() });

            Assert.That(typesFound, Is.EquivalentTo(new[] { typeof(MockProcessor) }));
        }

        [Test]
        public void ReturnsNonNullSolutionProcessors()
        {
            var repo = new OmniscientSolutionProcessorRepository(new[] { Assembly.GetExecutingAssembly() });
            var processors = repo.GetProcessors();

            Assert.That(processors, Has.All.Not.Null);
        }
    }
}
