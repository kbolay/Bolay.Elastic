using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Plugin
{
    public class PluginLoader
    {
        internal static List<IElasticIndexType> IndexTypes { get; set; }

        public static void AddIndexTypes(Assembly assembly)
        {
            IEnumerable<Type> plugins = assembly.GetTypes().Where(x => x is IElasticIndexType);
            if (plugins == null || !plugins.Any())
                return;

            if (IndexTypes == null)
                IndexTypes = new List<IElasticIndexType>();

            bool loadIntoAppDomain = false;
            
            foreach (Type plugin in plugins)
            {
                IElasticIndexType indexType = plugin as IElasticIndexType;
                if (indexType.TargetType == null)
                    throw new Exception("Plugin does not have a target type.");

                IndexTypes.Add(indexType);
                loadIntoAppDomain = true;
            }

            if (loadIntoAppDomain)
                AppDomain.CurrentDomain.Load(assembly.FullName);
        }

        public static void ScanDirectoryForIndexTypes(string directory)
        {
            // scan directory for *.dll
            string[] dllFiles = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);            

            // load assemblies into the domain
            foreach (string dllFile in dllFiles)
            {
                Assembly assembly = Assembly.LoadFile(dllFile);
                AddIndexTypes(assembly);                
            }
        }

        public static IElasticIndexType GetIndex(Type type)
        {
            // iterate through Indexes and find where type is of type T
            if(IndexTypes == null || !IndexTypes.Any())
                return null;

            IEnumerable<IElasticIndexType> matchingTypes = IndexTypes.Where(x => x.TargetType == type);
            if (matchingTypes.Count() == 0)
                return null;
            if (matchingTypes.Count() > 1)
                throw new Exception("More than one plugin matching this type.");

            return matchingTypes.First();
        }

        public static IElasticIndexType GetIndex(string typeName)
        {
            // iterate through Indexes and find where type is of type T
            if (IndexTypes == null || !IndexTypes.Any())
                return null;

            IEnumerable<IElasticIndexType> matchingTypes = IndexTypes.Where(x => x.TargetType.Name == typeName);
            if (matchingTypes.Count() == 0)
                return null;
            if (matchingTypes.Count() > 1)
                throw new Exception("More than one plugin matching this type.");

            return matchingTypes.First();
        }

        //public static void WatchDirectory(string directoryLocation)
        //{
        //    FileSystemWatcher watcher = new FileSystemWatcher(directoryLocation, "*.zip");
        //    watcher.Created += watcher_Created;
        //    watcher.BeginInit();
        //}

        //static void watcher_Created(object sender, FileSystemEventArgs e)
        //{
        //}
    }
}
