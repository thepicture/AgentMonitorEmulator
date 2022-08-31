using System;
using System.Reflection;

namespace AgentMonitorEmulator.AgentMonitor_server.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}