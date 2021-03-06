﻿using System;
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
using System.Text.RegularExpressions;

namespace AppBus
{    
    /// <summary>
    /// Classe utilizada para recuperar informações de uma página de linha transurb
    /// </summary>
    public class PageLinha
    {
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
        public async Task<List<Dados>[]> RetornarSiteCallback(string titulo)
        {
            Regex htmlRegex = new Regex("<[^>]*>");
            //faz conexão com o site
            Uri link = new Uri(this.link);
            StorageFile arquivoLocal =  await MainPage.Current.LocalFolder.CreateFileAsync(titulo+".html", CreationCollisionOption.OpenIfExists);
            HtmlDocument doc = new HtmlDocument();            
            if (!App.IsInternet())
            {
                using (Stream p = await arquivoLocal.OpenStreamForReadAsync())
                {
                    doc.Load(p, Encoding.UTF7);
                    //pega o elemento com class igual a div_ida e outro igual a div_volta
                    List<HtmlNode> divs = doc.DocumentNode.Descendants().Where(n => n.Name == "div").ToList();
                    List<HtmlNode> divsWithClass = divs.Where(n => n.Attributes.Where(t => t.Name == "class").Any() == true).ToList();
                    HtmlNode horariosIdaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_ida").ToList()[0];
                    HtmlNode horariosVoltaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_volta").ToList()[0];
                    //Separa os cabeçalhos e horarios em um vetor de string
                    string horariosIdaConc = horariosIdaNode.InnerHtml;
                    string[] horariosIda = htmlRegex.Split(horariosIdaConc);
                    horariosIdaConc = "";
                    foreach (string horIda in horariosIda)
                    {
                        horariosIdaConc += horIda + " ";
                    }
                    horariosIda = horariosIdaConc.Split('\r', '\n', ' ').Where(x => x != "").ToArray();
                    string horariosVoltaConc = horariosVoltaNode.InnerHtml;
                    string[] horariosVolta = htmlRegex.Split(horariosVoltaConc);
                    horariosVoltaConc = "";
                    foreach (string horVolta in horariosVolta)
                    {
                        horariosVoltaConc += horVolta + " ";
                    }
                    horariosVolta = horariosVoltaConc.Split('\r', '\n', ' ').Where(x => x != "").ToArray();
                    return new List<Dados>[] { HorasTitulo(horariosIda), HorasTitulo(horariosVolta) };
                }
            }
            else
            {
                BackgroundDownloader x = new BackgroundDownloader();
                DownloadOperation y = await x.CreateDownload(link, arquivoLocal).StartAsync();
                using (Stream p = await y.ResultFile.OpenStreamForReadAsync())
                {
                    doc.Load(p, Encoding.UTF7);
                    //pega o elemento com class igual a div_ida e outro igual a div_volta
                    List<HtmlNode> divs = doc.DocumentNode.Descendants().Where(n => n.Name == "div").ToList();
                    List<HtmlNode> divsWithClass = divs.Where(n => n.Attributes.Where(t => t.Name == "class").Any() == true).ToList();
                    HtmlNode horariosIdaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_ida").ToList()[0];
                    HtmlNode horariosVoltaNode = divsWithClass.Where(n => n.Attributes["class"].Value == "div_volta").ToList()[0];
                    //Separa os cabeçalhos e horarios em um vetor de string
                    string horariosIdaConc = horariosIdaNode.InnerHtml;
                    string[] horariosIda = htmlRegex.Split(horariosIdaConc);
                    horariosIdaConc = "";
                    foreach (string horIda in horariosIda)
                    {
                        horariosIdaConc += horIda + " ";
                    }
                    horariosIda = horariosIdaConc.Split('\r', '\n', ' ').Where(stri => stri != "").ToArray();
                    string horariosVoltaConc = horariosVoltaNode.InnerHtml;
                    string[] horariosVolta = htmlRegex.Split(horariosVoltaConc);
                    horariosVoltaConc = "";
                    foreach (string horVolta in horariosVolta)
                    {
                        horariosVoltaConc += horVolta + " ";
                    }
                    horariosVolta = horariosVoltaConc.Split('\r', '\n', ' ').Where(stri => stri != "").ToArray();
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
                Regex horasRegex = new Regex(@"(\d{2}:\d{2})");
                string[] titulo = horarios.TakeWhile(x => !(horasRegex.IsMatch(x))).ToArray();                
                if (titulo.Any())
                {                    
                    horarios = horarios.Skip(titulo.Length).ToArray();
                }
                string[] horas = horarios.TakeWhile(x => (horasRegex.IsMatch(x))).ToArray();
                if (horas.Any())
                {
                    horarios = horarios.Skip(horas.Length).ToArray();
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
