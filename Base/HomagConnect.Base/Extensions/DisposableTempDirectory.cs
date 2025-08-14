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
    /// Creates a new temporary directory with a unique name.
    /// </summary>
    private DisposableTempDirectory()
    {
        var randomPath = Path.Combine(
            Path.GetTempPath(),
            Path.GetRandomFileName());

        FullName = Directory.CreateDirectory(randomPath).FullName;
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
        RecursivelyDelete();
    }

    /// <summary>
    /// Creates a new instance of <see cref="DisposableTempDirectory" />.
    /// </summary>
    /// <returns></returns>
    public static DisposableTempDirectory Create()
    {
        return new DisposableTempDirectory();
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
            Console.WriteLine(ex);
        }
    }
}