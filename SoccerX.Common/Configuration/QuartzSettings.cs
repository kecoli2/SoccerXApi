using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// Configuration settings for Quartz.NET job scheduling.
    /// </summary>
    public class QuartzSettings
    {
        #region Fields      
        /// <summary>
        /// Indicates whether Quartz job scheduling is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The job store type to use for Quartz.
        /// Options: "Memory", "Database".
        /// </summary>
        public string JobStoreType { get; set; } = "Memory";

        /// <summary>
        /// Determines whether Quartz should start jobs automatically on application startup.
        /// </summary>
        public bool StartOnStartup { get; set; } = true;

        /// <summary>
        /// Defines the misfire threshold in milliseconds.
        /// Determines how long Quartz waits before considering a misfire scenario.
        /// </summary>
        public int MisfireThreshold { get; set; } = 60000;

        /// <summary>
        /// The frequency (in seconds) at which Quartz checks for new triggers.
        /// </summary>
        public int TriggerCheckInterval { get; set; } = 10;

        /// <summary>
        /// Maximum number of concurrent jobs allowed.
        /// </summary>
        public int MaxConcurrency { get; set; } = 5;
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Protected
        #endregion
    }
}
