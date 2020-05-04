// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.Unity.SolutionProcessing.Internal;
using NSubstitute;
using NUnit.Framework;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestOf(typeof(SolutionSyncPublishManager))]
    public class SolutionSyncPublishManagerTests : SolutionSyncPublisherTests
    {
        private ISolutionSyncPublisher publisher;

        private ISolutionSyncPublisherFactory factory;

        private SolutionSyncPublishManager manager;

        public SolutionSyncPublishManagerTests(SyncEventsTestArgs testArgs) : base(testArgs)
        {
            this.testArgs = testArgs;
        }

        [SetUp]
        public void SetUp()
        {
            this.publisher = Substitute.For<ISolutionSyncPublisher>();
            this.factory = Substitute.For<ISolutionSyncPublisherFactory>();
            this.factory.CreatePublisher().Returns(_ => this.publisher);
            this.manager = new SolutionSyncPublishManager(this.factory);
        }

        [Test]
        public void PublisherUpdateCallsCreatePublisher()
        {
            this.manager.UpdateToNewPublisher();
            this.factory.Received().CreatePublisher();
        }

        [Test]
        public void PublisherAssignedBeforeAllOtherEvents()
        {
            void PublishAllInOrder()
            {
                this.manager.PublishSolutionSyncing();
                this.manager.PublishSlnGenerated(default, default);
                this.manager.PublishCsprojGenerated(default, default);
                this.manager.PublishSolutionSynced();
            }

            Assert.That(PublishAllInOrder, Throws.Nothing);
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesSolutionSyncing()
        {
            this.manager.UpdateToNewPublisher();
            this.manager.PublishSolutionSyncing();
            this.publisher.Received().PublishSolutionSyncing();
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesSlnGenerated()
        {
            this.manager.UpdateToNewPublisher();
            this.manager.PublishSlnGenerated(this.testArgs.SlnPath, this.testArgs.SlnContent);
            this.publisher.Received().PublishSlnGenerated(this.testArgs.SlnPath, this.testArgs.SlnContent);
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesCsprojGenerated()
        {
            this.manager.UpdateToNewPublisher();
            this.manager.PublishCsprojGenerated(this.testArgs.CsprojPath, this.testArgs.CsprojContent);
            this.publisher.Received().PublishCsprojGenerated(this.testArgs.CsprojPath, this.testArgs.CsprojContent);
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesSolutionSynced()
        {
            this.manager.UpdateToNewPublisher();
            this.manager.PublishSolutionSynced();
            this.publisher.Received().PublishSolutionSynced();
        }
    }
}
