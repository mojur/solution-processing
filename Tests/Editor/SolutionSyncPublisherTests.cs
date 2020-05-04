// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestFixtureSource(typeof(SyncEventsTestArgsSource))]
    public abstract class SolutionSyncPublisherTests
    {
        protected SyncEventsTestArgs testArgs;

        protected SolutionSyncPublisherTests(SyncEventsTestArgs testArgs)
        {
            this.testArgs = testArgs;
        }

        public abstract void PublishesSolutionSyncing();

        public abstract void PublishesSlnGenerated();

        public abstract void PublishesCsprojGenerated();

        public abstract void PublishesSolutionSynced();
    }
}
