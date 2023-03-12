namespace LeagueOfLegends.Api.Persistence.NHibernate.Migrations;

public static class NHibernateMigrationsManager
{
    private static readonly string Path = new DirectoryInfo(Directory.GetCurrentDirectory())
        .Parent! // -> Api
        .Parent! // -> src
        .GetDirectories().First(d => d.Name == "Periphery")
        .GetDirectories().First(d => d.Name == "LeagueOfLegends.Api.Persistence")
        .GetDirectories().First(d => d.Name == "NHibernate")
        .GetDirectories().First(d => d.Name == "Migrations")
        .FullName + "/SqlMigrations";

    public static bool InitCreated() => new DirectoryInfo(Path).GetFiles().Any(d => d.Extension == ".sql");
    
    public static Action<string> InitMigration => x =>
    {
        using var stream = new FileStream($"{Path}/init.sql", FileMode.Append, FileAccess.Write);
        using var writer = new StreamWriter(stream);
        writer.Write($"{x}\n");
    };

    public static Action<string> UpdateMigration => x =>
    {
        var now = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
        using var stream = new FileStream($"{Path}/migration{now}.sql", FileMode.Append, FileAccess.Write);
        using var writer = new StreamWriter(stream);
        writer.Write(x + "\n");
    };
}