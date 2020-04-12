// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Mojur.Unity.SolutionProcessing.Utilities
{
    /// <summary>
    /// Extensions for the solution processing framework.
    /// </summary>
    public static class SolutionProcessingExtensions
    {
        /// <summary>
        /// Unsubscribes all delegates from the invocation list.
        /// </summary>
        /// <param name="action">Action from which to unsubscribe all.</param>
        public static void UnsubscribeAll(this Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var @delegate in action.GetInvocationList())
            {
                if (@delegate != null)
                {
                    action -= (Action)@delegate;
                }
            }
        }

        /// <summary>
        /// Unsubscribes all delegates from the invocation list.
        /// </summary>
        /// <param name="action">Action from which to unsubscribe all.</param>
        public static void UnsubscribeAll<TArgs>(this Action<TArgs> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var @delegate in action.GetInvocationList())
            {
                if (@delegate != null)
                {
                    action -= (Action<TArgs>)@delegate;
                }
            }
        }
    }
}
