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
    public sealed partial class Pousada_Campus : Page
    {
        public static Pousada_Campus Current;
        private static string htmlPousadaCampus = "http://transurbbauru.com.br/do/Linha/88/pousada_da_esperanca_campus__via_geisel";
        PageLinha paginaPousadaCampus;
        List<Dados>[] IdaeVolta;
        DadosViewModel ViewModel { get; set; }
        public Pousada_Campus()
        {
            this.InitializeComponent();
            Current = this;
            if (MainPage.Current.IsPhone)
            {
                IdaTitle.Text = "Pousada";
                VoltaTitle.Text = "Campus";
            }
            this.ViewModel = new DadosViewModel();
            paginaPousadaCampus = new PageLinha(htmlPousadaCampus);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DefineIda();
        }

        private async void DefineIda()
        {
            IdaeVolta = await paginaPousadaCampus.RetornarSiteCallback("pousadacampus");
            TitulosBox.ItemsSource = IdaeVolta[0];
            TitulosBoxVolta.ItemsSource = IdaeVolta[1];
        }

    }
}
