using AdminDashBoardV1._0._0.Models;

namespace AdminDashBoardV1._0._0.Views.User
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
