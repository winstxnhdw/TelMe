using System.IO;
using System.Reflection;

namespace TelMe;

public class Resources {
    Assembly Assembly { get; }
    string ProjectName { get; }

    public Resources() {
        this.Assembly = Assembly.GetExecutingAssembly();
        this.ProjectName = this.Assembly.GetName().Name;
    }

    public Stream GetStream(string resourceName) => this.Assembly.GetManifestResourceStream($"{this.ProjectName}.{resourceName}");
}
