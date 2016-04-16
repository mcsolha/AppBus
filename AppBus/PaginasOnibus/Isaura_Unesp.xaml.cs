using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Isaura_Unesp : Page
    {
        public static Isaura_Unesp Current;
        private static string htmlIsauraUnesp = "http://transurbbauru.com.br/do/Linha/77/isaura_p._garmes_unesp_-_cti";
        PageLinha paginaIsauraUnesp;
        List<Dados>[] IdaeVolta;
        public Isaura_Unesp()
        {
            this.InitializeComponent();
            Current = this;
            this.ViewModel = new DadosViewModel();
            paginaIsauraUnesp = new PageLinha(htmlIsauraUnesp);
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DefineIda();       
        }

        private async void DefineIda()
        {
            IdaeVolta = await paginaIsauraUnesp.RetornarSiteCallback("isauraunesp");            
            TitulosBox.ItemsSource = IdaeVolta[0];
            TitulosBoxVolta.ItemsSource = IdaeVolta[1];
        }

        public void NotifyListBox(Dados dado)
        {
            ViewModel.DefaultDados.Add(dado);
        }

        DadosViewModel ViewModel { get; set; }        
    }
}
