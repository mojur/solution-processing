// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Internal
{
    internal class SolutionProcessorPublisherFactory : ISolutionSyncPublisherFactory
    {
        private readonly ISolutionSyncProcessorRepository repository;

        public SolutionProcessorPublisherFactory(ISolutionSyncProcessorRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc />
        public ISolutionSyncPublisher CreatePublisher()
        {
            return new SolutionProcessorPublisher(this.repository.GetProcessors());
        }
    }
}
