// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Publishes solution synchronization events to <see cref="ISolutionSyncProcessor"/>s.
    /// </summary>
    internal class SolutionProcessorPublisher : ISolutionSyncEvents, ISolutionSyncPublisher
    {
        /// <inheritdoc />
        public event Action<ISolutionSyncStatus> SolutionSyncing;

        /// <inheritdoc />
        public event Action<IGeneratedFile> SlnGenerated;

        /// <inheritdoc />
        public event Action<IGeneratedFile> CSProjGenerated;

        /// <inheritdoc />
        public event Action SolutionSynced;

        public SolutionProcessorPublisher(IEnumerable<ISolutionSyncProcessor> listeners)
        {
            foreach (var listener in listeners)
            {
                listener.RegisterSubscriptions(this);
            }
        }

        /// <inheritdoc />
        public bool PublishSolutionSyncing()
        {
            var status = new SolutionSyncStatus();
            this.SolutionSyncing?.Invoke(status);

            return status.AbortSync;
        }

        /// <inheritdoc />
        public string PublishSlnGenerated(string path, string contents)
        {
            var file = new GeneratedFile(new FileInfo(path), contents);
            this.SlnGenerated?.Invoke(file);

            return file.Content;
        }

        /// <inheritdoc />
        public string PublishCsprojGenerated(string path, string contents)
        {
            var file = new GeneratedFile(new FileInfo(path), contents);
            this.CSProjGenerated?.Invoke(file);

            return file.Content;
        }

        /// <inheritdoc />
        public void PublishSolutionSynced()
        {
            this.SolutionSynced?.Invoke();
        }
    }
}
