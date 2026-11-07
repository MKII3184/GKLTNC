using GKLTNC.Entity;
using Microsoft.EntityFrameworkCore;

namespace GKLTNC.DAL
{
    public class NhanVienDAL
    {
        public List<NhanVien> GetAll()
        {
            using var db = new AppDbContext();
            return db.NhanViens
                .Include(n => n.PhongBan)
                .AsNoTracking()
                .OrderBy(n => n.MaNV)
                .ToList();
        }

        public NhanVien? GetById(string maNv)
        {
            if (string.IsNullOrWhiteSpace(maNv)) return null;

            using var db = new AppDbContext();
            return db.NhanViens
                .Include(n => n.PhongBan)
                .AsNoTracking()
                .FirstOrDefault(n => n.MaNV == maNv);
        }

        public List<NhanVien> GetByPhongBan(string maPb)
        {
            using var db = new AppDbContext();
            return db.NhanViens
                .Include(n => n.PhongBan)
                .Where(n => n.MaPB == maPb)
                .AsNoTracking()
                .OrderBy(n => n.MaNV)
                .ToList();
        }

        public List<NhanVien> Search(string? keyword, string? maPb)
        {
            using var db = new AppDbContext();
            var query = db.NhanViens
                .Include(n => n.PhongBan)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(maPb))
            {
                query = query.Where(n => n.MaPB == maPb);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(n => n.MaNV.Contains(keyword) || n.HoTen.Contains(keyword));
            }

            return query.OrderBy(n => n.MaNV).ToList();
        }

        public bool Exists(string maNv)
        {
            if (string.IsNullOrWhiteSpace(maNv)) return false;

            using var db = new AppDbContext();
            return db.NhanViens.Any(n => n.MaNV == maNv);
        }

        public void Add(NhanVien nv)
        {
            using var db = new AppDbContext();
            db.NhanViens.Add(nv);
            db.SaveChanges();
        }

        public void Update(NhanVien nv)
        {
            using var db = new AppDbContext();
            var existing = db.NhanViens.Find(nv.MaNV);
            if (existing == null)
                throw new InvalidOperationException($"Không tìm thấy nhân viên có mã '{nv.MaNV}'.");

            existing.HoTen = nv.HoTen;
            existing.GioiTinh = nv.GioiTinh;
            existing.NgaySinh = nv.NgaySinh;
            existing.DiaChi = nv.DiaChi;
            existing.Luong = nv.Luong;
            existing.MaPB = nv.MaPB;

            db.SaveChanges();
        }

        public void Delete(string maNv)
        {
            using var db = new AppDbContext();
            var existing = db.NhanViens.Find(maNv);
            if (existing != null)
            {
                db.NhanViens.Remove(existing);
                db.SaveChanges();
            }
        }
    }
}
