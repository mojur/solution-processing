// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Gathers <see cref="ISolutionSyncProcessor"/>s.
    /// </summary>b
    internal interface ISolutionSyncProcessorRepository
    {
        /// <summary>
        /// Gets <see cref="ISolutionSyncProcessor"/>s within the repository.
        /// </summary>
        /// <returns><see cref="ISolutionSyncProcessor"/>s within the repository.</returns>
        IEnumerable<ISolutionSyncProcessor> GetProcessors();
    }
}
