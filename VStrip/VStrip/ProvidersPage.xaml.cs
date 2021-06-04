using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VStrip
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProvidersPage : ContentPage
    {
        public ProvidersPage() {
            InitializeComponent();

            var providers = MainPage.all_providers;
            
            Content = new StackLayout
            {
                Margin = new Thickness(20)
            };

            foreach (var provider in providers) {
                ImageButton btn = new ImageButton {Source = provider.Cover};
                
            }
            
        }
    }
}