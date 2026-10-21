using System;
using System.IO;
using System.Threading;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Temporary directory that is automatically deleted when disposed.
/// </summary>
public class DisposableTempDirectory : IDisposable
{
    /// <summary>
    /// Name of the marker file written into every directory created by this class, so that orphaned
    /// directories can be safely identified and swept up later by <see cref="CleanupOrphanedDirectories" />
    /// without touching unrelated content in the temp folder.
    /// </summary>
    private const string MarkerFileName = ".homagconnect-disposable-temp-dir";

    private bool _disposed;

    /// <summary>
    /// Creates a new temporary directory with a unique name.
    /// </summary>
    private DisposableTempDirectory()
    {
        var randomPath = Path.Combine(
            Path.GetTempPath(),
            Path.GetRandomFileName());

        FullName = Directory.CreateDirectory(randomPath).FullName;

        try
        {
            File.WriteAllText(Path.Combine(FullName, MarkerFileName), DateTimeOffset.UtcNow.ToString("O"));
        }
        catch (IOException)
        {
            // Not fatal: the marker is only used for the best-effort orphan cleanup sweep.
        }
    }

    /// <summary>
    /// Finalizer acting as a safety net in case <see cref="Dispose" /> is never called (e.g. the owner
    /// forgot the <c>using</c> statement, or the process crashed before reaching it).
    /// </summary>
    ~DisposableTempDirectory()
    {
        RecursivelyDelete();
    }

    /// <summary>
    /// Gets the <see cref="DirectoryInfo" /> for the temporary directory.
    /// </summary>
    public DirectoryInfo DirectoryInfo
    {
        get
        {
            return new DirectoryInfo(FullName);
        }
    }

    /// <summary>
    /// Gets the path of the temporary directory.
    /// </summary>
    public string FullName { get; }

    /// <summary>
    /// Deletes the temporary directory and all its contents.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        RecursivelyDelete();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Creates a new instance of <see cref="DisposableTempDirectory" />.
    /// </summary>
    /// <returns></returns>
    public static DisposableTempDirectory Create()
    {
        return new DisposableTempDirectory();
    }

    /// <summary>
    /// Deletes directories previously created by <see cref="Create" /> that are still present under
    /// <see cref="Path.GetTempPath" /> because their owning <see cref="DisposableTempDirectory" /> instance
    /// was never disposed (e.g. a process crash). Only directories containing the marker file written by
    /// this class are touched, so unrelated temp content is left untouched. Call this on application
    /// startup to recover disk space leaked by previous runs.
    /// </summary>
    /// <param name="olderThan">
    /// Only directories whose marker file is older than this age are deleted. Defaults to one hour, so
    /// directories that are still legitimately in use by a running process are not removed.
    /// </param>
    public static void CleanupOrphanedDirectories(TimeSpan? olderThan = null)
    {
        var minAge = olderThan ?? TimeSpan.FromHours(1);
        var tempPath = Path.GetTempPath();

        DirectoryInfo[] candidates;

        try
        {
            candidates = new DirectoryInfo(tempPath).GetDirectories();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"Failed to enumerate temp directory '{tempPath}' for orphan cleanup: {ex}");

            return;
        }

        foreach (var candidate in candidates)
        {
            var markerFile = Path.Combine(candidate.FullName, MarkerFileName);

            if (!File.Exists(markerFile))
            {
                continue;
            }

            try
            {
                if (File.GetLastWriteTimeUtc(markerFile) > DateTimeOffset.UtcNow.Subtract(minAge))
                {
                    continue;
                }

                DeleteDirectoryWithRetry(candidate.FullName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete orphaned temp directory '{candidate.FullName}': {ex}");
            }
        }
    }

    private static void DeleteDirectoryWithRetry(string path, int maxRetries = 5, int delayMilliseconds = 500)
    {
        if (!Directory.Exists(path))
        {
            return;
        }

        for (var attempt = 0; attempt < maxRetries; attempt++)
        {
            try
            {
                Directory.Delete(path, recursive: true);
                return; // Success
            }
            catch (IOException) when (attempt < maxRetries - 1)
            {
                Thread.Sleep(delayMilliseconds);
            }
            catch (UnauthorizedAccessException) when (attempt < maxRetries - 1)
            {
                Thread.Sleep(delayMilliseconds);
            }
        }

        // Final attempt without catching
        Directory.Delete(path, recursive: true);
    }

    private void RecursivelyDelete()
    {
        try
        {
            DeleteDirectoryWithRetry(FullName);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temp directory '{FullName}': {ex}");
        }
    }
}