// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.Unity.SolutionProcessing.Internal;
using NUnit.Framework;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestFixture]
    public class SolutionSyncStatusTests
    {
        [Test]
        [TestOf(typeof(SolutionSyncStatus))]
        public void AbortValueOfTruePersistsThroughAllOtherSetValues()
        {
            var status = new SolutionSyncStatus { AbortSync = false };

            status.AbortSync = true;
            status.AbortSync = false;

            Assert.That(status.AbortSync, Is.True);
        }

        [Test]
        [TestOf(typeof(SolutionSyncStatus))]
        public void DefaultAbortValueIsFalse()
        {
            var status = new SolutionSyncStatus();
            Assert.That(status.AbortSync, Is.False);
        }
    }
}
