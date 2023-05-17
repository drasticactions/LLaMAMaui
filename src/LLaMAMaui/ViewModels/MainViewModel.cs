// <copyright file="MainViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Drastic.ViewModels;
using LlamaCppLib;
using LlamaToken = System.Int32;

namespace LLaMAMaui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string modelPath = string.Empty;
        private string prompt = string.Empty;
        private string generatedText = string.Empty;
        private LlamaCppSession? session;
        private CancellationTokenSource tokenSource;

        private static LlamaCppOptions options = new()
        {
            Seed = 0,
            PredictCount = -1,
            ContextSize = 2048,
            LastTokenCountPenalty = 64,
            UseHalf = true,
            NewLinePenalty = false,
            UseMemoryMapping = true,
            UseMemoryLocking = false,
            //GpuLayers = 0,

            ThreadCount = Environment.ProcessorCount / 2, // Assuming hyperthreading
            TopK = 40,
            TopP = 0.95f,
            Temperature = 0.8f,
            RepeatPenalty = 1.1f,

            // Mirostat sampling options
            TfsZ = 1.0f,
            TypicalP = 0.95f,
            FrequencyPenalty = 0.0f,
            PresencePenalty = 0.0f,
            Mirostat = Mirostat.Mirostat2,
            MirostatTAU = 5.0f,     // Target entropy
            MirostatETA = 0.10f,    // Learning rate
            PenalizeNewLine = true,
        };

        public MainViewModel(IServiceProvider services)
            : base(services)
        {
            this.tokenSource = new CancellationTokenSource();
            this.StopCommand = new AsyncCommand(this.StopAsync, null, this.Dispatcher, this.ErrorHandler);
            this.PickModelCommand = new AsyncCommand(this.PickModelAsync, null, this.Dispatcher, this.ErrorHandler);
            this.GenerateCommand = new AsyncCommand(this.GenerateAsync, () => !string.IsNullOrEmpty(this.ModelPath) && File.Exists(this.ModelPath) && !string.IsNullOrEmpty(this.Prompt), this.Dispatcher, this.ErrorHandler);
        }

        public AsyncCommand PickModelCommand { get; }

        public AsyncCommand GenerateCommand { get; }

        public AsyncCommand StopCommand { get; }

        /// <summary>
        /// Gets or sets the Model Path.
        /// </summary>
        public string ModelPath {
            get { return this.modelPath; }
            set {
                this.SetProperty(ref this.modelPath, value);
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Prompt.
        /// </summary>
        public string Prompt {
            get { return this.prompt; }
            set { this.SetProperty(ref this.prompt, value); this.RaiseCanExecuteChanged(); }
        }

        /// <summary>
        /// Gets or sets the Generated Text.
        /// </summary>
        public string GeneratedText {
            get { return this.generatedText; }
            set { this.SetProperty(ref this.generatedText, value); }
        }

        public override void RaiseCanExecuteChanged()
        {
            base.RaiseCanExecuteChanged();
            this.GenerateCommand.RaiseCanExecuteChanged();
        }

        private async Task PickModelAsync()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    if (result.FileName.EndsWith("bin", StringComparison.OrdinalIgnoreCase))
                    {
                       this.ModelPath = result.FullPath;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorHandler.HandleError(ex);
            }
        }

        private async Task GenerateAsync()
        {
            this.GeneratedText = string.Empty;

            if (this.session is null)
            {
                var llamacpp = new LlamaCpp(Path.GetFileNameWithoutExtension(this.modelPath), options);
                llamacpp.Load(this.ModelPath);
                this.session = llamacpp.CreateSession("Test Session");
            }

            var result = this.session.Predict(this.Prompt, cancellationToken: this.tokenSource.Token);
            Task.Run(async () => {
                await foreach (var item in result)
                {
                    if (item is null) continue;
                    this.GeneratedText += item;
                }
            }).FireAndForgetSafeAsync();
        }

        private async Task StopAsync()
        {
            this.tokenSource.Cancel();
        }
    }
}
