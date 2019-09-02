using Microsoft.VisualStudio.Services.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCore.Common
{
    public  class CircuitBreaker
    {
        private readonly object monitor = new object();
        private CircuitBreakerState state;

        public CircuitBreaker(int threshold,TimeSpan timeout)
        {
            if (threshold < 1)
                throw new ArgumentOutOfRangeException("threshold", "閘道必須大於0");
            if (timeout.TotalMilliseconds < 1)
                throw new ArgumentOutOfRangeException("timeout","延遲時間必須大於0");

            Timeout = timeout;
            Threshold = threshold;
            MoveToClosedState();
        }

        internal CircuitBreakerState MoveToClosedState()
        {
            state = new ClosedState(this);
            return state;
        }

        internal void ResetFailureCount()
        {
            Failures = 0;
        }

        public int Threshold { get; set; }
        public TimeSpan Timeout { get; set; }
        public int Failures { get; set; }
        private Exception exceptionFromLastAttemptCall = null;

        internal void IncreaseFailureCount()
        {
         

        }

        public CircuitBreaker AttemptCall(Action protectedCode)
        {
            this.exceptionFromLastAttemptCall = null;
            lock(monitor)
            {
                state.ProtectedCodeIsAboutToBeCalled();
                if (state is OpenState)
                {
                    return this;
                }
            }

            try
            {

            }
            catch (Exception e)
            {

            }

            lock(monitor)
            {

            }
            return this;

        }

    }

    //斷路器 已關閉狀態
    public class ClosedState : CircuitBreakerState
    {
        public ClosedState(CircuitBreaker circuitBreaker) : base(circuitBreaker)
        {
            circuitBreaker.ResetFailureCount();
        }

        //public override 
    }

    //斷路器 開啟狀態
    public class OpenState : CircuitBreakerState
    {
        public OpenState(CircuitBreaker circuitBreaker) : base(circuitBreaker)
        {

        }
    }

    //斷路器 半開啟狀態
    public class HalfOpenState : CircuitBreakerState
    {
        public HalfOpenState(CircuitBreaker circuitBreaker) : base(circuitBreaker)
        {

        }
    }

    public abstract class CircuitBreakerState
    {
        protected readonly CircuitBreaker circuitBreaker;

        protected CircuitBreakerState(CircuitBreaker circuitBreaker)
        {
            this.circuitBreaker = circuitBreaker;
        }

        public virtual CircuitBreaker ProtectedCodeIsAboutToBeCalled()
        {
            return this.circuitBreaker;
        }

        public virtual void ProtectedCodeHasBeenCalled() { }
        public virtual void ActUponException(Exception e)
        {
            circuitBreaker.IncreaseFailureCount();
        }
        public virtual CircuitBreakerState Update()
        {
            return this;
        }


    }
}
