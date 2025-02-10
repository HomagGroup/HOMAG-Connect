using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// Trace extensions.
    /// </summary>
    public static class TraceExtensions
    {
        /// <summary>
        /// Trace an enumerable.
        /// </summary>
        public static void Trace(this IEnumerable enumerable, [CallerMemberName] string description = "")
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(enumerable, SerializerSettings.Default));

            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Trace an object.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="description"></param>
#pragma warning disable S4225 // Extension methods should not extend "object"
        public static void Trace(this object o, [CallerMemberName] string description = "")
#pragma warning restore S4225 // Extension methods should not extend "object"
        {
            Console.WriteLine(description);

            Console.WriteLine(JsonConvert.SerializeObject(o, SerializerSettings.Default));

            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Trace an object into a file.
        /// </summary>
        public static FileInfo TraceToFile(this object o, string description)
        {
            var fileName = Path.Combine(GetTempDirectory().FullName, description) + ".json";

            var fileInfo = new FileInfo(fileName);

            using var writer = new StreamWriter(fileInfo.FullName);

            writer.WriteLine(JsonConvert.SerializeObject(o, SerializerSettings.Default));

            return fileInfo;
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
    }
}