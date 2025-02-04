using System.IO.Compression;

using HomagConnect.Base.Extensions;
using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Tests
{
    /// <summary />
    [TestClass]
    [TestCategory("DataExchange")]
    [TestCategory("DataExchange.Project.Read")]
    public class ProjectReadTests
    {
        /// <summary />
        public TestContext? TestContext { get; set; }

        /// <summary />
        [TestMethod]
        public void Project_ReadXml_Valid()
        {
            var assembly = typeof(ProjectReadTests).Assembly;
            var verName = typeof(ProjectReadTests).Namespace + ".TestData.project-01.xml";
            var stream = assembly.GetManifestResourceStream(verName);

            Assert.IsNotNull(stream);

            var p = Project.Load(stream);
            Assert.IsNotNull(p);

            // Project
            Assert.AreEqual("2", p.ContentVersion);
            Assert.AreEqual(1, p.Properties.Count);
            Assert.AreEqual("Name", p.Properties[0].Name);
            Assert.AreEqual("Project 01", p.Properties[0].Value);

            Assert.IsNotNull(p.Orders);

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
        public void Project_ReadZip_Valid()
        {
            using var zip = ZipFile.OpenRead("TestData/project-01.zip");

            var project = Project.Load(zip);
            
            Assert.IsNotNull(project);
            Assert.IsNotNull(project.Orders);

            Assert.AreEqual(1, project.Orders.Count);
            Assert.AreEqual(2, project.Orders[0].Entities.Count);

            project.Trace();
        }
    }
}