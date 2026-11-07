using GKLTNC.DAL;
using GKLTNC.Entity;

namespace GKLTNC.BUS
{
    public class PhongBanBUS
    {
        private readonly PhongBanDAL _phongBanDAL;

        public PhongBanBUS()
        {
            _phongBanDAL = new PhongBanDAL();
        }

        public PhongBanBUS(PhongBanDAL phongBanDAL)
        {
            _phongBanDAL = phongBanDAL;
        }

        public List<PhongBan> GetAllPhongBan()
        {
            return _phongBanDAL.GetAll();
        }

        public PhongBan? GetPhongBanById(string maPb)
        {
            if (string.IsNullOrWhiteSpace(maPb)) return null;
            return _phongBanDAL.GetById(maPb.Trim());
        }

        public bool KiemTraTonTai(string maPb)
        {
            if (string.IsNullOrWhiteSpace(maPb)) return false;
            return _phongBanDAL.Exists(maPb.Trim());
        }
    }
}
