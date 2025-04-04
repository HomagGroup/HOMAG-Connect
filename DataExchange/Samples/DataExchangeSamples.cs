using System.Runtime.CompilerServices;

using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Wrapper;

namespace HomagConnect.DataExchange.Samples
{
#pragma warning disable S1199
    /// <summary>
    /// Provides sample data exchange files.
    /// </summary>
    public static class DataExchangeSamples
    {
        /// <summary>
        /// Provides a sample project.zip file with typical properties.
        /// </summary>
        /// <returns></returns>
        public static FileInfo GetProjectHavingTypicalProperties()
        {
            var testDataFolder = new DirectoryInfo(@"Data\\project-02");

            var project = new ProjectWrapper();
            var projectFiles = new Dictionary<string, FileInfo>();

            project.Name = "Project 01";
            project.Source = "smartWOP";

            var order = new OrderWrapper();

            order.OrderNumber = "101";
            order.OrderName = "Sample order";
            order.OrderDescription = "Sample order description";
            order.CustomerNumber = "201";
            order.CustomerName = "Max Mustermann";
            order.Company = "Mustermann GmbH";
            order.OrderDate = DateTime.Today;
            order.DeliveryDatePlanned = DateTime.Today.AddDays(10);

            order.Properties.Add(new Param { Name = "MyCustomProperty", Value = "MyCustomValue" });

            project.Orders.Add(order);
            {
                var image = new ImageWrapper();

                const string name = "Cabinet";
                const string fileName = name + ".png";
                const string reference = @"images\" + fileName;

                image.Category = "Scene";
                image.Description = name;
                image.OriginalFileName = fileName;
                image.ImageLinkPicture = new Uri(reference, UriKind.Relative);

                projectFiles.Add(reference, new FileInfo(testDataFolder + @"\" + fileName));

                order.Images.Add(image);
            }

            {
                var image = new ImageWrapper();

                const string name = "Cabinet xray";
                const string fileName = name + ".jpg";
                const string reference = @"images\" + fileName;

                image.Category = "AssemblyXRayImage";
                image.Description = name;
                image.OriginalFileName = fileName;
                image.ImageLinkPicture = new Uri(reference, UriKind.Relative); 

                projectFiles.Add(reference, new FileInfo(testDataFolder + @"\" + fileName));

                order.Images.Add(image);
            }

            var zipFile = GetTempFileName();

            project.Save(zipFile, projectFiles);

            return zipFile;
        }

        private static DirectoryInfo GetTempDirectory([CallerMemberName] string callerMemberName = "")
        {
            var directoryInfo = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "HomagConnect", callerMemberName, Guid.NewGuid().ToString()));

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            return directoryInfo;
        }

        private static FileInfo GetTempFileName(string fileName = "project.zip", [CallerMemberName] string callerMemberName = "")
        {
            return new FileInfo(Path.Combine(GetTempDirectory(callerMemberName).FullName, fileName));
        }
    }
#pragma warning restore S1199
}