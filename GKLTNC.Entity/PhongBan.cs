using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GKLTNC.Entity
{
    [Table("PhongBan")]
    public class PhongBan
    {
        [Key]
        [StringLength(10)]
        [Column("MaPB")]
        public string MaPB { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("TenPB")]
        public string TenPB { get; set; } = string.Empty;

        // Navigation property: Một phòng ban có nhiều nhân viên
        public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();

        public override string ToString()
        {
            return TenPB;
        }
    }
}
