// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Manages the creation of <see cref="ISolutionSyncPublisher"/>s.
    /// </summary>
    internal class SolutionSyncPublishManager : ISolutionSyncPublisher
    {
        private readonly ISolutionSyncPublisherFactory factory;

        private ISolutionSyncPublisher publisher;

        public SolutionSyncPublishManager(ISolutionSyncPublisherFactory factory)
        {
            this.factory = factory;
        }

        /// <inheritdoc />
        public bool PublishSolutionSyncing()
        {
            this.UpdateToNewPublisher();
            return this.publisher.PublishSolutionSyncing();
        }

        /// <inheritdoc />
        public string PublishSlnGenerated(string path, string contents)
        {
            return this.publisher.PublishSlnGenerated(path, contents);
        }

        /// <inheritdoc />
        public string PublishCsprojGenerated(string path, string contents)
        {
            return this.publisher.PublishCsprojGenerated(path, contents);
        }

        /// <inheritdoc />
        public void PublishSolutionSynced()
        {
            this.publisher.PublishSolutionSynced();
        }

        public void UpdateToNewPublisher()
        {
            this.publisher = this.factory.CreatePublisher();
        }
    }
}
