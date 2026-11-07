namespace GKLTNC
{
    partial class Quan_Ly_Nhan_Vien
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label10 = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            cboPhongban = new ComboBox();
            label3 = new Label();
            txtMaNV = new TextBox();
            label4 = new Label();
            txtHoTen = new TextBox();
            label5 = new Label();
            rdbNam = new RadioButton();
            rdbNu = new RadioButton();
            label8 = new Label();
            dtpNgaySinh = new DateTimePicker();
            label6 = new Label();
            txtDiaChi = new TextBox();
            label7 = new Label();
            txtLuong = new TextBox();
            groupBox2 = new GroupBox();
            btnMoi = new Button();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnTimKiem = new Button();
            btnThoat = new Button();
            groupBox3 = new GroupBox();
            dgvNhanVien = new DataGridView();
            manv = new DataGridViewTextBoxColumn();
            hovaten = new DataGridViewTextBoxColumn();
            gioitinh = new DataGridViewTextBoxColumn();
            ngaysin = new DataGridViewTextBoxColumn();
            diachi = new DataGridViewTextBoxColumn();
            Luong = new DataGridViewTextBoxColumn();
            Phongban = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
            SuspendLayout();
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label10.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(15, 23, 42);
            label10.Location = new Point(20, 12);
            label10.Name = "label10";
            label10.Size = new Size(924, 38);
            label10.TabIndex = 99;
            label10.Text = "QUẢN LÝ NHÂN VIÊN THEO PHÒNG BAN";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cboPhongban);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtMaNV);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtHoTen);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(rdbNam);
            groupBox1.Controls.Add(rdbNu);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(dtpNgaySinh);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtDiaChi);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtLuong);
            groupBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(30, 41, 59);
            groupBox1.Location = new Point(25, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(735, 245);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = " Thông tin nhân viên ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(25, 38);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 99;
            label2.Text = "Phòng ban:";
            // 
            // cboPhongban
            // 
            cboPhongban.Font = new Font("Segoe UI", 9F);
            cboPhongban.FormattingEnabled = true;
            cboPhongban.Location = new Point(135, 35);
            cboPhongban.Name = "cboPhongban";
            cboPhongban.Size = new Size(215, 28);
            cboPhongban.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(25, 78);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 99;
            label3.Text = "Mã nhân viên:";
            // 
            // txtMaNV
            // 
            txtMaNV.Font = new Font("Segoe UI", 9F);
            txtMaNV.Location = new Point(135, 75);
            txtMaNV.Name = "txtMaNV";
            txtMaNV.Size = new Size(215, 27);
            txtMaNV.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(25, 118);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 99;
            label4.Text = "Họ và tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 9F);
            txtHoTen.Location = new Point(135, 115);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(215, 27);
            txtHoTen.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(25, 158);
            label5.Name = "label5";
            label5.Size = new Size(68, 20);
            label5.TabIndex = 99;
            label5.Text = "Giới tính:";
            // 
            // rdbNam
            // 
            rdbNam.AutoSize = true;
            rdbNam.Checked = true;
            rdbNam.Font = new Font("Segoe UI", 9F);
            rdbNam.ForeColor = Color.Black;
            rdbNam.Location = new Point(135, 156);
            rdbNam.Name = "rdbNam";
            rdbNam.Size = new Size(62, 24);
            rdbNam.TabIndex = 3;
            rdbNam.TabStop = true;
            rdbNam.Text = "Nam";
            rdbNam.UseVisualStyleBackColor = true;
            // 
            // rdbNu
            // 
            rdbNu.AutoSize = true;
            rdbNu.Font = new Font("Segoe UI", 9F);
            rdbNu.ForeColor = Color.Black;
            rdbNu.Location = new Point(215, 156);
            rdbNu.Name = "rdbNu";
            rdbNu.Size = new Size(50, 24);
            rdbNu.TabIndex = 4;
            rdbNu.Text = "Nữ";
            rdbNu.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(25, 198);
            label8.Name = "label8";
            label8.Size = new Size(77, 20);
            label8.TabIndex = 99;
            label8.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Font = new Font("Segoe UI", 9F);
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(135, 195);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(215, 27);
            dtpNgaySinh.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(380, 78);
            label6.Name = "label6";
            label6.Size = new Size(58, 20);
            label6.TabIndex = 99;
            label6.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            txtDiaChi.Font = new Font("Segoe UI", 9F);
            txtDiaChi.Location = new Point(455, 75);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(255, 27);
            txtDiaChi.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(380, 118);
            label7.Name = "label7";
            label7.Size = new Size(54, 20);
            label7.TabIndex = 99;
            label7.Text = "Lương:";
            // 
            // txtLuong
            // 
            txtLuong.Font = new Font("Segoe UI", 9F);
            txtLuong.Location = new Point(455, 115);
            txtLuong.Name = "txtLuong";
            txtLuong.Size = new Size(255, 27);
            txtLuong.TabIndex = 7;
            txtLuong.Text = "0";
            txtLuong.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(btnMoi);
            groupBox2.Controls.Add(btnThem);
            groupBox2.Controls.Add(btnSua);
            groupBox2.Controls.Add(btnXoa);
            groupBox2.Controls.Add(btnTimKiem);
            groupBox2.Controls.Add(btnThoat);
            groupBox2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            groupBox2.ForeColor = Color.FromArgb(30, 41, 59);
            groupBox2.Location = new Point(780, 60);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(160, 245);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = " Chức năng ";
            // 
            // btnMoi
            // 
            btnMoi.BackColor = Color.White;
            btnMoi.Font = new Font("Segoe UI", 9F);
            btnMoi.ForeColor = Color.Black;
            btnMoi.ImageAlign = ContentAlignment.MiddleLeft;
            btnMoi.Location = new Point(16, 25);
            btnMoi.Name = "btnMoi";
            btnMoi.Padding = new Padding(8, 0, 0, 0);
            btnMoi.Size = new Size(128, 32);
            btnMoi.TabIndex = 0;
            btnMoi.Text = "Làm mới";
            btnMoi.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMoi.UseVisualStyleBackColor = false;
            btnMoi.Click += btnMoi_Click;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.White;
            btnThem.Font = new Font("Segoe UI", 9F);
            btnThem.ForeColor = Color.Black;
            btnThem.ImageAlign = ContentAlignment.MiddleLeft;
            btnThem.Location = new Point(16, 61);
            btnThem.Name = "btnThem";
            btnThem.Padding = new Padding(8, 0, 0, 0);
            btnThem.Size = new Size(128, 32);
            btnThem.TabIndex = 1;
            btnThem.Text = "Thêm";
            btnThem.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.White;
            btnSua.Font = new Font("Segoe UI", 9F);
            btnSua.ForeColor = Color.Black;
            btnSua.ImageAlign = ContentAlignment.MiddleLeft;
            btnSua.Location = new Point(16, 97);
            btnSua.Name = "btnSua";
            btnSua.Padding = new Padding(8, 0, 0, 0);
            btnSua.Size = new Size(128, 32);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.White;
            btnXoa.Font = new Font("Segoe UI", 9F);
            btnXoa.ForeColor = Color.Black;
            btnXoa.ImageAlign = ContentAlignment.MiddleLeft;
            btnXoa.Location = new Point(16, 133);
            btnXoa.Name = "btnXoa";
            btnXoa.Padding = new Padding(8, 0, 0, 0);
            btnXoa.Size = new Size(128, 32);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.White;
            btnTimKiem.Font = new Font("Segoe UI", 9F);
            btnTimKiem.ForeColor = Color.Black;
            btnTimKiem.ImageAlign = ContentAlignment.MiddleLeft;
            btnTimKiem.Location = new Point(16, 169);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Padding = new Padding(8, 0, 0, 0);
            btnTimKiem.Size = new Size(128, 32);
            btnTimKiem.TabIndex = 4;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.FromArgb(254, 242, 242);
            btnThoat.Font = new Font("Segoe UI", 9F);
            btnThoat.ForeColor = Color.FromArgb(185, 28, 28);
            btnThoat.ImageAlign = ContentAlignment.MiddleLeft;
            btnThoat.Location = new Point(16, 205);
            btnThoat.Name = "btnThoat";
            btnThoat.Padding = new Padding(8, 0, 0, 0);
            btnThoat.Size = new Size(128, 32);
            btnThoat.TabIndex = 5;
            btnThoat.Text = "Thoát";
            btnThoat.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(dgvNhanVien);
            groupBox3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            groupBox3.ForeColor = Color.FromArgb(30, 41, 59);
            groupBox3.Location = new Point(25, 315);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(915, 340);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = " Danh sách nhân viên ";
            // 
            // dgvNhanVien
            // 
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.AllowUserToDeleteRows = false;
            dgvNhanVien.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dgvNhanVien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.BackgroundColor = Color.White;
            dgvNhanVien.BorderStyle = BorderStyle.Fixed3D;
            dgvNhanVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(230, 238, 248);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(230, 238, 248);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvNhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvNhanVien.ColumnHeadersHeight = 32;
            dgvNhanVien.Columns.AddRange(new DataGridViewColumn[] { manv, hovaten, gioitinh, ngaysin, diachi, Luong, Phongban });
            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.GridColor = Color.FromArgb(226, 232, 240);
            dgvNhanVien.Location = new Point(15, 28);
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.Name = "dgvNhanVien";
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.RowHeadersVisible = false;
            dgvNhanVien.RowHeadersWidth = 51;
            dgvNhanVien.RowTemplate.Height = 28;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.Size = new Size(885, 295);
            dgvNhanVien.TabIndex = 0;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            // 
            // manv
            // 
            manv.FillWeight = 80F;
            manv.HeaderText = "Mã NV";
            manv.MinimumWidth = 6;
            manv.Name = "manv";
            manv.ReadOnly = true;
            // 
            // hovaten
            // 
            hovaten.FillWeight = 140F;
            hovaten.HeaderText = "Họ và tên";
            hovaten.MinimumWidth = 6;
            hovaten.Name = "hovaten";
            hovaten.ReadOnly = true;
            // 
            // gioitinh
            // 
            gioitinh.FillWeight = 65F;
            gioitinh.HeaderText = "Giới tính";
            gioitinh.MinimumWidth = 6;
            gioitinh.Name = "gioitinh";
            gioitinh.ReadOnly = true;
            // 
            // ngaysin
            // 
            ngaysin.FillWeight = 85F;
            ngaysin.HeaderText = "Ngày sinh";
            ngaysin.MinimumWidth = 6;
            ngaysin.Name = "ngaysin";
            ngaysin.ReadOnly = true;
            // 
            // diachi
            // 
            diachi.FillWeight = 140F;
            diachi.HeaderText = "Địa chỉ";
            diachi.MinimumWidth = 6;
            diachi.Name = "diachi";
            diachi.ReadOnly = true;
            // 
            // Luong
            // 
            Luong.FillWeight = 85F;
            Luong.HeaderText = "Lương";
            Luong.MinimumWidth = 6;
            Luong.Name = "Luong";
            Luong.ReadOnly = true;
            // 
            // Phongban
            // 
            Phongban.FillWeight = 105F;
            Phongban.HeaderText = "Phòng ban";
            Phongban.MinimumWidth = 6;
            Phongban.Name = "Phongban";
            Phongban.ReadOnly = true;
            // 
            // Quan_Ly_Nhan_Vien
            // 
            AcceptButton = btnThem;
            AccessibleName = "Quan_ly_nhan_vien";
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            CancelButton = btnThoat;
            ClientSize = new Size(964, 670);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label10);
            KeyPreview = true;
            MinimumSize = new Size(900, 600);
            Name = "Quan_Ly_Nhan_Vien";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý nhân viên – 24110903 – Nguyễn Hoàng Minh";
            FormClosing += Quan_Ly_Nhan_Vien_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label10;
        private GroupBox groupBox1;
        private Label label2;
        private ComboBox cboPhongban;
        private Label label3;
        private TextBox txtMaNV;
        private Label label4;
        private TextBox txtHoTen;
        private Label label5;
        private RadioButton rdbNam;
        private RadioButton rdbNu;
        private Label label8;
        private DateTimePicker dtpNgaySinh;
        private Label label6;
        private TextBox txtDiaChi;
        private Label label7;
        private TextBox txtLuong;
        private GroupBox groupBox2;
        private Button btnMoi;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnTimKiem;
        private Button btnThoat;
        private GroupBox groupBox3;
        private DataGridView dgvNhanVien;
        private DataGridViewTextBoxColumn manv;
        private DataGridViewTextBoxColumn hovaten;
        private DataGridViewTextBoxColumn gioitinh;
        private DataGridViewTextBoxColumn ngaysin;
        private DataGridViewTextBoxColumn diachi;
        private DataGridViewTextBoxColumn Luong;
        private DataGridViewTextBoxColumn Phongban;
    }
}
