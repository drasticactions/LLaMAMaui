# LLaMAMaui

LLaMAMaui is an example of using LLaMA in a .NET application, using modified versions of [https://github.com/drasticactions/llama.cpp-dotnet](llama.cpp-dotnet) with versions of [https://github.com/drasticactions/llama.cpp.runtimes](llama.cpp) built for desktop and mobile platforms.

This is a proof-of-concept. The default llama.cpp runtime builds are CPU only and will run slower than GPU based builds. With rebuilt libraries and tweeked settings, you can get faster results.

## How To

LLaMAMaui is a MAUI app. As long as you have set up MAUI in your Windows/macOS environment, it should launch.

Some models can be found below.

- [TheBloke on Hugging Face](https://huggingface.co/TheBloke)

Download a LLaMA model, pick it, enter a prompt, and hit "Generate".