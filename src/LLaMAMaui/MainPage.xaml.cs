using LLaMAMaui.ViewModels;

namespace LLaMAMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.InitializeComponent();
        this.BindingContext = this.MainViewModel = App.Current!.Handler.MauiContext!.Services.GetRequiredService<MainViewModel>();
    }

    public MainViewModel MainViewModel { get; set; }
}