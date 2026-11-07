using GKLTNC.Entity;
using Microsoft.EntityFrameworkCore;

namespace GKLTNC.DAL
{
    public class PhongBanDAL
    {
        public List<PhongBan> GetAll()
        {
            using var db = new AppDbContext();
            return db.PhongBans
                .AsNoTracking()
                .OrderBy(p => p.MaPB)
                .ToList();
        }

        public PhongBan? GetById(string maPb)
        {
            if (string.IsNullOrWhiteSpace(maPb)) return null;

            using var db = new AppDbContext();
            return db.PhongBans
                .AsNoTracking()
                .FirstOrDefault(p => p.MaPB == maPb);
        }

        public bool Exists(string maPb)
        {
            if (string.IsNullOrWhiteSpace(maPb)) return false;

            using var db = new AppDbContext();
            return db.PhongBans.Any(p => p.MaPB == maPb);
        }

        public void Add(PhongBan pb)
        {
            using var db = new AppDbContext();
            db.PhongBans.Add(pb);
            db.SaveChanges();
        }

        public void Update(PhongBan pb)
        {
            using var db = new AppDbContext();
            var existing = db.PhongBans.Find(pb.MaPB);
            if (existing == null)
                throw new InvalidOperationException($"Không tìm thấy phòng ban có mã '{pb.MaPB}'.");

            existing.TenPB = pb.TenPB;
            db.SaveChanges();
        }

        public void Delete(string maPb)
        {
            using var db = new AppDbContext();
            var existing = db.PhongBans.Find(maPb);
            if (existing != null)
            {
                db.PhongBans.Remove(existing);
                db.SaveChanges();
            }
        }
    }
}
