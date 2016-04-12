using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace AppBus
{    
    /// <summary>
    /// Classe utilizada para recuperar informações de uma página de linha transurb
    /// </summary>
    public class PageLinha
    {
        private StorageFile arquivoLocal;
        private DadosViewModel vm;
        private AsyncCallback callback;
        private WebRequest webRequest;
        private string link;
        private List<Dados> titulosIda = new List<Dados>();
        private List<Dados> titulosVolta = new List<Dados>();

        public List<Dados> TitulosIda
        {
            get
            {
                return titulosIda;
            }

            set
            {
                titulosIda = value;
            }
        }

        public List<Dados> TitulosVolta
        {
            get
            {
                return titulosVolta;
            }

            set
            {
                titulosVolta = value;
            }
        }

        public PageLinha(string link)
        {
            this.link = link;
        }

        /// <summary>
        /// Faz conexão com o site de maneira assíncrona
        /// </summary>        
        public async Task<List<Dados>[]> RetornarSiteCallback()
        {
            bool _loaded = true;
            //faz conexão com o site
            Uri link = new Uri(this.link);
            StorageFile arquivoLocal =  await MainPage.Current.LocalFolder.CreateFileAsync("paginaisaura.html", CreationCollisionOption.OpenIfExists);
            HtmlDocument doc = new HtmlDocument();
            using (Stream filetext = await arquivoLocal.OpenStreamForReadAsync())
            {
                if(filetext.Length == 0)
                {
                    _loaded = false;
                }
            }
            if (_loaded)
            {
                using (Stream p = await arquivoLocal.OpenStreamForReadAsync())
                {
                    doc.Load(p);
                    //pega o elemento com class igual a div_ida e outro igual a div_volta
                    List<HtmlNode> divs = doc.DocumentNode.Descendants().Where(n => n.Name == "div").ToList();
                    List<HtmlNode> divsWithClass = divs.Where(n => n.Attributes.Where(t => t.Name == "class").Any() == true).ToList();
                    HtmlNode horariosIdaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_ida").ToList()[0];
                    HtmlNode horariosVoltaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_volta").ToList()[0];
                    //Separa os cabeçalhos e horarios em um vetor de string
                    string horariosIdaConc = horariosIdaNode.InnerText;
                    string[] horariosIda = horariosIdaConc.Split(' ', '\r', '\n');
                    string horariosVoltaConc = horariosVoltaNode.InnerText;
                    string[] horariosVolta = horariosVoltaConc.Split(' ', '\r', '\n');
                    horariosIda = horariosIda.Where(horario => horario != "").ToArray();
                    horariosVolta = horariosVolta.Where(horario => horario != "").ToArray();
                    return new List<Dados>[] { HorasTitulo(horariosIda), HorasTitulo(horariosVolta) };
                }
            }
            else
            {
                BackgroundDownloader x = new BackgroundDownloader();
                DownloadOperation y = await x.CreateDownload(link, arquivoLocal).StartAsync();
                using (Stream p = await y.ResultFile.OpenStreamForReadAsync())
                {
                    doc.Load(p);
                    //pega o elemento com class igual a div_ida e outro igual a div_volta
                    List<HtmlNode> divs = doc.DocumentNode.Descendants().Where(n => n.Name == "div").ToList();
                    List<HtmlNode> divsWithClass = divs.Where(n => n.Attributes.Where(t => t.Name == "class").Any() == true).ToList();
                    HtmlNode horariosIdaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_ida").ToList()[0];
                    HtmlNode horariosVoltaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_volta").ToList()[0];
                    //Separa os cabeçalhos e horarios em um vetor de string
                    string horariosIdaConc = horariosIdaNode.InnerText;
                    string[] horariosIda = horariosIdaConc.Split(' ', '\r', '\n');
                    string horariosVoltaConc = horariosVoltaNode.InnerText;
                    string[] horariosVolta = horariosVoltaConc.Split(' ', '\r', '\n');
                    horariosIda = horariosIda.Where(horario => horario != "").ToArray();
                    horariosVolta = horariosVolta.Where(horario => horario != "").ToArray();
                    return new List<Dados>[] { HorasTitulo(horariosIda), HorasTitulo(horariosVolta) };
                }
            }
        }

        /// <summary>
        /// Retorna uma lista com o cabeçalho e os horários referentes a ele.
        /// </summary>
        /// <param name="horarios">Vetor que contém os dados a serem retornados na lista</param>        
        /// <returns>Lista contendo titulo do cabeçalho e horários</returns>
        private List<Dados> HorasTitulo(string[] horarios)
        {
            List<Dados> titulosGen = new List<Dados>();
            while (horarios.Any())
            {
                string[] titulo = horarios.TakeWhile(x => !(48 <= x[0] && x[0] <= 57)).ToArray();
                if (titulo.Any())
                {
                    horarios = horarios.Except(titulo).ToArray();
                }
                string[] horas = horarios.TakeWhile(x => (48 <= x[0] && x[0] <= 57)).ToArray();
                if (horas.Any())
                {
                    horarios = horarios.Except(horas).ToArray();
                }
                if(titulo.Any() && horas.Any())
                {
                    titulosGen.Add(new Dados(titulo,horas));
                }
            }
            return titulosGen;
        }

    }
}
