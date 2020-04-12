// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Internal
{
    internal class SolutionSyncStatus : ISolutionSyncStatus
    {
        private bool complete;

        /// <inheritdoc/>
        /// <remarks>Assignments of 'true' override all previous and further assignments.</remarks>
        public bool Complete
        {
            get => this.complete;
            set => this.complete |= value;
        }

        public SolutionSyncStatus(bool complete = false)
        {
            this.complete = complete;
        }
    }
}
