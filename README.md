# LLaMAMaui

LLaMAMaui is an example of using LLaMA in a .NET application, using modified versions of [llama.cpp-dotnet](https://github.com/drasticactions/llama.cpp-dotnet) with versions of [llama.cpp](https://github.com/drasticactions/llama.cpp.runtimes) built for desktop and mobile platforms.

This is a proof-of-concept. The default llama.cpp runtime builds are CPU only and will run slower than GPU based builds. With rebuilt libraries and tweeked settings, you can get faster results.

## How To

LLaMAMaui is a MAUI app. As long as you have set up MAUI in your Windows/macOS environment, it should launch.

Some models can be found below.

- [TheBloke on Hugging Face](https://huggingface.co/TheBloke)

Download a LLaMA model, pick it, enter a prompt, and hit "Generate".

## NOTE

In order to run this on macOS/iPhone, you must match the architecture you're running on your system. For example, if you're on an M1, you must use the arm64 runtime identifiers (Ex: `maccatalyst-arm64`). If you don't, your application will crash.