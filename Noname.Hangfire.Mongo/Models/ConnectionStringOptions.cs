using System;
using System.Collections.Generic;
using System.Text;

namespace Noname.Hangfire.Mongo.Models
{
    /// <summary>
    /// </summary>
    public class ConnectionStringOptions
    {
        /// <summary>
        /// Gets or sets the authentication source.
        /// </summary>
        /// <value> The authentication source. </value>
        public string authSource { set; get; } = "admin";

        /// <summary>
        /// the connect timeout ms
        /// <para> Mặc định 10 s </para>
        /// </summary>
        /// <value> The connect timeout ms. </value>
        public int connectTimeoutMS { set; get; } = 10000;

        /// <summary>
        /// </summary>
        public int maxPoolSize { set; get; } = 100;

        /// <summary>
        /// Gets or sets the replica set.
        /// </summary>
        /// <value> The replica set. </value>
        public string replicaSet { set; get; }

        /// <summary>
        /// </summary>
        public bool retryWrites { set; get; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ConnectionStringOptions" /> is SSL.
        /// </summary>
        /// <value> <c> true </c> if SSL; otherwise, <c> false </c>. </value>
        public bool ssl { set; get; } = false;

        /// <summary>
        /// queue timeout
        /// <para> Mặc định 15 s </para>
        /// </summary>
        public int waitQueueTimeoutMS { set; get; } = 15000;
    }
}
