// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.NUnitExtensions.Reflection;
using Mojur.Unity.SolutionProcessing.Internal;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Reflection;
using Has = Mojur.NUnitExtensions.Has;

namespace Mojur.Unity.SolutionProcessing.Tests
{
    [TestOf(typeof(SolutionSyncUnitySubscriber))]
    public class SolutionSyncUnitySubscriberTests : SolutionSyncPublisherTests
    {
        private ISolutionSyncPublisher publisher;

        public SolutionSyncUnitySubscriberTests(SyncEventsTestArgs testArgs) : base(testArgs)
        {
        }

        [OneTimeSetUp]
        public void SetMockPublisher()
        {
            this.publisher = Substitute.For<ISolutionSyncPublisher>();
            SolutionSyncUnitySubscriber.Publisher = this.publisher;
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesSolutionSyncing()
        {
            UnitySolutionSyncTestUtilities.InvokeStaticSyncMethodFor<SolutionSyncUnitySubscriber>(
                UnitySyncMethodName.OnPreGeneratingCSProjectFiles);

            this.publisher.Received().PublishSolutionSyncing();
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesSlnGenerated()
        {
            UnitySolutionSyncTestUtilities.InvokeStaticSyncMethodFor<SolutionSyncUnitySubscriber>(
                UnitySyncMethodName.OnGeneratedSlnSolution,
                this.testArgs.SlnPath,
                this.testArgs.SlnContent);

            this.publisher.Received().PublishSlnGenerated(this.testArgs.SlnPath, this.testArgs.SlnContent);
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesCsprojGenerated()
        {
            UnitySolutionSyncTestUtilities.InvokeStaticSyncMethodFor<SolutionSyncUnitySubscriber>(
                UnitySyncMethodName.OnGeneratedCSProject,
                this.testArgs.CsprojPath,
                this.testArgs.CsprojContent);

            this.publisher.Received().PublishCsprojGenerated(this.testArgs.CsprojPath, this.testArgs.CsprojContent);
        }

        /// <inheritdoc />
        [Test]
        public override void PublishesSolutionSynced()
        {
            UnitySolutionSyncTestUtilities.InvokeStaticSyncMethodFor<SolutionSyncUnitySubscriber>(
                UnitySyncMethodName.OnGeneratedCSProjectFiles);

            this.publisher.Received().PublishSolutionSynced();
        }

        [Test]
        public void OnPreGeneratingCSProjectFilesCanBeCalledByUnity()
        {
            Assert.That(
                typeof(SolutionSyncUnitySubscriber),
                HasSyncMethod(UnitySyncMethodName.OnPreGeneratingCSProjectFiles)
                    .WithNoParams.ThatReturns(typeof(bool)));
        }

        [Test]
        public void OnGeneratedSlnSolutionCanBeCalledByUnity()
        {
            Assert.That(
                typeof(SolutionSyncUnitySubscriber),
                HasSyncMethod(UnitySyncMethodName.OnGeneratedSlnSolution)
                    .WithParams(typeof(string), typeof(string))
                    .ThatReturns(typeof(string)));
        }

        [Test]
        public void OnGeneratedCSProjectCanBeCalledByUnity()
        {
            Assert.That(
                typeof(SolutionSyncUnitySubscriber),
                HasSyncMethod(UnitySyncMethodName.OnGeneratedCSProject)
                    .WithParams(typeof(string), typeof(string))
                    .ThatReturns(typeof(string)));
        }

        [Test]
        public void OnGeneratedCSProjectFilesCanBeCalledByUnity()
        {
            Assert.That(
                typeof(SolutionSyncUnitySubscriber),
                HasSyncMethod(UnitySyncMethodName.OnGeneratedCSProjectFiles).WithNoParams.ThatReturns(typeof(void)));
        }

        [TearDown]
        public void ResetPublisherCalls()
        {
            this.publisher.ClearReceivedCalls();
        }

        [OneTimeTearDown]
        public static void ResetPublisherField()
        {
            SolutionSyncUnitySubscriber.ResetPublisher();
        }

        private static MethodExistsConstraint HasSyncMethod(UnitySyncMethodName methodName)
        {
            return (MethodExistsConstraint)Has.Method(methodName.ToString()).Using(SyncMethodSearch);

            MethodBase[] SyncMethodSearch(Type type, string name)
            {
                return new MethodBase[] { UnitySolutionSyncTestUtilities.GetStaticSyncMethodFor(type, name) };
            }
        }
    }
}
