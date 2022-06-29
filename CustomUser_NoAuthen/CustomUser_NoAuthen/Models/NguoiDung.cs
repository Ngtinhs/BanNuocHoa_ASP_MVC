using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomUser_NoAuthen.Models
{
    public class NguoiDung
    {
        [Key]
        public int UserIdentityId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int UserRoleId { get; set; }
        public VaiTro VaiTro { get; set; }
    }

    public class VaiTro
    {
        [Key]
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class KhachHang : NguoiDung
    {
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<GioHang> GioHangs { get; set; }
    }
}