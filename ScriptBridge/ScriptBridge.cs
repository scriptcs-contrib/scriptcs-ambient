using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace ScriptBridge
{
    public class MY_ScriptHost : ScriptHost
    {
        public MY_ScriptHost(IScriptPackManager scriptPackManager, ScriptEnvironment environment)
            : base(scriptPackManager, environment)
        {
        }

        public dynamic InputAmbient { get; set; }
        public dynamic OutputAmbient { get; set; }
    }

    class MY_Factory : IScriptHostFactory
    {
        Dictionary<string, object> m_globs;
        public MY_ScriptHost m_host;
        public MY_Factory(Dictionary<string, object> globs)
        {
            m_globs = globs;
        }
        public IScriptHost CreateScriptHost(IScriptPackManager scriptPackManager, string[] scriptArgs)
        {
            m_host = new MY_ScriptHost(scriptPackManager, new ScriptEnvironment(scriptArgs));
            m_host.InputAmbient = m_globs["InputAmbient"];
            m_host.OutputAmbient = m_globs["OutputAmbient"];
            return m_host;
        }
    }
    public class ScriptBridge
    {
        Dictionary<string, object> m_globals;
        public ScriptBridge(Dictionary<string,object> Globals)
        {
            m_globals = Globals;
        }

        public dynamic Execute(string Code)
        {
            var console = (IConsole)new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);

            var builder = new ScriptServicesBuilder(console, logProvider);
            builder.ScriptHostFactory<MY_Factory>();
            builder.LogLevel(LogLevel.Info).Cache(false).Repl(false).ScriptEngine<CSharpScriptEngine>();

            var shf = new MY_Factory(m_globals);
            builder.SetOverride<IScriptHostFactory, MY_Factory>(shf);
            var services = builder.Build();

            var executor = (ScriptExecutor)services.Executor;
            executor.Initialize(Enumerable.Empty<string>(), Enumerable.Empty<IScriptPack>());

            var result = executor.ExecuteScript(Code);
            m_globals["OutputAmbient"] = shf.m_host.OutputAmbient;

            if (result.CompileExceptionInfo != null)
                throw result.CompileExceptionInfo.SourceException;
            if (result.ExecuteExceptionInfo != null)
                throw result.ExecuteExceptionInfo.SourceException;

            return result.ReturnValue;
        }
    }
}
