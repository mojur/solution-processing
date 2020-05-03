// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Creates <see cref="ISolutionSyncPublisher"/>s.
    /// </summary>
    internal interface ISolutionSyncPublisherFactory
    {
        ISolutionSyncPublisher CreatePublisher();
    }
}
