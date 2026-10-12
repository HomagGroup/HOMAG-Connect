using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts.Extensions;

namespace HomagConnect.OrderManager.Tests.Extensions
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Extensions")]
    public sealed class AdditionalDataEntityExtensionsTests
    {
        private static readonly Uri _DownloadUri = new("https://example.com/file.jpg");
        private static readonly Uri _SmallUri = new("https://example.com/file.Small.jpg");
        private static readonly Uri _MediumUri = new("https://example.com/file.Medium.jpg");
        private static readonly Uri _LargeUri = new("https://example.com/file.Large.jpg");

        private static AdditionalDataEntity CreateEntity(Uri? downloadUri, params AdditionalDataPreview[] previews)
        {
            return new AdditionalDataEntity
            {
                DownloadUri = downloadUri,
                Previews = previews.Length > 0 ? new Collection<AdditionalDataPreview>([.. previews]) : null
            };
        }

        [TestMethod]
        public void GetPreview_AllSizesAvailable_ReturnsSmall()
        {
            var entity = CreateEntity(_DownloadUri,
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Large, Uri = _LargeUri },
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Medium, Uri = _MediumUri },
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Small, Uri = _SmallUri });

            Assert.AreEqual(_SmallUri, entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_SmallMissing_ReturnsMedium()
        {
            var entity = CreateEntity(_DownloadUri,
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Large, Uri = _LargeUri },
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Medium, Uri = _MediumUri });

            Assert.AreEqual(_MediumUri, entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_OnlyLarge_ReturnsLarge()
        {
            var entity = CreateEntity(_DownloadUri, new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Large, Uri = _LargeUri });

            Assert.AreEqual(_LargeUri, entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_PreviewWithoutUri_IsSkipped()
        {
            var entity = CreateEntity(_DownloadUri,
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Small },
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Small, Uri = new Uri(string.Empty, UriKind.Relative) },
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Large, Uri = _LargeUri });

            Assert.AreEqual(_LargeUri, entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_OnlyUnsupportedSizes_ReturnsDownloadUri()
        {
            var entity = CreateEntity(_DownloadUri,
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Thumbnail, Uri = _SmallUri },
                new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Unknown, Uri = _SmallUri });

            Assert.AreEqual(_DownloadUri, entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_NoPreviews_ReturnsDownloadUri()
        {
            var entity = CreateEntity(_DownloadUri);

            Assert.AreEqual(_DownloadUri, entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_NothingSet_ReturnsNull()
        {
            var entity = CreateEntity(null, new AdditionalDataPreview { Size = AdditionalDataPreviewSize.Small });

            Assert.IsNull(entity.GetPreview());
        }

        [TestMethod]
        public void GetPreview_NullEntity_ReturnsNull()
        {
            AdditionalDataEntity? entity = null;

            Assert.IsNull(entity.GetPreview());
        }
    }
}
