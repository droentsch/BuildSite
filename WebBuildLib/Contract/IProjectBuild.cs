using Microsoft.Build.Evaluation;
using System.Collections.Generic;

namespace WebBuildLib.Contract
{
    public interface IProjectBuild
    {
        Project Build { get; set; }
        string Filepath { get; set; }
        string Link { get; set; }
        string ShortName { get; set; }
        bool RunDataGenerator { get; set; }
        bool Minify { get; set; }
        bool StartFileTransferService { get; set; }
        IBuildConfig Configuration { get; set; }
        void Run();
        void Unload();
        void AddRecipients(string recipients_to_add);
    }
}
