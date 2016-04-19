using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AppBus
{
    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "App Bus";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title="Isaura P. Garmes | Unesp CTI.", ClassType=typeof(Isaura_Unesp)},
            new Scenario() { Title="Campus - CTI | Falcão ITE", ClassType=typeof(Campus_Falcao)},
            new Scenario() { Title="Unesp CTI | Jardim Ouro Verde", ClassType=typeof(UnespOuroVerde)},
            new Scenario() { Title="Vila Dutra | Campus IPMET", ClassType=typeof(Dutra_Campus)},
            new Scenario() { Title="Pousada da Esperança | Campus / Via Geisel", ClassType=typeof(Pousada_Campus)}
        };
    }
    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
