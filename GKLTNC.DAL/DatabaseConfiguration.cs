using Microsoft.Extensions.Configuration;

namespace GKLTNC.DAL
{
    public static class DatabaseConfiguration
    {
        private const string DefaultConnectionString = "Server=localhost;Port=3306;Database=quanlynhanvien_db;User ID=root;Password=dmcvmkii28;";

        public static string GetConnectionString()
        {
            try
            {
                // Tìm kiếm appsettings.json ở thư mục chạy ứng dụng (AppDomain.CurrentDomain.BaseDirectory) hoặc thư mục hiện tại
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string configFile = Path.Combine(basePath, "appsettings.json");

                if (!File.Exists(configFile))
                {
                    // Nếu đang chạy trong môi trường phát triển hoặc migration, tìm ở thư mục project
                    string projectDir = Directory.GetCurrentDirectory();
                    if (File.Exists(Path.Combine(projectDir, "appsettings.json")))
                    {
                        basePath = projectDir;
                    }
                    else
                    {
                        // Kiểm tra trong thư mục GKLTNC.DAL
                        string dalDir = Path.Combine(projectDir, "GKLTNC.DAL");
                        if (File.Exists(Path.Combine(dalDir, "appsettings.json")))
                        {
                            basePath = dalDir;
                        }
                    }
                }

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string? connStr = configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrWhiteSpace(connStr))
                {
                    return connStr;
                }
            }
            catch
            {
                // Fallback nếu có lỗi đọc cấu hình
            }

            return DefaultConnectionString;
        }
    }
}
