using System.IO.Compression;

using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Tests
{
    [TestClass]
    [TestCategory("DataExchange")]
    [TestCategory("DataExchange.Project.Read")]
    public class ProjectReadTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Project_ReadXml_Valid()
        {
            var assembly = typeof(ProjectReadTests).Assembly;
            var verName = typeof(ProjectReadTests).Namespace + ".TestData.project-01.xml";
            var stream = assembly.GetManifestResourceStream(verName);
            var p = Project.Load(stream);
            Assert.IsNotNull(p);

            // Project
            Assert.AreEqual("2", p.ContentVersion);
            Assert.AreEqual(1, p.Properties.Count);
            Assert.AreEqual("Name", p.Properties[0].Name);
            Assert.AreEqual("Project 01", p.Properties[0].Value);

            // Order
            Assert.AreEqual(1, p.Orders.Count);
            Assert.AreEqual(3, p.Orders[0].Images.Count);
            Assert.AreEqual(9, p.Orders[0].Properties.Count);
            Assert.AreEqual(1, p.Orders[0].Entities.Count);

            Assert.AreEqual(4, p.Orders[0].Images[1].Properties.Count);
            Assert.AreEqual("Description1", p.Orders[0].Images[1].Properties[1].Name);
            Assert.AreEqual("ART_XRAY-2", p.Orders[0].Images[1].Properties[1].Value);

            // Entity
            var e = p.Orders[0].Entities[0];
            Assert.AreEqual(13, e.Properties.Count);
            Assert.AreEqual(3, e.Images.Count);
            Assert.AreEqual(10, e.Entities.Count);

            Assert.AreEqual(5, e.Entities[0].Properties.Count);
            Assert.AreEqual("Typ4", e.Entities[0].Properties[0].Name);
            Assert.AreEqual("ProductionOrder4", e.Entities[0].Properties[0].Value);

            Assert.AreEqual(3, e.Entities[0].Images[0].Properties.Count);
            Assert.AreEqual("Categor2y", e.Entities[0].Images[0].Properties[0].Name);
            Assert.AreEqual("AboveImage2", e.Entities[0].Images[0].Properties[0].Value);
        }

        [TestMethod]
        public void Project_ReadZIP_Valid()
        {
            var zipFile = new FileInfo("TestData/project-01.zip");
            var projectXml = ExtractZipAndGetProjectXmlPath(zipFile);

            var project = Project.Load(projectXml);

            Assert.IsNotNull(project);

            Assert.AreEqual(1, project.Orders.Count);
            Assert.AreEqual(2, project.Orders[0].Entities.Count);
        }


        private FileInfo ExtractZipAndGetProjectXmlPath(FileInfo zipFile)
        {
            Assert.IsTrue(zipFile.Exists);

            Assert.IsNotNull(TestContext);
            Assert.IsNotNull(TestContext.TestRunDirectory);

            var extractDirectory = new DirectoryInfo(Path.Combine(TestContext.TestRunDirectory, @"Temp\TestData\project-01"));

            if (extractDirectory.Exists)
            {
                extractDirectory.Delete(true);
            }

            using var zip = ZipFile.OpenRead(zipFile.FullName);

            zip.ExtractToDirectory(extractDirectory.FullName);

            extractDirectory.Refresh();

            var projectXml = extractDirectory.EnumerateFiles("project.xml", SearchOption.AllDirectories).FirstOrDefault();

            TestContext.WriteLine("Working directory: " + extractDirectory.FullName);

            Assert.IsNotNull(projectXml);
            Assert.IsTrue(projectXml.Exists);

            return projectXml;
        }

        [TestMethod]
        public void TestSer02()
        {
            var p = new Project();
            var p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders.Count, p1.Orders.Count);

            p.Orders.Add(new Order());
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders.Count, p1.Orders.Count);

            p.Orders[0].Properties.Add(new Param());
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders[0].Properties.Count, p1.Orders[0].Properties.Count);

            var dt = DateTimeOffset.Now;
            p.Orders[0].Properties[0].SetValue(dt);
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders[0].Properties[0].Value, p1.Orders[0].Properties[0].Value);

            p.Orders[0].Properties[0].SetValue(12.3d);
            Assert.AreEqual("12.3", p.Orders[0].Properties[0].Value);
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders[0].Properties[0].Value, p1.Orders[0].Properties[0].Value);

            p.Orders[0].Properties[0].Name = "name1";
            Assert.AreEqual("name1", p.Orders[0].Properties[0].Name);
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders[0].Properties[0].Name, p1.Orders[0].Properties[0].Name);

            p.Orders[0].Images.Add(new Image());
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders[0].Images.Count, p1.Orders[0].Images.Count);

            p.Orders[0].Entities.Add(new Entity());
            p1 = Project.LoadFromString(p.SaveToString());
            Assert.AreEqual(p.Orders[0].Entities.Count, p1.Orders[0].Entities.Count);
        }
    }
}