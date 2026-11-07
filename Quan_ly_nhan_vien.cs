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

        public Quan_Ly_Nhan_Vien()
        {
            InitializeComponent();
            InitializeButtonIcons();
        }

        #region Khởi tạo Biểu tượng (Icons) cho Buttons
        private void InitializeButtonIcons()
        {
            button1.Image = CreateIcon("refresh"); // Làm mới
            button2.Image = CreateIcon("add");     // Thêm
            button3.Image = CreateIcon("edit");    // Sửa
            button4.Image = CreateIcon("delete");  // Xóa
            button5.Image = CreateIcon("search");  // Tìm kiếm
            button6.Image = CreateIcon("exit");    // Thoát
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
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}\n\nVui lòng kiểm tra dịch vụ MySQL đang chạy và thông tin cấu hình trong appsettings.json.",
                                "Lỗi khởi động", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nạp danh sách Phòng ban vào ComboBox qua tầng BUS
        /// </summary>
        private void LoadPhongBanComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new PhongBanItem("", "- Chọn phòng ban -"));

            try
            {
                var dsPhongBan = _phongBanBUS.GetAllPhongBan();
                foreach (var pb in dsPhongBan)
                {
                    comboBox1.Items.Add(new PhongBanItem(pb.MaPB, pb.TenPB));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải danh sách phòng ban: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Nạp danh sách Nhân viên lên DataGridView qua tầng BUS
        /// </summary>
        private void LoadNhanVienGridView(List<NhanVien>? danhSach = null)
        {
            dataGridView1.Rows.Clear();

            try
            {
                var list = danhSach ?? _nhanVienBUS.GetAllNhanVien();
                foreach (var nv in list)
                {
                    int rowIndex = dataGridView1.Rows.Add(
                        nv.MaNV,
                        nv.HoTen,
                        nv.GioiTinh ? "Nam" : "Nữ",
                        nv.NgaySinh.ToString("dd/MM/yyyy"),
                        nv.DiaChi ?? "",
                        nv.Luong.ToString("#,##0"),
                        nv.PhongBan?.TenPB ?? nv.MaPB
                    );

                    // Lưu thực thể NhanVien vào Tag của dòng để dễ dàng truy xuất
                    dataGridView1.Rows[rowIndex].Tag = nv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải danh sách nhân viên: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Làm sạch dữ liệu, bỏ chọn dòng, bật nút Thêm, tắt Sửa/Xóa, đưa con trỏ về Mã NV
        /// </summary>
        private void ResetForm()
        {
            // 1. Làm sạch các ô nhập
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0; // Trở về "- Chọn phòng ban -"

            textBox1.Clear();
            textBox2.Clear();
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            dateTimePicker2.Value = DateTime.Now;
            textBox3.Clear();
            textBox4.Text = "0";

            // 2. Bỏ chọn dòng hiện tại trên DataGridView
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;

            // 3. Trạng thái nút bấm
            button2.Enabled = true;  // Bật nút Thêm
            button3.Enabled = false; // Tắt nút Sửa
            button4.Enabled = false; // Tắt nút Xóa

            // 4. Cho phép nhập Mã NV và đưa con trỏ về ô Mã NV
            textBox1.ReadOnly = false;
            textBox1.BackColor = Color.White;
            textBox1.Focus();
        }
        #endregion

        #region Chọn dòng DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            // 1. Nạp dữ liệu từ dòng được chọn lên các ô nhập
            textBox1.Text = row.Cells["manv"].Value?.ToString() ?? "";
            textBox2.Text = row.Cells["hovaten"].Value?.ToString() ?? "";

            string gt = row.Cells["gioitinh"].Value?.ToString() ?? "";
            if (gt.Equals("Nữ", StringComparison.OrdinalIgnoreCase))
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            if (DateTime.TryParseExact(row.Cells["ngaysin"].Value?.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ns))
            {
                dateTimePicker2.Value = ns;
            }
            else if (DateTime.TryParse(row.Cells["ngaysin"].Value?.ToString(), out DateTime nsFallback))
            {
                dateTimePicker2.Value = nsFallback;
            }

            textBox3.Text = row.Cells["diachi"].Value?.ToString() ?? "";
            textBox4.Text = row.Cells["Luong"].Value?.ToString()?.Replace(",", "").Replace(".", "").Trim() ?? "0";

            // Chọn phòng ban tương ứng
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
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;

            // 3. Không cho sửa Mã nhân viên
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.FromArgb(241, 245, 249);
        }

        private void SelectPhongBanByMa(string maPb)
        {
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i] is PhongBanItem item && item.MaPB == maPb)
                {
                    comboBox1.SelectedIndex = i;
                    return;
                }
            }
        }

        private void SelectPhongBanByTen(string tenPb)
        {
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i] is PhongBanItem item && item.TenPB.Equals(tenPb, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox1.SelectedIndex = i;
                    return;
                }
            }
        }
        #endregion

        #region Các Thao Tác Chức Năng (CRUD qua tầng BUS)
        // Nút Mới (Làm mới)
        private void button1_Click(object sender, EventArgs e)
        {
            LoadNhanVienGridView();
            ResetForm();
        }

        // Nút Thêm (Enter)
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra sơ bộ giao diện trước khi gửi xuống BUS
                if (comboBox1.SelectedItem is not PhongBanItem selectedPb || string.IsNullOrEmpty(selectedPb.MaPB))
                {
                    MessageBox.Show("Vui lòng chọn phòng ban hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.Focus();
                    return;
                }

                string rawSalary = textBox4.Text.Replace(",", "").Replace(".", "").Trim();
                if (!decimal.TryParse(rawSalary, out decimal luong) || luong < 0)
                {
                    MessageBox.Show("Lương phải là một số dương hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return;
                }

                // Tạo Entity NhanVien
                NhanVien nv = new NhanVien
                {
                    MaNV = textBox1.Text.Trim(),
                    HoTen = textBox2.Text.Trim(),
                    GioiTinh = radioButton1.Checked,
                    NgaySinh = dateTimePicker2.Value,
                    DiaChi = textBox3.Text.Trim(),
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
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (comboBox1.SelectedItem is not PhongBanItem selectedPb || string.IsNullOrEmpty(selectedPb.MaPB))
                {
                    MessageBox.Show("Vui lòng chọn phòng ban hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.Focus();
                    return;
                }

                string rawSalary = textBox4.Text.Replace(",", "").Replace(".", "").Trim();
                if (!decimal.TryParse(rawSalary, out decimal luong) || luong < 0)
                {
                    MessageBox.Show("Lương phải là một số dương hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return;
                }

                NhanVien nv = new NhanVien
                {
                    MaNV = textBox1.Text.Trim(),
                    HoTen = textBox2.Text.Trim(),
                    GioiTinh = radioButton1.Checked,
                    NgaySinh = dateTimePicker2.Value,
                    DiaChi = textBox3.Text.Trim(),
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ma = dataGridView1.CurrentRow.Cells["manv"].Value?.ToString() ?? "";
            string ten = dataGridView1.CurrentRow.Cells["hovaten"].Value?.ToString() ?? "";

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
        private void button5_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) keyword = textBox2.Text.Trim();

            string? maPb = null;
            if (comboBox1.SelectedItem is PhongBanItem selectedPb && !string.IsNullOrEmpty(selectedPb.MaPB))
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

        // Nút Thoát (ESC)
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Sự kiện Form Closing để xác nhận thoát an toàn
        private void Quan_Ly_Nhan_Vien_FormClosing(object sender, FormClosingEventArgs e)
        {
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
