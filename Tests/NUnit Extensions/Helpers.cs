// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Mojur.NUnitExtensions.Events;
using Mojur.NUnitExtensions.Reflection;

namespace Mojur.NUnitExtensions
{
    public class Does
    {
        public static EventRaisedConstraint<TEventArg> RaiseWith<TEventArg>(
            SubscriptionCallback<TEventArg> subscription)
        {
            return new EventRaisedConstraint<TEventArg>(subscription);
        }

        public static EventRaisedConstraint Raise(SubscriptionCallback subscription)
        {
            return new EventRaisedConstraint(subscription);
        }
    }

    public class Has : NUnit.Framework.Has
    {
        public static MethodExistsConstraint Method(string methodName)
        {
            return new MethodExistsConstraint(methodName);
        }
    }
}
