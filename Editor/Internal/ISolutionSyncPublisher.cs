// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Publishes solution synchronization events.
    /// </summary>
    internal interface ISolutionSyncPublisher
    {
        bool PublishSolutionSyncing();

        string PublishSlnGenerated(string path, string contents);

        string PublishCsprojGenerated(string path, string contents);

        void PublishSolutionSynced();
    }
}
