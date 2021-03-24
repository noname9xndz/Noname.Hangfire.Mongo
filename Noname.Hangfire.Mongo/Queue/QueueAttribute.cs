using Hangfire.Common;
using Hangfire.States;
using Noname.Hangfire.Mongo.Models;

namespace Noname.Hangfire.Mongo.Queue
{
    /// <summary>
    /// </summary>
    public sealed class QueueAttribute : JobFilterAttribute, IElectStateFilter
    {
        /// <summary>
        /// </summary>
        /// <param name="queue"> </param>
        public QueueAttribute(string queue)
        {
            Queue = QueueCustom.GetValue(queue);
        }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        /// <value> The queue. </value>
        public string Queue { get; }

        /// <summary>
        /// Called when the current state of the job is being changed to the specified candidate state. This state change could be intercepted and the final state could be changed
        /// through setting the different state in the context in an implementation of this method.
        /// </summary>
        /// <param name="context"> </param>
        public void OnStateElection(ElectStateContext context)
        {
            EnqueuedState EnqueuedState = context.CandidateState as EnqueuedState;
            if (EnqueuedState != null)
            {
                EnqueuedState.Queue = QueueCustom.GetValue(Queue);
            }
            FailedState failedState = context.CandidateState as FailedState;
            if (failedState != null && failedState.Exception != null)
            {
                //todo push error data
            }
        }
    }
}