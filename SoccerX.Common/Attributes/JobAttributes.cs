using System.Resources;
using SoccerX.Common.Enums;
using SoccerX.Common.Properties;

namespace SoccerX.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class JobAttributes: Attribute
    {
        #region Field
        public JobKeyEnum JobKey { get; }
        public JobCategoryEnum JobCategory { get; }
        public string JobName { get; }
        public string JobDescription => GetResource();
        public Type JobCriteria { get; }
        public bool IsVisible { get; }
        private string JobDescriptionKey { get; }
        #endregion

        #region Constructor
        public JobAttributes(JobKeyEnum jobKey, JobCategoryEnum jobCategory, string jobName, string jobDescriptionKey, Type jobCriteria, bool isVisible)
        {
            JobKey = jobKey;
            JobName = jobName;
            JobCriteria = jobCriteria;
            IsVisible = isVisible;
            JobCategory = jobCategory;
            JobDescriptionKey = jobDescriptionKey;
        }

        #endregion

        #region Public Method
        #endregion

        #region Private Method
        private string GetResource()
        {
            var assembly = typeof(SoccerX.Common.Properties.Resources).Assembly;
            foreach (var name in assembly.GetManifestResourceNames())
            {
                Console.WriteLine(name);
            }


            var rm = new ResourceManager(typeof(Resources).FullName, typeof(Resources).Assembly);
            return rm.GetString(JobDescriptionKey) ?? $"[{JobDescriptionKey}]";
            //return new ResourceManager(typeof(Resources))?.GetString(JobDescriptionKey) ?? ; ;
        }

        private static ResourceManager ResourceManager =>
            new ResourceManager("SoccerX.Common.Properties.Resources",
                typeof(JobAttributes).Assembly);
        #endregion
    }
}
