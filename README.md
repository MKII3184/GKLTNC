# HƯỚNG DẪN CÀI ĐẶT, KẾT NỐI VÀ CHẠY ỨNG DỤNG QUẢN LÝ NHÂN VIÊN (GKLTNC)

> **Thông tin sinh viên / Đồ án:**
> - **Tiêu đề Form:** `Quản lý nhân viên – 24110903 – Nguyễn Hoàng Minh`
> - **Nền tảng:** .NET 10 (`net10.0-windows`), C# Windows Forms
> - **Cơ sở dữ liệu:** MySQL Server 8.0
> - **ORM:** Entity Framework Core (Code-First với Migrations)
> - **Mô hình kiến trúc:** Three-Layer Architecture (GUI - BUS/BLL - DAL/DAO - Entity)

---

## 1. Cấu Trúc Kiến Trúc Dự Án (3 Tầng + 1 Entity)

Solution được tổ chức phân tách độc lập thành 4 project nhằm đảm bảo đúng chuẩn Three-Layer và nguyên tắc trách nhiệm duy nhất:

```text
C:\Users\24110\OneDrive\Desktop\DA\GKLTNC\
├── GKLTNC.slnx                      # File Solution quản lý 4 project
├── App.config                       # Cấu hình kết nối MySQL cho tầng GUI
├── appsettings.json                 # Cấu hình JSON bổ trợ
│
├── GKLTNC.Entity\                   # TẦNG THỰC THỂ (Domain Models)
│   ├── PhongBan.cs                  # Entity Phòng ban (Data Annotations, quan hệ 1-nhiều)
│   └── NhanVien.cs                  # Entity Nhân viên (Data Annotations, khóa ngoại MaPB)
│
├── GKLTNC.DAL\                      # TẦNG TRUY CẬP DỮ LIỆU (Data Access Layer - DAO)
│   ├── AppDbContext.cs              # DbContext & DesignTime Factory cho EF Core Migrations
│   ├── DatabaseConfiguration.cs     # Đọc chuỗi kết nối từ App.config qua ConfigurationManager
│   ├── PhongBanDAL.cs               # Truy vấn bảng PhongBan
│   ├── NhanVienDAL.cs               # Thao tác CRUD bảng NhanVien qua EF Core
│   ├── App.config                   # Cấu hình kết nối phục vụ Migration
│   └── Migrations\                  # Các tệp Migration Code-First tự động sinh
│
├── GKLTNC.BUS\                      # TẦNG NGHIỆP VỤ (Business Logic Layer - BLL)
│   ├── PhongBanBUS.cs               # Nghiệp vụ xử lý danh mục phòng ban
│   └── NhanVienBUS.cs               # Nghiệp vụ nhân viên: kiểm tra trùng mã, tuổi >= 18, lương >= 0,...
│
└── GKLTNC\ (GUI)                    # TẦNG GIAO DIỆN (Windows Forms)
    ├── Quan_ly_nhan_vien.cs         # Xử lý giao diện (chỉ gọi BUS, KHÔNG đụng DbContext)
    ├── Quan_ly_nhan_vien.Designer.cs# Giao diện controls (txtMaNV, cboPhongban, dgvNhanVien,...)
    └── Program.cs                   # Điểm khởi chạy ứng dụng
```

**Luồng dữ liệu xử lý nghiêm ngặt:**
$$\text{GUI (WinForms)} \longrightarrow \text{BUS / BLL} \longrightarrow \text{DAL / DAO} \longrightarrow \text{MySQL Database}$$
- **GUI:** Tuyệt đối không khởi tạo `DbContext`, không gọi `DbSet` hay `SaveChanges`.
- **DAL:** Không sử dụng bất kỳ điều khiển WinForms nào (`System.Windows.Forms`).
- **BUS:** Chịu trách nhiệm toàn bộ logic nghiệp vụ, thẩm định dữ liệu trước khi chuyển xuống DAL.

---

## 2. Cấu Hình Kết Nối MySQL (Không Hardcode)

Chuỗi kết nối được lưu trữ tập trung trong tệp `App.config` ở thư mục gốc của project GUI (`GKLTNC`) và `GKLTNC.DAL`:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="DefaultConnection"
         connectionString="Server=localhost;Port=3306;Database=quanlynhanvien_db;User ID=root;Password=dmcvmkii28;"
         providerName="MySql.Data.MySqlClient" />
  </connectionStrings>
</configuration>
```

> [!NOTE]
> - **Server:** `localhost` (Port: `3306`)
> - **Tài khoản mặc định:** `root`
> - **Mật khẩu mặc định:** `dmcvmkii28`
> - **Tên cơ sở dữ liệu:** `quanlynhanvien_db`
>
> Nếu máy của bạn sử dụng mật khẩu hoặc cổng MySQL khác, bạn chỉ cần thay đổi giá trị trong tệp `App.config` mà không cần chỉnh sửa bất kỳ dòng mã nguồn C# nào.

---

## 3. Khởi Tạo Cơ Sở Dữ Liệu (Code-First Migrations)

Dự án áp dụng phương pháp **Entity Framework Core Code-First**, không sử dụng Database First.

> [!IMPORTANT]
> **Khởi tạo Danh mục Phòng ban (ComboBox):**
> Ứng dụng **không yêu cầu chức năng CRUD danh mục Phòng ban**. Danh mục Phòng ban đã được cấu hình tự động khởi tạo dữ liệu mẫu (Seed Data) trong Code-First Migration với 5 phòng ban chuẩn:
> - `PB01` – Phòng Kỹ thuật
> - `PB02` – Phòng Kế toán
> - `PB03` – Phòng Nhân sự
> - `PB04` – Phòng Kinh doanh
> - `PB05` – Phòng Hành chính
>
> Dữ liệu danh mục này được tự động nạp lên ComboBox (`cboPhongban`) khi mở ứng dụng để người dùng chọn phòng ban cho nhân viên.

### Các bước tạo cơ sở dữ liệu và bảng:

Mở PowerShell hoặc Command Prompt tại thư mục dự án và chạy lệnh:

```powershell
# Di chuyển vào thư mục DAL
cd "C:\Users\24110\OneDrive\Desktop\DA\GKLTNC\GKLTNC.DAL"

# Áp dụng migration để tạo database và các bảng trên MySQL
dotnet ef database update
```

*(Nếu làm việc trong Visual Studio Package Manager Console, chọn Default project là `GKLTNC.DAL` và chạy lệnh: `Update-Database`).*

Sau khi lệnh hoàn tất:
- Cơ sở dữ liệu `quanlynhanvien_db` sẽ được tạo trên MySQL.
- Bảng `phongban` và bảng `nhanvien` được tạo với đầy đủ khóa chính, khóa ngoại, độ dài chuỗi và kiểu dữ liệu `decimal(18,2)`.
- Dữ liệu phòng ban và các nhân viên mẫu ban đầu đã sẵn sàng trong MySQL.

---

## 4. Hướng Dẫn Chạy Ứng Dụng

### Cách 1: Sử dụng Visual Studio (Khuyên dùng)
1. Mở tệp Solution `GKLTNC.slnx` bằng Visual Studio 2022/2026.
2. Kiểm tra cửa sổ **Solution Explorer**, chuột phải vào project `GKLTNC` $\rightarrow$ chọn **Set as Startup Project**.
3. Nhấn phím **F5** (hoặc `Ctrl + F5`) để biên dịch và chạy ứng dụng.

### Cách 2: Sử dụng dòng lệnh .NET CLI
Mở terminal và thực thi lệnh sau:

```powershell
cd "C:\Users\24110\OneDrive\Desktop\DA\GKLTNC"
dotnet run --project GKLTNC.csproj
```

---

## 5. Quy Tắc Nghiệp Vụ & Hướng Dẫn Sử Dụng

### 5.1. Phím tắt và điều hướng
- **Phím `Enter`:** Tự động kích hoạt nút **Thêm** (`AcceptButton = btnThem`).
- **Phím `Esc`:** Tự động kích hoạt nút **Thoát** (`CancelButton = btnThoat`).
- **Phím `Tab`:** Di chuyển con trỏ theo thứ tự logic:
  $$\text{cboPhongban} \rightarrow \text{txtMaNV} \rightarrow \text{txtHoTen} \rightarrow \text{rdbNam/rdbNu} \rightarrow \text{dtpNgaySinh} \rightarrow \text{txtDiaChi} \rightarrow \text{txtLuong} \rightarrow \text{Cụm nút} \rightarrow \text{Bảng dgvNhanVien}$$

### 5.2. Trạng thái Form Load và nút "Mới" (Làm mới)
- Xóa trắng toàn bộ các ô nhập dữ liệu, đưa mức lương về `0`.
- Đưa `cboPhongban` về trạng thái chưa chọn: `"- Chọn phòng ban -"`.
- Bỏ chọn dòng hiện tại trên `dgvNhanVien`.
- Mở khóa ô `txtMaNV` (`ReadOnly = false`, nền trắng) và tự động đưa con trỏ vào ô `txtMaNV`.
- **Bật** nút Thêm (`btnThem.Enabled = true`), **Tắt** nút Sửa và Xóa (`btnSua.Enabled = false`, `btnXoa.Enabled = false`).

### 5.3. Trạng thái khi chọn một dòng trên DataGridView
- Tự động điền đầy đủ thông tin dòng được chọn lên các ô nhập.
- Tự động chọn đúng phòng ban trên ComboBox theo **khóa ngoại `MaPB`**.
- **Khóa ô Mã nhân viên** (`txtMaNV.ReadOnly = true`, nền xám nhạt) nhằm giữ nguyên khóa chính khi chỉnh sửa.
- **Tắt** nút Thêm (`btnThem.Enabled = false`), **Bật** nút Sửa và Xóa (`btnSua.Enabled = true`, `btnXoa.Enabled = true`).

### 5.4. Thẩm định dữ liệu (Validation nghiệp vụ tại BUS)
- **Mã NV & Họ tên:** Không được để trống; tự động loại bỏ khoảng trắng đầu/cuối (`Trim()`).
- **Chống trùng mã:** Khi Thêm mới, BUS kiểm tra nếu mã đã tồn tại trong MySQL sẽ báo lỗi và không lưu.
- **Phòng ban:** Bắt buộc phải chọn một phòng ban hợp lệ từ danh sách (không để ở mục `"- Chọn phòng ban -"`).
- **Ngày sinh & Độ tuổi:** Ngày sinh không được lớn hơn ngày hiện tại; nhân viên bắt buộc phải **đủ 18 tuổi trở lên**.
- **Mức lương:** Phải là số hợp lệ không âm ($\ge 0$).

### 5.5. Hộp thoại xác nhận an toàn
- **Xác nhận khi xóa:** Luôn hiển thị hộp thoại xác nhận Yes/No trước khi xóa nhân viên khỏi hệ thống.
- **Xác nhận khi thoát:** Bấm nút **Thoát**, bấm phím **Esc** hoặc bấm nút **[X]** đóng cửa sổ đều hiển thị hộp thoại xác nhận:
  > *"Bạn có chắc chắn muốn thoát khỏi chương trình?"*
- Cơ chế kiểm soát cờ đảm bảo **không bao giờ bị hiển thị lặp hai lần** hộp thoại đóng form.

---

## 6. Lệnh Kiểm Tra Biên Dịch Nhanh

```powershell
cd "C:\Users\24110\OneDrive\Desktop\DA\GKLTNC"
dotnet build
# Kết quả: Build succeeded. 0 Warning(s), 0 Error(s)
```
