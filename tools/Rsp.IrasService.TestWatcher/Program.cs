using System.Text.RegularExpressions;

static string FindRepoRoot()
{
    var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
    while (dir != null)
    {
        if (Directory.Exists(Path.Combine(dir.FullName, "src")) && Directory.Exists(Path.Combine(dir.FullName, "tests")))
            return dir.FullName;
        dir = dir.Parent;
    }
    return Directory.GetCurrentDirectory();
}

static string Sanitize(string name)
{
    var invalid = Path.GetInvalidFileNameChars();
    var cleaned = new string(name.Where(ch => !invalid.Contains(ch)).ToArray());
    // strip generics and spaces
    cleaned = Regex.Replace(cleaned, "[<> ,]", "");
    return cleaned;
}

var root = FindRepoRoot();
var srcRoot = Path.Combine(root, "src");
var testsRoot = Path.Combine(root, "tests", "UnitTests", "Rsp.IrasService.UnitTests");

Console.WriteLine($"Watching {srcRoot} for changes and generating tests in {testsRoot}");

var watcher = new FileSystemWatcher(srcRoot)
{
    IncludeSubdirectories = true,
    Filter = "*.cs",
    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
};

var changed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

void OnChange(object? s, FileSystemEventArgs e)
{
    if (!e.FullPath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)) return;
    lock (changed)
    {
        changed.Add(e.FullPath);
    }
}

watcher.Changed += OnChange;
watcher.Created += OnChange;
watcher.Renamed += (s, e) => OnChange(s, new FileSystemEventArgs(WatcherChangeTypes.Changed, Path.GetDirectoryName(e.FullPath)!, Path.GetFileName(e.FullPath)));
watcher.EnableRaisingEvents = true;

while (true)
{
    await Task.Delay(TimeSpan.FromMinutes(5));

    string[] toProcess;
    lock (changed)
    {
        toProcess = changed.ToArray();
        changed.Clear();
    }

    if (toProcess.Length == 0)
    {
        Console.WriteLine($"[{DateTime.Now:O}] No changes detected.");
        continue;
    }

    Console.WriteLine($"[{DateTime.Now:O}] Processing {toProcess.Length} changed files...");

    foreach (var file in toProcess)
    {
        try
        {
            var content = await File.ReadAllTextAsync(file);

            // Skip generated/obj files
            if (file.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar))
                continue;

            var nsMatch = Regex.Match(content, @"namespace\s+([\w\.]+);", RegexOptions.Multiline);
            var classMatch = Regex.Match(content, @"class\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*(?:\:|\{|where|$)", RegexOptions.Multiline);
            if (!nsMatch.Success || !classMatch.Success) continue;

            var ns = nsMatch.Groups[1].Value;
            var className = classMatch.Groups["name"].Value;

            // Extract public method signatures (exclude ctors, properties, records)
            var methodRegex = new Regex(@"public\s+(?:async\s+)?(?<ret>[A-Za-z_][A-ZaZ0-9_\.<>\[\]?]*)\s+(?<mname>[A-Za-z_][A-Za-z0-9_]*)\s*\(", RegexOptions.Multiline);
            var methodNames = methodRegex.Matches(content)
                .Select(m => m.Groups["mname"].Value)
                .Where(n => n != className) // exclude constructor-like matches
                .Distinct()
                .ToList();

            if (methodNames.Count == 0)
            {
                // Nothing to generate for classes without public methods
                continue;
            }

            // Determine test target area by convention
            string? testsSubFolder = null;
            if (ns.Contains(".WebApi.Controllers"))
                testsSubFolder = Path.Combine("Web", "Controllers", className + "Tests");
            else if (ns.Contains(".Application.CQRS.Handlers"))
            {
                if (ns.Contains(".QueryHandlers"))
                    testsSubFolder = Path.Combine("Application", "CQRS", "Handlers", "QueryHandlers", className + "Tests");
                else if (ns.Contains(".CommandHandlers"))
                    testsSubFolder = Path.Combine("Application", "CQRS", "Handlers", "CommandHandlers", className + "Tests");
                else
                    testsSubFolder = Path.Combine("Application", "CQRS", "Handlers", className + "Tests");
            }
            else if (ns.Contains(".Application.CQRS.Commands"))
                testsSubFolder = Path.Combine("Application", "CQRS", "Commands");
            else if (ns.Contains(".Application.CQRS.Queries"))
                testsSubFolder = Path.Combine("Application", "CQRS", "Queries");
            else if (ns.Contains(".Infrastructure"))
                testsSubFolder = Path.Combine("Infrastructure", className + "Tests");
            else if (ns.Contains(".Services"))
                testsSubFolder = Path.Combine("Services", className + "Tests");
            else if (ns.Contains(".Application"))
                testsSubFolder = Path.Combine("Application", className + "Tests");

            if (testsSubFolder == null) continue;

            var testDir = Path.Combine(testsRoot, testsSubFolder);
            Directory.CreateDirectory(testDir);

            var testNs = "Rsp.IrasService.UnitTests" + "." + testsSubFolder.Replace(Path.DirectorySeparatorChar, '.');
            var targetType = ns + "." + className;

            foreach (var m in methodNames)
            {
                var sanitizedMethod = Sanitize(m);
                var baseName = sanitizedMethod + "_AutoGenerated";
                var testFile = Path.Combine(testDir, $"{baseName}.cs");

                // Choose async or not based on method signature
                var isAsync = Regex.IsMatch(content, $@"public\\s+async\\s+.*\\s+{Regex.Escape(m)}\\s*\\(");
                var ret = isAsync ? "async Task" : "void";
                var awaitLine = isAsync ? "await Task.CompletedTask;" : string.Empty;

                var testCode = $$"""
                using System.Threading.Tasks;
                using Rsp.IrasService.UnitTests;
                using Xunit;

                namespace {{testNs}};

                public class {{baseName}} : TestServiceBase<{{targetType}}>
                {
                    [Fact(Skip = "Auto-generated placeholder. Replace with real tests following project patterns.")]
                    public {{ret}} Test()
                    {
                        {{awaitLine}}
                    }
                }
                """;

                if (!File.Exists(testFile))
                {
                    await File.WriteAllTextAsync(testFile, testCode);
                    Console.WriteLine($"Created: {testFile}");
                }
                else
                {
                    // Update content if it looks like an auto-generated file; otherwise, just touch timestamp
                    var existing = await File.ReadAllTextAsync(testFile);
                    if (existing.Contains("Auto-generated placeholder"))
                    {
                        await File.WriteAllTextAsync(testFile, testCode);
                        Console.WriteLine($"Refreshed: {testFile}");
                    }
                    else
                    {
                        File.SetLastWriteTimeUtc(testFile, DateTime.UtcNow);
                        Console.WriteLine($"Skipped (custom test present): {testFile}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {file}: {ex.Message}");
        }
    }
}
