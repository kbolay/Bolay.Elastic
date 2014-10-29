using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic
{
    public static class EmbeddedResource
    {
        public static string GetContentOfEmbeddedResourceFile(Assembly assembly, string resourcePath)
        {
            string jsonValue;

            using (StreamReader stream = new StreamReader(assembly.GetManifestResourceStream(resourcePath)))
            {
                jsonValue = stream.ReadToEnd();
            }

            return jsonValue;
        }
    }
}
