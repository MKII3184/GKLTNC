using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace GKLTNC.DAL
{
    public static class DatabaseConfiguration
    {
        public static string GetConnectionString()
        {
            // 1. Đọc từ tệp App.config theo đúng chuẩn .NET và yêu cầu của đề thi
            try
            {
                var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
                if (connSetting != null && !string.IsNullOrWhiteSpace(connSetting.ConnectionString))
                {
                    return connSetting.ConnectionString;
                }
            }
            catch
            {
                // Bỏ qua nếu môi trường chưa nạp App.config
            }

            // 2. Fallback đọc từ appsettings.json nếu có
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string configFile = Path.Combine(basePath, "appsettings.json");

                if (!File.Exists(configFile))
                {
                    string projectDir = Directory.GetCurrentDirectory();
                    if (File.Exists(Path.Combine(projectDir, "appsettings.json")))
                    {
                        basePath = projectDir;
                    }
                    else
                    {
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
                // Bỏ qua lỗi fallback
            }

            throw new InvalidOperationException("Không tìm thấy chuỗi kết nối 'DefaultConnection' trong tệp cấu hình App.config!");
        }
    }
}
