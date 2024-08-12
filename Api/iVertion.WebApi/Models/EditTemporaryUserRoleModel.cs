
namespace iVertion.WebApi.Models
{
    /// <summary>
    /// This is the model for update a temporary user role.
    /// </summary>
    public class EditTemporaryUserRoleModel
    {
        /// <summary>
        /// An integer as Id.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// The start date of the temporary user role.
        /// </summary>
        /// <value></value>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// The end date of the temporary user role.
        /// </summary>
        /// <value></value>
        public DateTime ExpirationDate { get; set; }
    }
}