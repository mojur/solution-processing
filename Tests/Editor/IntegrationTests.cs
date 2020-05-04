// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.Unity.SolutionProcessing.Internal;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void DanglingProcessorsAreDeleted()
        {
            WeakReference processorRef = null;
            var repo = Substitute.For<ISolutionSyncProcessorRepository>();
            repo.GetProcessors()
                .Returns(
                    call =>
                    {
                        var processor = new MockProcessor();
                        processorRef = processorRef ?? new WeakReference(processor);

                        return new[] { processor };
                    });
            var manager = new SolutionSyncPublishManager(new SolutionProcessorPublisherFactory(repo));
            manager.UpdateToNewPublisher();

            manager.UpdateToNewPublisher();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.That(processorRef.IsAlive, Is.False);
        }
    }
}
