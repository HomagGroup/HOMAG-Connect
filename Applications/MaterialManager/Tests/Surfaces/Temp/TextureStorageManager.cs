using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Temp;

// TODO: Remove this temporary test implementation when a real one is available

/// <summary>
/// Manages uploading textures to Azure Blob Storage and generating SAS URIs or CDN endpoints.
/// </summary>
public class TextureStorageManager
{
    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureStorageManager" /> class.
    /// </summary>
    /// <param name="configuration">The storage configuration.</param>
    /// <param name="subscriptionId">The subscription identifier.</param>
    public TextureStorageManager(StorageConfiguration configuration, Guid subscriptionId)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        Section = string.Format(_SectionFormat, subscriptionId);

        if (!string.IsNullOrWhiteSpace(Configuration.BlobStorageConnectionString))
        {
            var blobServiceClient = new BlobServiceClient(Configuration.BlobStorageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_Container);

            containerClient.CreateIfNotExists();
            BlobContainerClient = containerClient;
        }
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// Storage configuration for the texture storage manager.
    /// </summary>
    public class StorageConfiguration
    {
        /// <summary>
        /// Blob storage connection string.
        /// </summary>
        public string? BlobStorageConnectionString { get; set; }

        /// <summary>
        /// Blob storage uri to set CDN endpoint.
        /// </summary>
        public string? BlobStorageEndpoint { get; set; }
    }

    #endregion

    #region Fields

    private const string _Container = "textures";
    private const string _SectionFormat = "subscriptions/{0}/textures/";

    #endregion

    #region Properties

    private BlobContainerClient? BlobContainerClient { get; }

    private StorageConfiguration Configuration { get; }

    private string Section { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Uploads a stream to the interim storage and returns a SAS or CDN URI.
    /// </summary>
    /// <param name="stream">The stream to upload.</param>
    /// <param name="blobName">The blob name.</param>
    /// <returns>The URI to access the uploaded blob.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the BlobContainerClient is not initialized.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the stream is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the blob name is null or empty.</exception>
    public virtual async Task<Uri?> Upload(Stream stream, string blobName)
    {
        if (BlobContainerClient == null)
        {
            throw new InvalidOperationException("BlobContainerClient is not initialized.");
        }

        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (string.IsNullOrWhiteSpace(blobName))
        {
            throw new ArgumentException("Blob name must not be null or empty.", nameof(blobName));
        }
       
        var blobPath = Path.Combine(Section, blobName);

        var blobClient = BlobContainerClient.GetBlobClient(blobPath);

        var extension = Path.GetExtension(blobName);
        var contentType = MimeTypes.GetMimeType(extension);

        var headers = new BlobHttpHeaders
        {
            ContentType = contentType,
            CacheControl = "public, max-age=31536000, immutable"
        };

        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = headers });

        var sasUri = GenerateSasUri(blobClient);

        if (!string.IsNullOrWhiteSpace(Configuration.BlobStorageEndpoint))
        {
            var blobStorageEndpoint = Configuration.BlobStorageEndpoint.TrimEnd('/');
            if (!string.IsNullOrWhiteSpace(blobStorageEndpoint))
            {
                return new Uri(blobStorageEndpoint + sasUri.PathAndQuery);
            }
        }

        return sasUri;
    }

    /// <summary>
    /// Generates a SAS URI for the given blob client.
    /// </summary>
    /// <param name="blobClient">The blob client.</param>
    /// <returns>The SAS URI.</returns>
    private static Uri GenerateSasUri(BlobClient blobClient)
    {
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
            ExpiresOn = DateTimeOffset.MaxValue
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        return blobClient.GenerateSasUri(sasBuilder);
    }

    #endregion
}