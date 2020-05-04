using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;

namespace Mojur.NUnitExtensions.Events
{
    public delegate void SubscriptionCallback(Action assertion);

    public delegate void SubscriptionCallback<out TEventArg>(Action<TEventArg> assertion);

    public abstract class BaseEventRaisedConstraint : Constraint
    {
        protected bool eventWasRaised { get; set; }

        /// <inheritdoc />
        public override string Description
        {
            get { return "event raised"; }
        }

        /// <inheritdoc />
        public override ConstraintResult ApplyTo(object actual)
        {
            return actual is TestDelegate action
                ? this.ApplyTo(action)
                : throw new ArgumentException(
                    $"The actual value must be a TestDelegate or AsyncTestDelegate but was {actual.GetType().Name}",
                    nameof(actual));
        }

        /// <inheritdoc />
        public override ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del)
        {
            return this.ApplyTo(() => del());
        }

        public abstract EventRaisedConstraintResult ApplyTo(TestDelegate publication);

        public class EventRaisedConstraintResult : ConstraintResult
        {
            public EventRaisedConstraintResult(BaseEventRaisedConstraint constraint, bool eventWasRaised) : base(
                constraint,
                eventWasRaised,
                eventWasRaised)
            {
            }

            /// <inheritdoc />
            public override void WriteActualValueTo(MessageWriter writer)
            {
                bool eventWasRaised = (bool)this.ActualValue;
                writer.Write(eventWasRaised ? "event raised" : "event not raised");
            }
        }
    }

    public abstract class BaseEventRaisedConstraint<TSubscription, TAssertion> : BaseEventRaisedConstraint
        where TSubscription : Delegate where TAssertion : Delegate
    {
        private readonly TSubscription subscription;

        protected BaseEventRaisedConstraint(TSubscription subscription)
        {
            this.subscription = subscription;
        }

        public override EventRaisedConstraintResult ApplyTo(TestDelegate publication)
        {
            var assertion = this.GetAssertion();
            this.subscription.DynamicInvoke(assertion);
            publication();

            return this.GetConstraintResult();
        }

        protected abstract EventRaisedConstraintResult GetConstraintResult();

        protected abstract TAssertion GetAssertion();
    }

    public class EventRaisedConstraint : BaseEventRaisedConstraint<SubscriptionCallback, Action>
    {
        /// <inheritdoc />
        public EventRaisedConstraint(SubscriptionCallback subscription) : base(subscription)
        {
        }

        /// <inheritdoc />
        protected override EventRaisedConstraintResult GetConstraintResult()
        {
            return new EventRaisedConstraintResult(this, this.eventWasRaised);
        }

        /// <inheritdoc />
        protected override Action GetAssertion()
        {
            return () => this.eventWasRaised = true;
        }
    }

    public class EventRaisedConstraint<TEventArg>
        : BaseEventRaisedConstraint<SubscriptionCallback<TEventArg>, Action<TEventArg>>
    {
        public delegate TSelected ArgSelector<out TSelected>(TEventArg arg);

        private Func<TEventArg, ConstraintResult> applyArgConstraint;

        private ConstraintResult argConstraintResult;

        /// <inheritdoc />
        public override string Description
        {
            get
            {
                return this.applyArgConstraint == null
                    ? base.Description
                    : "event raised with argument: " +
                    (this.argConstraintResult ?? this.applyArgConstraint(default)).Description;
            }
        }

        /// <inheritdoc />
        public EventRaisedConstraint(SubscriptionCallback<TEventArg> subscription) : base(subscription)
        {
        }

        /// <inheritdoc />
        protected override EventRaisedConstraintResult GetConstraintResult()
        {
            return new EventRaisedArgConstraintResult(this, this.eventWasRaised, this.argConstraintResult);
        }

        /// <inheritdoc />
        protected override Action<TEventArg> GetAssertion()
        {
            return arg =>
            {
                this.eventWasRaised = true;
                this.argConstraintResult = this.applyArgConstraint?.Invoke(arg);
            };
        }

        public EventRaisedConstraint<TEventArg> WithArgThat(IResolveConstraint expression)
        {
            return this.WithArgWhere(arg => arg, expression);
        }

        public EventRaisedConstraint<TEventArg> WithArgWhere<TSelected>(
            ArgSelector<TSelected> selector,
            IResolveConstraint expression)
        {
            var constraint = expression.Resolve();
            this.applyArgConstraint = arg => constraint.ApplyTo(selector(arg));

            return this;
        }

        public class EventRaisedArgConstraintResult : EventRaisedConstraintResult
        {
            private readonly ConstraintResult argConstraintResult;

            public EventRaisedArgConstraintResult(
                BaseEventRaisedConstraint constraint,
                bool eventWasRaised,
                ConstraintResult argResult) : base(constraint, eventWasRaised && (argResult?.IsSuccess ?? true))
            {
                this.argConstraintResult = argResult;
            }

            /// <inheritdoc />
            public override void WriteActualValueTo(MessageWriter writer)
            {
                if (this.argConstraintResult == null)
                {
                    base.WriteActualValueTo(writer);
                }
                else
                {
                    writer.Write("event raised with argument: ");
                    this.argConstraintResult.WriteActualValueTo(writer);
                }
            }
        }
    }
}
