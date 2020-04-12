// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.Unity.SolutionProcessing.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mojur.Unity.SolutionProcessing.Internal
{
    /// <summary>
    /// Publishes solution synchronization events to <see cref="SolutionProcessor"/>s.
    /// </summary>
    internal class SolutionSyncPublisher : ISolutionSyncSubscriber, ISolutionSyncEvents
    {
        #region ISolutionSyncEvents Implementation

        /// <inheritdoc/>
        public event Action<ISolutionSyncStatus> SolutionSyncing;

        /// <inheritdoc/>
        public event Action<IGeneratedFile> SlnGenerated;

        /// <inheritdoc/>
        public event Action<IGeneratedFile> CSProjGenerated;

        /// <inheritdoc/>
        public event Action SolutionSynced;

        #endregion

        #region ISolutionSyncSubscriber Implementation

        /// <inheritdoc/>
        public bool OnPreGeneratingCSProjectFiles()
        {
            this.SetupProcessors();

            var args = new SolutionSyncStatus();
            this.SolutionSyncing?.Invoke(args);

            return args.Complete;
        }

        /// <inheritdoc/>
        public string OnGeneratedSlnSolution(string path, string content)
        {
            var args = new GeneratedFile(new FileInfo(path), content);
            this.SlnGenerated?.Invoke(args);

            return args.Contents;
        }

        /// <inheritdoc/>
        public string OnGeneratedCSProject(string path, string content)
        {
            var args = new GeneratedFile(new FileInfo(path), content);
            this.CSProjGenerated?.Invoke(args);

            return args.Contents;
        }

        /// <inheritdoc/>
        public void OnGeneratedCSProjectFiles()
        {
            this.SolutionSynced?.Invoke();
        }

        #endregion

        private void SetupProcessors()
        {
            this.UnsubscribeAll();
            this.RegisterProcessors(FindSolutionProcessorTypes());
        }

        private void UnsubscribeAll()
        {
            this.SolutionSyncing?.UnsubscribeAll();
            this.SlnGenerated?.UnsubscribeAll();
            this.CSProjGenerated?.UnsubscribeAll();
            this.SolutionSynced?.UnsubscribeAll();
        }

        private void RegisterProcessors(IEnumerable<Type> solutionProcessorTypes)
        {
            foreach (var solutionProcessorType in solutionProcessorTypes)
            {
                var processor = (SolutionProcessor)Activator.CreateInstance(solutionProcessorType);
                processor.RegisterSubscriptions(this);
            }
        }

        private static IEnumerable<Type> FindSolutionProcessorTypes()
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                   from type in assembly.GetTypes()
                   where !type.IsAbstract && type.IsSubclassOf(typeof(SolutionProcessor))
                   select type;
        }
    }
}
