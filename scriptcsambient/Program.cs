using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptBridge;

namespace scriptcsambient
{
    public class InputThing
    {
        public string ThingString { get; set; }
    }

    public class OutputThing
    {
        public string OutThingString { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var it = new InputThing();
            var ot = new OutputThing();
            it.ThingString = "Random Input";
            var globs = new Dictionary<string, object>();
            globs.Add("InputAmbient", it);
            globs.Add("OutputAmbient", ot);
            var sb = new ScriptBridge.ScriptBridge(globs);
            var code = @"OutputAmbient.OutThingString = InputAmbient.ThingString + "" (Out)"";
Console.WriteLine(1234);
""some return object""";
            var rv = sb.Execute(code);
            Console.WriteLine("---- OutThing ----");
            Console.WriteLine(ot.OutThingString);
            Console.WriteLine("---- Return Value ----");
            Console.WriteLine(rv);
            Console.WriteLine("---- Console ----");
            Console.WriteLine(sb.CapturedOutput);
        }
    }
}
