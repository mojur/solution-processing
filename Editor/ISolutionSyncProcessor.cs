// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing
{
    /// <summary>
    /// Processes solution synchronization events
    /// </summary>
    public interface ISolutionSyncProcessor
    {
        /// <summary>
        /// Registers processing actions to solution processing events with <paramref name="events"/>.
        /// </summary>
        /// <param name="events">Events on which to register processing actions.</param>
        void RegisterSubscriptions(ISolutionSyncEvents events);
    }
}
