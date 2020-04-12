// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing
{
    /// <summary>
    /// Processes/edits solution and project files as they are generated.
    /// </summary>
    public abstract class SolutionProcessor
    {
        /// <summary>
        /// Registers processing actions to solution processing events with <paramref name="events"/>.
        /// </summary>
        /// <param name="events">Events on which to register processing actions.</param>
        public abstract void RegisterSubscriptions(ISolutionSyncEvents events);
    }
}
