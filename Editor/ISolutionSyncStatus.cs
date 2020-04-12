// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing
{
    /// <summary>
    /// Provides the status of the solution's synchronization.
    /// </summary>
    public interface ISolutionSyncStatus
    {
        /// <summary>
        /// Whether the the synchronization is complete.
        /// </summary>
        bool Complete { get; set; }
    }
}
