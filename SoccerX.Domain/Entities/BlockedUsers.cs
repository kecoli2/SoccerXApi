using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerX.Domain.Entities
{
    /// <summary>
    /// Represents the relationship between users who have blocked other users.
    /// </summary>
    public class BlockedUsers
    {
        /// <summary>
        /// The ID of the user who is blocking another user.
        /// </summary>
        public Guid BlockerId { get; set; }

        /// <summary>
        /// The ID of the user who is being blocked.
        /// </summary>
        public Guid BlockedId { get; set; }

        /// <summary>
        /// Navigation property for the blocker user.
        /// </summary>
        public virtual Users Blocker { get; set; }

        /// <summary>
        /// Navigation property for the blocked user.
        /// </summary>
        public virtual Users Blocked { get; set; }
    }
}
