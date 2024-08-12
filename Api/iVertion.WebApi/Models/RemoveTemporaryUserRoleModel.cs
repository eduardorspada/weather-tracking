using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iVertion.WebApi.Models
{
    /// <summary>
    /// This model is the body of users' requests for temporary roles.
    /// </summary>
    public class RemoveTemporaryUserRoleModel
    {
        /// <summary>
        /// A role registered in the Identity roles.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// An existing username on Identity.
        /// </summary>
        public string? UserName { get; set; }
    }
}