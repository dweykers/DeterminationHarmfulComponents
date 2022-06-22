namespace Notes.Views
{
    using System;

    using Xamarin.Essentials;
    using Xamarin.Forms;
    
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            // ССылка на гитхаб проекта.
            await Launcher.OpenAsync("https://aka.ms/xamarin-quickstart");
        }
    }
}