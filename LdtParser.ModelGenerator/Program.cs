using System.Reflection;
using System.Xml.Linq;

namespace LdtParser.ModelGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("XML directory missing; call " + Path.GetFileName(Assembly.GetExecutingAssembly().Location) + " <xml-directory>! Exit.");
                return;
            }

            var xmlPath = args[0];
            if (!Directory.Exists(xmlPath))
            {
                Console.WriteLine($"Directory '{xmlPath}' does not exist! Exit.");
                return;
            }

            var destinationPath = Path.Combine("..", "..", "..", "..", "LdtParser", "Definition", "Generated");
            if (args.Length == 2)
            {
                destinationPath = args[1];
            }

            var version = XDocument.Load(Path.Combine(xmlPath, "version.xml"))
                .Root!.DescendantsAndSelf("version")
                .First().Value;

            destinationPath = Path.Combine(destinationPath, version);
            Directory.CreateDirectory(destinationPath);


        }
    }
}
