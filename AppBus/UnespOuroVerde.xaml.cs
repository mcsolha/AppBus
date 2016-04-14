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
    public sealed partial class UnespOuroVerde : Page
    {
        public static UnespOuroVerde Current;
        private static string htmlUnespOuro = "http://transurbbauru.com.br/do/Linha/93/unesp__cti_jd._ouro_verde";
        PageLinha paginaUnespOuro;
        List<Dados>[] IdaeVolta;
        DadosViewModel ViewModel { get; set; }
        public UnespOuroVerde()
        {
            this.InitializeComponent();
            Current = this;
            this.ViewModel = new DadosViewModel();
            paginaUnespOuro = new PageLinha(htmlUnespOuro);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DefineIda();
        }

        private async void DefineIda()
        {
            IdaeVolta = await paginaUnespOuro.RetornarSiteCallback("unespouro");
            TitulosBox.ItemsSource = IdaeVolta[0];
            TitulosBoxVolta.ItemsSource = IdaeVolta[1];
        }
    }
}
