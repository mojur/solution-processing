// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Internal
{
    internal class SolutionSyncStatus : ISolutionSyncStatus
    {
        private bool abortSync;

        /// <inheritdoc/>
        /// <remarks>Assignments of 'true' override all previous and further assignments.</remarks>
        public bool AbortSync
        {
            get => this.abortSync;
            set => this.abortSync |= value;
        }

        public SolutionSyncStatus(bool abortSync = false)
        {
            this.abortSync = abortSync;
        }
    }
}
