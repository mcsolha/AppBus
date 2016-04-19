using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
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
    public sealed partial class Campus_Falcao : Page
    {
        public static Campus_Falcao Current;
        private static string htmlCampusFalcao = "http://transurbbauru.com.br/do/Linha/70/campus_-_cti_falcao_-_ite";
        PageLinha paginaCampusFalcao;
        List<Dados>[] IdaeVolta;
        DadosViewModel ViewModel { get; set; }
        public Campus_Falcao()
        {
            this.InitializeComponent();
            if (MainPage.Current.IsPhone)
            {
                IdaTitle.Text = "Campus";
                VoltaTitle.Text = "Falcão";
            }
            Current = this;
            this.ViewModel = new DadosViewModel();            
            paginaCampusFalcao = new PageLinha(htmlCampusFalcao);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DefineIda();
        }

        private async void DefineIda()
        {
            IdaeVolta = await paginaCampusFalcao.RetornarSiteCallback("campusfalcao");
            TitulosBox.ItemsSource = IdaeVolta[0];
            TitulosBoxVolta.ItemsSource = IdaeVolta[1];
        }
    }
}
