using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;

namespace API.NodeServices
{
    public class ScriptRunner

    {
        /// <summary>
        /// returns the output of the script string, or null if the script had an error.
        /// </summary>
        /// <param name="_script">The script as a string to be run and the output returned.</param>
        public static async Task<string> Run(string _script)
        {

            Script<object> script = CSharpScript.Create(_script, ScriptOptions.Default.WithImports("System"));

            ImmutableArray<Diagnostic> diagnostics = script.Compile();

            if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))

                return null;
                // Console.WriteLine(diagnostics.First().GetMessage());

            else
            {
                var state = await script.RunAsync();

                if (state.Exception != null)
                    return null;
                    // Console.WriteLine(state.Exception);
                else
                    return (string)state.ReturnValue;
            }
        }
    }
}
