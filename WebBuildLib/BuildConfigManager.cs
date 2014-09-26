using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebBuildLib.Extensions;

namespace WebBuildLib
{
    public class BuildConfigManager
    {
        private BuildConfigs _buildConfigs;

        public BuildConfigs BuildConfigs
        {
            get { return _buildConfigs; }
        }
        public BuildConfigManager(string config_path)
        {
            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(config_path);
            _buildConfigs = xdoc.Deserialize<BuildConfigs>();         
        }
        public BuildConfig FindConfig(string build_type, string build_level, string build_name)
        {
            BuildConfig config = _buildConfigs.Where(x => x.Type.ToUpper() == build_type.ToUpper()).Where(x => x.Level.ToUpper() == build_level.ToUpper()).Where(x => x.Name.ToUpper() == build_name.ToUpper()).SingleOrDefault();
            return config;
        }
        //public BuildConfig FindConfig(string build_level, string build_name)
        //{
        //    BuildConfig config = _buildConfigs.Where(x => x.Level == build_level).Where(x => x.Name == build_name).SingleOrDefault();
        //    return config;
        //}
        public BuildConfigs FindConfig(string build_type, string build_level)
        {
            BuildConfigs configs = new BuildConfigs();
            List<BuildConfig> cfgs = _buildConfigs.Where(x=>x.Type.ToUpper() == build_type.ToUpper()).Where(x => x.Level.ToUpper() == build_level && x.IncludeInBulkPush == true).ToList<BuildConfig>();
            foreach (var cfg in cfgs)
            {
                configs.Add(cfg);
            }
            return configs;
        }
    }
}