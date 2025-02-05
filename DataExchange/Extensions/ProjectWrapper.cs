using System.Collections.ObjectModel;

using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions;

namespace HomagConnect.DataExchange.Extensions
{
    /// <summary>
    /// Wrapper for the Project class.
    /// </summary>
    public class ProjectWrapper
    {
        #region Private Properties

        private Project Project { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the project wrapper.
        /// </summary>
        public ProjectWrapper()
        {
            Project = new Project();
        }

        /// <summary>
        /// Constructor for the project wrapper.
        /// </summary>
        public ProjectWrapper(Project project)
        {
            Project = project;
        }

        #endregion

        #region Public properties

        /// <summary />
        public string? Source
        {
            get
            {
                return Project.GetPropertyValue<string>();
            }
            set
            {
                Project.SetPropertyValue(value);
            }
        }

        /// <summary />

        public string? Name
        {
            get
            {
                return Project.GetPropertyValue<string>();
            }
            set
            {
                Project.SetPropertyValue(value);
            }
        }

        /// <summary />
        public IList<OrderWrapper> Orders
        {
            get
            {
                return new OrderWrapperList( Project.Orders);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Save project to project.zip file.
        /// </summary>
        public void Save(FileInfo projectZip, Dictionary<string, string>? projectFiles)
        {
            Project.Save(projectZip, projectFiles);
        }

        #endregion
    }

    

    
}