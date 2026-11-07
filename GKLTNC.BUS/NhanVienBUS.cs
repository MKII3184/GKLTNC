using GKLTNC.DAL;
using GKLTNC.Entity;

namespace GKLTNC.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _nhanVienDAL;
        private readonly PhongBanDAL _phongBanDAL;

        public NhanVienBUS()
        {
            _nhanVienDAL = new NhanVienDAL();
            _phongBanDAL = new PhongBanDAL();
        }

        public NhanVienBUS(NhanVienDAL nhanVienDAL, PhongBanDAL phongBanDAL)
        {
            _nhanVienDAL = nhanVienDAL;
            _phongBanDAL = phongBanDAL;
        }

        public List<NhanVien> GetAllNhanVien()
        {
            return _nhanVienDAL.GetAll();
        }

        public NhanVien? GetNhanVienById(string maNv)
        {
            if (string.IsNullOrWhiteSpace(maNv)) return null;
            return _nhanVienDAL.GetById(maNv.Trim());
        }

        public List<NhanVien> SearchNhanVien(string? keyword, string? maPb)
        {
            return _nhanVienDAL.Search(keyword, maPb);
        }

        public void ThemNhanVien(NhanVien nv)
        {
            ValidateNhanVien(nv, isAdding: true);

            _nhanVienDAL.Add(nv);
        }

        public void SuaNhanVien(NhanVien nv)
        {
            ValidateNhanVien(nv, isAdding: false);

            _nhanVienDAL.Update(nv);
        }

        public void XoaNhanVien(string maNv)
        {
            if (string.IsNullOrWhiteSpace(maNv))
                throw new ArgumentException("Mã nhân viên không được để trống!");

            string cleanMaNv = maNv.Trim();
            if (!_nhanVienDAL.Exists(cleanMaNv))
                throw new InvalidOperationException($"Không tìm thấy nhân viên có mã '{cleanMaNv}' để xóa!");

            _nhanVienDAL.Delete(cleanMaNv);
        }

        private void ValidateNhanVien(NhanVien nv, bool isAdding)
        {
            if (nv == null)
                throw new ArgumentNullException(nameof(nv), "Dữ liệu nhân viên không được để trống!");

            // 1. Kiểm tra Mã nhân viên
            if (string.IsNullOrWhiteSpace(nv.MaNV))
                throw new ArgumentException("Mã nhân viên không được để trống!");

            nv.MaNV = nv.MaNV.Trim();
            if (nv.MaNV.Length > 20)
                throw new ArgumentException("Mã nhân viên không được vượt quá 20 ký tự!");

            if (isAdding)
            {
                if (_nhanVienDAL.Exists(nv.MaNV))
                    throw new InvalidOperationException($"Mã nhân viên '{nv.MaNV}' đã tồn tại trong cơ sở dữ liệu!");
            }
            else
            {
                if (!_nhanVienDAL.Exists(nv.MaNV))
                    throw new InvalidOperationException($"Không tìm thấy nhân viên có mã '{nv.MaNV}' để cập nhật!");
            }

            // 2. Kiểm tra Họ và tên
            if (string.IsNullOrWhiteSpace(nv.HoTen))
                throw new ArgumentException("Họ và tên nhân viên không được để trống!");

            nv.HoTen = nv.HoTen.Trim();
            if (nv.HoTen.Length > 100)
                throw new ArgumentException("Họ và tên không được vượt quá 100 ký tự!");

            // 3. Kiểm tra Phòng ban
            if (string.IsNullOrWhiteSpace(nv.MaPB))
                throw new ArgumentException("Vui lòng chọn phòng ban cho nhân viên!");

            nv.MaPB = nv.MaPB.Trim();
            if (!_phongBanDAL.Exists(nv.MaPB))
                throw new ArgumentException($"Phòng ban '{nv.MaPB}' không tồn tại trong hệ thống!");

            // 4. Kiểm tra Ngày sinh & Độ tuổi
            if (nv.NgaySinh.Date > DateTime.Today)
                throw new ArgumentException("Ngày sinh không thể lớn hơn ngày hiện tại!");

            int age = DateTime.Today.Year - nv.NgaySinh.Year;
            if (nv.NgaySinh.Date > DateTime.Today.AddYears(-age)) age--;

            if (age < 18)
                throw new ArgumentException($"Nhân viên chưa đủ 18 tuổi (hiện tại {age} tuổi)! Vui lòng kiểm tra lại ngày sinh.");

            // 5. Kiểm tra Lương
            if (nv.Luong < 0)
                throw new ArgumentException("Mức lương của nhân viên phải là số không âm!");

            // 6. Địa chỉ
            if (!string.IsNullOrWhiteSpace(nv.DiaChi))
            {
                nv.DiaChi = nv.DiaChi.Trim();
                if (nv.DiaChi.Length > 200)
                    throw new ArgumentException("Địa chỉ không được vượt quá 200 ký tự!");
            }
        }
    }
}
