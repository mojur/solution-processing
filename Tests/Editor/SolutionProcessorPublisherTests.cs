// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.Unity.SolutionProcessing.Internal;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using System.Linq;
using Does = Mojur.NUnitExtensions.Does;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestOf(typeof(SolutionProcessorPublisher))]
    public class SolutionSyncProcessorPublisherTests : SolutionSyncPublisherTests
    {
        private const string SomeModifiedContent = "Some modified content";

        private SolutionProcessorPublisher publisher;

        public SolutionSyncProcessorPublisherTests(SyncEventsTestArgs testArgs) : base(testArgs)
        {
        }

        [SetUp]
        public void SetPublisher()
        {
            this.publisher = new SolutionProcessorPublisher(Enumerable.Empty<ISolutionSyncProcessor>());
        }

        [Test]
        public void RegistersProcessors()
        {
            var mockProcessors = new[]
            {
                Substitute.For<ISolutionSyncProcessor>(), Substitute.For<ISolutionSyncProcessor>()
            };

            this.publisher = new SolutionProcessorPublisher(mockProcessors);

            foreach (var processor in mockProcessors)
            {
                processor.Received().RegisterSubscriptions(this.publisher);
            }
        }

        [Test]
        public override void PublishesSolutionSyncing()
        {
            Assert.That(
                () => this.publisher.PublishSolutionSyncing(),
                Does.RaiseWith<ISolutionSyncStatus>(_ => this.publisher.SolutionSyncing += _)
                    .WithArgWhere(status => status.AbortSync, Is.False));
        }

        [Test]
        public override void PublishesSlnGenerated()
        {
            var fileArg = new GeneratedFile(new FileInfo(this.testArgs.SlnPath), this.testArgs.SlnContent);

            Assert.That(
                () => this.publisher.PublishSlnGenerated(this.testArgs.SlnPath, this.testArgs.SlnContent),
                Does.RaiseWith<IGeneratedFile>(_ => this.publisher.SlnGenerated += _)
                    .WithArgThat(Is.EqualTo(fileArg).Using(new GeneratedFileEqualityComparer())));
        }

        [Test]
        public override void PublishesCsprojGenerated()
        {
            var fileArg = new GeneratedFile(new FileInfo(this.testArgs.CsprojPath), this.testArgs.CsprojContent);

            Assert.That(
                () => this.publisher.PublishCsprojGenerated(this.testArgs.CsprojPath, this.testArgs.CsprojContent),
                Does.RaiseWith<IGeneratedFile>(_ => this.publisher.CsprojGenerated += _)
                    .WithArgThat(Is.EqualTo(fileArg).Using(new GeneratedFileEqualityComparer())));
        }

        [Test]
        public override void PublishesSolutionSynced()
        {
            Assert.That(
                () => this.publisher.PublishSolutionSynced(),
                Does.Raise(_ => this.publisher.SolutionSynced += _));
        }

        [Test]
        public void PublishSolutionSyncingReturnsUpdatedStatus()
        {
            bool returnedAbortStatus = false;
            void AbortSync(ISolutionSyncStatus status) => status.AbortSync = true;

            Assume.That(
                () => returnedAbortStatus = this.publisher.PublishSolutionSyncing(),
                Does.RaiseWith<ISolutionSyncStatus>(_ => this.publisher.SolutionSyncing += _ + AbortSync));
            Assert.That(returnedAbortStatus, Is.True);
        }

        [Test]
        public void PublishSlnGeneratedReturnsModifiedContent()
        {
            string returnedContent = null;

            Assume.That(
                () => returnedContent = this.publisher.PublishSlnGenerated(
                    this.testArgs.SlnPath,
                    this.testArgs.SlnContent),
                Does.RaiseWith<IGeneratedFile>(_ => this.publisher.SlnGenerated += _ + ModifyContent));
            Assert.That(returnedContent, Is.EqualTo(SomeModifiedContent));
        }

        [Test]
        public void PublishCsprojGeneratedReturnsModifiedContent()
        {
            string returnedContent = null;

            Assume.That(
                () => returnedContent = this.publisher.PublishCsprojGenerated(
                    this.testArgs.CsprojPath,
                    this.testArgs.CsprojContent),
                Does.RaiseWith<IGeneratedFile>(_ => this.publisher.CsprojGenerated += _ + ModifyContent));
            Assert.That(returnedContent, Is.EqualTo(SomeModifiedContent));
        }

        private static void ModifyContent(IGeneratedFile file)
        {
            file.Content = SomeModifiedContent;
        }
    }
}
