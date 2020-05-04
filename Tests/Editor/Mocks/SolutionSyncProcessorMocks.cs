// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Mojur.Unity.SolutionProcessing.Tests
{
    public class MockProcessor : ISolutionSyncProcessor
    {
        /// <inheritdoc />
        public void RegisterSubscriptions(ISolutionSyncEvents events)
        {
            events.SolutionSynced += this.OnSolutionSynced;
        }

        private void OnSolutionSynced() // Must not be static for unit testing.
        {
        }
    }

    public interface IMockProcessor : ISolutionSyncProcessor
    {
    }

    public abstract class MockAbstractProcessor : ISolutionSyncProcessor
    {
        /// <inheritdoc />
        public abstract void RegisterSubscriptions(ISolutionSyncEvents events);
    }

    public class OuterClass
    {
        protected class HiddenMockProcessor : ISolutionSyncProcessor
        {
            /// <inheritdoc />
            public void RegisterSubscriptions(ISolutionSyncEvents events)
            {
            }
        }
    }

    public class GenericMockProcessor<TArg> : ISolutionSyncProcessor
    {
        /// <inheritdoc />
        public void RegisterSubscriptions(ISolutionSyncEvents events)
        {
        }
    }
}
