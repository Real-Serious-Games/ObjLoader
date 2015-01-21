using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using ObjLoader.Loader.Loaders;
using System.Linq;

namespace ObjLoader.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create(new MaterialStreamProvider());

            var fileStream = new FileStream(args[0], FileMode.Open, FileAccess.Read);

            var result = objLoader.Load(fileStream);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            
            PrintResult(result);
        }

        private static void PrintResult(LoadResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Load result:");
            sb.Append("Vertices: ");
            sb.AppendLine(result.Vertices.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append("Textures: ");
            sb.AppendLine(result.Textures.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append("Normals: ");
            sb.AppendLine(result.Normals.Count.ToString(CultureInfo.InvariantCulture));
            sb.AppendLine();
            sb.AppendLine("Groups: ");

            foreach (var loaderGroup in result.Groups)
            {
                sb.AppendLine("\t" + loaderGroup.Name);
                sb.Append("\t\tFaces: ");
                sb.AppendLine("\t\t\t" + loaderGroup.Faces.Count.ToString(CultureInfo.InvariantCulture));
                sb.AppendLine("\t\tLines: ");
                foreach (var line in loaderGroup.Lines)
                {
                    var lineStr = string.Join(", ", line.VertexIndicies.Select(i => i.ToString()).ToArray());
                    sb.AppendLine("\t\t\t" + lineStr);
                }
            }

            Console.WriteLine(sb);
        }
    }
}
