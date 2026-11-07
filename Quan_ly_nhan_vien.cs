using System.Drawing.Drawing2D;
using System.Globalization;
using GKLTNC.BUS;
using GKLTNC.Entity;

namespace GKLTNC
{
    public partial class Quan_Ly_Nhan_Vien : Form
    {
        // Tầng GUI CHỈ giao tiếp với tầng BUS (Nghiệp vụ), tuyệt đối không khởi tạo DbContext hay gọi DbSet/SaveChanges
        private readonly PhongBanBUS _phongBanBUS = new PhongBanBUS();
        private readonly NhanVienBUS _nhanVienBUS = new NhanVienBUS();

        // Cờ kiểm soát tránh hiển thị lặp hai lần hộp thoại đóng form
        private bool _isClosingConfirmed = false;

        #region Thuộc tính tương thích ngược (Compatibility Aliases)
        public TextBox textBox1 => txtMaNV;
        public TextBox textBox2 => txtHoTen;
        public TextBox textBox3 => txtDiaChi;
        public TextBox textBox4 => txtLuong;
        public ComboBox comboBox1 => cboPhongban;
        public RadioButton radioButton1 => rdbNam;
        public RadioButton radioButton2 => rdbNu;
        public DateTimePicker dateTimePicker2 => dtpNgaySinh;
        public Button button1 => btnMoi;
        public Button button2 => btnThem;
        public Button button3 => btnSua;
        public Button button4 => btnXoa;
        public Button button5 => btnTimKiem;
        public Button button6 => btnThoat;
        public DataGridView dataGridView1 => dgvNhanVien;
        #endregion

        public Quan_Ly_Nhan_Vien()
        {
            InitializeComponent();
            InitializeButtonIcons();
        }

        #region Khởi tạo Biểu tượng (Icons) cho Buttons
        private void InitializeButtonIcons()
        {
            btnMoi.Image = CreateIcon("refresh");     // Làm mới
            btnThem.Image = CreateIcon("add");         // Thêm
            btnSua.Image = CreateIcon("edit");        // Sửa
            btnXoa.Image = CreateIcon("delete");      // Xóa
            btnTimKiem.Image = CreateIcon("search");  // Tìm kiếm
            btnThoat.Image = CreateIcon("exit");      // Thoát
        }

        private Image CreateIcon(string type)
        {
            Bitmap bmp = new Bitmap(18, 18);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.Clear(Color.Transparent);

                switch (type)
                {
                    case "refresh": // Mũi tên vòng xoay làm mới xanh dương
                        using (Pen p = new Pen(Color.FromArgb(2, 132, 199), 2f))
                        {
                            g.DrawArc(p, 2, 2, 13, 13, 35, 275);
                            PointF[] arrow = new PointF[] { new PointF(10.5f, 1f), new PointF(15f, 4.5f), new PointF(10.5f, 8f) };
                            using (Brush b = new SolidBrush(Color.FromArgb(2, 132, 199)))
                                g.FillPolygon(b, arrow);
                        }
                        break;

                    case "add": // Dấu cộng hình tròn xanh lá
                        using (Brush b = new SolidBrush(Color.FromArgb(22, 163, 74)))
                        {
                            g.FillEllipse(b, 1, 1, 16, 16);
                        }
                        using (Pen p = new Pen(Color.White, 2.2f))
                        {
                            g.DrawLine(p, 4.5f, 9f, 13.5f, 9f);
                            g.DrawLine(p, 9f, 4.5f, 9f, 13.5f);
                        }
                        break;

                    case "edit": // Cây bút chì chỉnh sửa màu cam
                        using (Pen p = new Pen(Color.FromArgb(217, 119, 6), 2f))
                        {
                            g.DrawLine(p, 3.5f, 14.5f, 12.5f, 5.5f);
                            g.DrawLine(p, 12.5f, 5.5f, 14.5f, 7.5f);
                            g.DrawLine(p, 14.5f, 7.5f, 5.5f, 16.5f);
                            g.DrawLine(p, 5.5f, 16.5f, 1.5f, 16.5f);
                            g.DrawLine(p, 1.5f, 16.5f, 1.5f, 12.5f);
                            g.DrawLine(p, 1.5f, 12.5f, 3.5f, 14.5f);
                        }
                        break;

                    case "delete": // Thùng rác xóa màu đỏ
                        using (Pen p = new Pen(Color.FromArgb(220, 38, 38), 1.8f))
                        {
                            g.DrawLine(p, 2, 5, 16, 5);       // nắp ngang
                            g.DrawLine(p, 6, 2.5f, 12, 2.5f); // quai nắp
                            g.DrawLine(p, 4.5f, 5, 5.5f, 16); // cạnh trái
                            g.DrawLine(p, 13.5f, 5, 12.5f, 16); // cạnh phải
                            g.DrawLine(p, 5.5f, 16, 12.5f, 16); // đáy
                            g.DrawLine(p, 7.5f, 7.5f, 7.5f, 13.5f); // vạch trong 1
                            g.DrawLine(p, 10.5f, 7.5f, 10.5f, 13.5f); // vạch trong 2
                        }
                        break;

                    case "search": // Kính lúp tìm kiếm xanh navy
                        using (Pen p = new Pen(Color.FromArgb(37, 99, 235), 2f))
                        {
                            g.DrawEllipse(p, 2, 2, 9.5f, 9.5f);
                            g.DrawLine(p, 9.5f, 9.5f, 15.5f, 15.5f);
                        }
                        break;

                    case "exit": // Cửa mở và mũi tên đăng xuất màu đỏ
                        using (Pen p = new Pen(Color.FromArgb(185, 28, 28), 1.8f))
                        {
                            g.DrawRectangle(p, 2.5f, 2, 7.5f, 14); // khung cửa
                            g.DrawLine(p, 6.5f, 9, 15.5f, 9);      // mũi tên ngang
                            g.DrawLine(p, 12, 5.5f, 15.5f, 9);     // cánh trên mũi tên
                            g.DrawLine(p, 12, 12.5f, 15.5f, 9);    // cánh dưới mũi tên
                        }
                        break;
                }
            }
            return bmp;
        }
        #endregion

        #region Form Load & Data Binding
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Tải danh sách phòng ban từ BUS
                LoadPhongBanComboBox();

                // 2. Tải danh sách nhân viên từ BUS
                LoadNhanVienGridView();

                // 3. Khôi phục trạng thái form ban đầu
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}\n\nVui lòng kiểm tra dịch vụ MySQL đang chạy và chuỗi kết nối trong App.config.",
                                "Lỗi khởi động", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nạp danh sách Phòng ban vào cboPhongban qua tầng BUS
        /// </summary>
        private void LoadPhongBanComboBox()
        {
            cboPhongban.Items.Clear();
            cboPhongban.Items.Add(new PhongBanItem("", "- Chọn phòng ban -"));

            try
            {
                var dsPhongBan = _phongBanBUS.GetAllPhongBan();
                foreach (var pb in dsPhongBan)
                {
                    cboPhongban.Items.Add(new PhongBanItem(pb.MaPB, pb.TenPB));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải danh sách phòng ban: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cboPhongban.SelectedIndex = 0;
        }

        /// <summary>
        /// Nạp danh sách Nhân viên lên dgvNhanVien qua tầng BUS
        /// </summary>
        private void LoadNhanVienGridView(List<NhanVien>? danhSach = null)
        {
            dgvNhanVien.Rows.Clear();

            try
            {
                var list = danhSach ?? _nhanVienBUS.GetAllNhanVien();
                foreach (var nv in list)
                {
                    int rowIndex = dgvNhanVien.Rows.Add(
                        nv.MaNV,
                        nv.HoTen,
                        nv.GioiTinh ? "Nam" : "Nữ",
                        nv.NgaySinh.ToString("dd/MM/yyyy"),
                        nv.DiaChi ?? "",
                        nv.Luong.ToString("#,##0"),
                        nv.PhongBan?.TenPB ?? nv.MaPB
                    );

                    // Lưu thực thể NhanVien vào Tag của dòng
                    dgvNhanVien.Rows[rowIndex].Tag = nv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải danh sách nhân viên: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Làm sạch dữ liệu, bỏ chọn dòng, đưa con trỏ về txtMaNV; bật Thêm, tắt Sửa/Xóa; cbo ở trạng thái chưa chọn
        /// </summary>
        private void ResetForm()
        {
            // 1. Làm sạch các ô nhập
            if (cboPhongban.Items.Count > 0)
                cboPhongban.SelectedIndex = 0; // Trở về "- Chọn phòng ban -"

            txtMaNV.Clear();
            txtHoTen.Clear();
            rdbNam.Checked = true;
            rdbNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
            txtDiaChi.Clear();
            txtLuong.Text = "0";

            // 2. Bỏ chọn dòng hiện tại trên dgvNhanVien
            dgvNhanVien.ClearSelection();
            dgvNhanVien.CurrentCell = null;

            // 3. Trạng thái nút bấm
            btnThem.Enabled = true;  // Bật nút Thêm
            btnSua.Enabled = false;  // Tắt nút Sửa
            btnXoa.Enabled = false;  // Tắt nút Xóa

            // 4. Cho phép nhập Mã NV và đưa con trỏ về ô txtMaNV
            txtMaNV.ReadOnly = false;
            txtMaNV.BackColor = Color.White;
            txtMaNV.Focus();
        }
        #endregion

        #region Chọn dòng DataGridView & Khóa ngoại ComboBox
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.Rows.Count)
                return;

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            // 1. Nạp dữ liệu từ dòng được chọn lên các ô nhập
            txtMaNV.Text = row.Cells["manv"].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells["hovaten"].Value?.ToString() ?? "";

            string gt = row.Cells["gioitinh"].Value?.ToString() ?? "";
            if (gt.Equals("Nữ", StringComparison.OrdinalIgnoreCase))
            {
                rdbNu.Checked = true;
            }
            else
            {
                rdbNam.Checked = true;
            }

            if (DateTime.TryParseExact(row.Cells["ngaysin"].Value?.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ns))
            {
                dtpNgaySinh.Value = ns;
            }
            else if (DateTime.TryParse(row.Cells["ngaysin"].Value?.ToString(), out DateTime nsFallback))
            {
                dtpNgaySinh.Value = nsFallback;
            }

            txtDiaChi.Text = row.Cells["diachi"].Value?.ToString() ?? "";
            txtLuong.Text = row.Cells["Luong"].Value?.ToString()?.Replace(",", "").Replace(".", "").Trim() ?? "0";

            // Chọn cboPhongban theo khóa ngoại MaPB
            if (row.Tag is NhanVien nvEntity && !string.IsNullOrEmpty(nvEntity.MaPB))
            {
                SelectPhongBanByMa(nvEntity.MaPB);
            }
            else
            {
                string pbTen = row.Cells["Phongban"].Value?.ToString() ?? "";
                SelectPhongBanByTen(pbTen);
            }

            // 2. Tắt nút Thêm, Bật nút Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            // 3. Không cho sửa Mã nhân viên (giữ nguyên khóa chính)
            txtMaNV.ReadOnly = true;
            txtMaNV.BackColor = Color.FromArgb(241, 245, 249);
        }

        private void SelectPhongBanByMa(string maPb)
        {
            for (int i = 0; i < cboPhongban.Items.Count; i++)
            {
                if (cboPhongban.Items[i] is PhongBanItem item && item.MaPB == maPb)
                {
                    cboPhongban.SelectedIndex = i;
                    return;
                }
            }
        }

        private void SelectPhongBanByTen(string tenPb)
        {
            for (int i = 0; i < cboPhongban.Items.Count; i++)
            {
                if (cboPhongban.Items[i] is PhongBanItem item && item.TenPB.Equals(tenPb, StringComparison.OrdinalIgnoreCase))
                {
                    cboPhongban.SelectedIndex = i;
                    return;
                }
            }
        }
        #endregion

        #region Các Thao Tác Chức Năng (CRUD qua tầng BUS)
        // Nút Mới (Làm mới)
        private void btnMoi_Click(object sender, EventArgs e)
        {
            LoadNhanVienGridView();
            ResetForm();
        }

        // Nút Thêm (Enter)
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra sơ bộ giao diện trước khi gửi xuống BUS
                if (cboPhongban.SelectedItem is not PhongBanItem selectedPb || string.IsNullOrEmpty(selectedPb.MaPB))
                {
                    MessageBox.Show("Vui lòng chọn phòng ban hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboPhongban.Focus();
                    return;
                }

                string rawSalary = txtLuong.Text.Replace(",", "").Replace(".", "").Trim();
                if (!decimal.TryParse(rawSalary, out decimal luong) || luong < 0)
                {
                    MessageBox.Show("Lương phải là một số không âm hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLuong.Focus();
                    return;
                }

                // Tạo Entity NhanVien
                NhanVien nv = new NhanVien
                {
                    MaNV = txtMaNV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdbNam.Checked,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text.Trim(),
                    Luong = luong,
                    MaPB = selectedPb.MaPB
                };

                // Gọi tầng BUS xử lý nghiệp vụ và lưu trữ qua DAL
                _nhanVienBUS.ThemNhanVien(nv);

                MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVienGridView();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thêm nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null || dgvNhanVien.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (cboPhongban.SelectedItem is not PhongBanItem selectedPb || string.IsNullOrEmpty(selectedPb.MaPB))
                {
                    MessageBox.Show("Vui lòng chọn phòng ban hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboPhongban.Focus();
                    return;
                }

                string rawSalary = txtLuong.Text.Replace(",", "").Replace(".", "").Trim();
                if (!decimal.TryParse(rawSalary, out decimal luong) || luong < 0)
                {
                    MessageBox.Show("Lương phải là một số không âm hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLuong.Focus();
                    return;
                }

                NhanVien nv = new NhanVien
                {
                    MaNV = txtMaNV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdbNam.Checked,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text.Trim(),
                    Luong = luong,
                    MaPB = selectedPb.MaPB
                };

                // Gọi tầng BUS cập nhật thông tin
                _nhanVienBUS.SuaNhanVien(nv);

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVienGridView();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null || dgvNhanVien.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ma = dgvNhanVien.CurrentRow.Cells["manv"].Value?.ToString() ?? "";
            string ten = dgvNhanVien.CurrentRow.Cells["hovaten"].Value?.ToString() ?? "";

            // Xác nhận trước khi xóa (Câu 4d)
            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên '{ten}' (Mã: {ma}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    // Gọi tầng BUS xóa nhân viên
                    _nhanVienBUS.XoaNhanVien(ma);

                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVienGridView();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi xóa nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Nút Tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) keyword = txtHoTen.Text.Trim();

            string? maPb = null;
            if (cboPhongban.SelectedItem is PhongBanItem selectedPb && !string.IsNullOrEmpty(selectedPb.MaPB))
            {
                maPb = selectedPb.MaPB;
            }

            try
            {
                // Gọi tầng BUS tìm kiếm nhân viên theo từ khóa và/hoặc phòng ban
                var ketQua = _nhanVienBUS.SearchNhanVien(keyword, maPb);

                if (ketQua.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào phù hợp với điều kiện tìm kiếm!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                LoadNhanVienGridView(ketQua);
                MessageBox.Show($"Tìm thấy {ketQua.Count} nhân viên phù hợp!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Thoát (ESC hoặc bấm nút)
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Sự kiện Form Closing để xác nhận thoát an toàn - Tránh hiển thị hai lần (Câu 4d)
        private void Quan_Ly_Nhan_Vien_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isClosingConfirmed) return;

            DialogResult dr = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát khỏi chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (dr != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                _isClosingConfirmed = true;
            }
        }
        #endregion

        #region Helper Class cho ComboBox Phòng Ban
        private class PhongBanItem
        {
            public string MaPB { get; set; }
            public string TenPB { get; set; }

            public PhongBanItem(string maPb, string tenPb)
            {
                MaPB = maPb;
                TenPB = tenPb;
            }

            public override string ToString()
            {
                return TenPB;
            }
        }
        #endregion
    }
}
