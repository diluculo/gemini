@echo off

set path=out
set ver=1.0.1 

echo Set version = %ver%
nuget.exe pack src\Gemini\Gemini.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.CodeCompiler\Gemini.Modules.CodeCompiler.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.CodeEditor\Gemini.Modules.CodeEditor.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.ErrorList\Gemini.Modules.ErrorList.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.GraphEditor\Gemini.Modules.GraphEditor.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.Inspector\Gemini.Modules.Inspector.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.Inspector.MonoGame\Gemini.Modules.Inspector.MonoGame.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.Inspector.Xna\Gemini.Modules.Inspector.Xna.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.MonoGame\Gemini.Modules.MonoGame.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.Output\Gemini.Modules.Output.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.PropertyGrid\Gemini.Modules.PropertyGrid.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.SharpDX\Gemini.Modules.SharpDX.csproj -OutputDirectory %path% -Version %ver%
nuget.exe pack src\Gemini.Modules.Xna\Gemini.Modules.Xna.csproj -OutputDirectory %path% -Version %ver%