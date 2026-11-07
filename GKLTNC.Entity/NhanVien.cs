using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GKLTNC.Entity
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [StringLength(20)]
        [Column("MaNV")]
        public string MaNV { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("HoTen")]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [Column("GioiTinh")]
        public bool GioiTinh { get; set; } // true: Nam, false: Nữ

        [Required]
        [Column("NgaySinh")]
        public DateTime NgaySinh { get; set; }

        [StringLength(200)]
        [Column("DiaChi")]
        public string? DiaChi { get; set; }

        [Required]
        [Column("Luong", TypeName = "decimal(18,2)")]
        public decimal Luong { get; set; }

        [Required]
        [StringLength(10)]
        [Column("MaPB")]
        public string MaPB { get; set; } = string.Empty;

        // Navigation property: Mỗi nhân viên thuộc một phòng ban
        [ForeignKey(nameof(MaPB))]
        public virtual PhongBan? PhongBan { get; set; }
    }
}
