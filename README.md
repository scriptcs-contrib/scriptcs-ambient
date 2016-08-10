# scriptcs-ambient
Example of injecting ambient (global) objects into a ScriptCS script engine.  

This is a complete Visual Studio solution that hosts ScriptCS and injects some ambient
objects (globals) into the script's namespace.

It imports ScriptCS 0.16.1 via NuGet and wraps it in an "Engine" abstraction,
which happens to match the architecture of the larger application the author is
working in.

The only goal here is to illuminate the moving parts involved in ambient injection,
including getting access to the ambient objects after the script has run.  The script
can assign to the global symbols so just keeping the object references may not
meet requirements.

When a script engine is used to extend a product, the design of the "calling convention"
for scripts becomes a product feature.  Controlling the syntax (global names) and
semantics (e.g. the meaning of assignment) for the objects exposed to the script
context is therefore important to the developer adopting the script engine.

TODO:
- demo initially null, assignable ambient symbol
- figure out how to use expando object so the set of ambients is not fixed by the engine layer at compile time
- make it do something entertaining
