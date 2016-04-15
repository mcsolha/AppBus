using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppBus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dutra_Campus : Page
    {
        public static Dutra_Campus Current;
        private static string htmlDutraCampus = "http://transurbbauru.com.br/do/Linha/66/vila_dutra_campus_ipmet";
        PageLinha paginaDutraCampus;
        List<Dados>[] IdaeVolta;
        public Dutra_Campus()
        {
            this.InitializeComponent();
            Current = this;
            this.ViewModel = new DadosViewModel();
            paginaDutraCampus = new PageLinha(htmlDutraCampus);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DefineIda();
        }

        private async void DefineIda()
        {
            IdaeVolta = await paginaDutraCampus.RetornarSiteCallback("isauraunesp");
            TitulosBox.ItemsSource = IdaeVolta[0];
            TitulosBoxVolta.ItemsSource = IdaeVolta[1];
        }

        DadosViewModel ViewModel { get; set; }
    }
}
