﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBus
{
    /// <summary>
    /// Classe utilizada para armazenar os horários para cada cabeçalho
    /// </summary>
    public class Dados
    {        
        private string titulo;
        private string[] horas;
        public string Titulo { get { return titulo; } }
        public string[] Horas { get { return horas; } }
        public Dados(string[] titulo, string[] horas)
        {
            for (int i = 0; i < titulo.Length; i++)
            {
                if (i == titulo.Length - 1)
                    this.titulo += titulo[i];
                else
                {
                    if (titulo[i] != "Sábado" && titulo[i] != "Feriado" && titulo[i] != "útil")
                        this.titulo += titulo[i] + ' ';
                    else
                        this.titulo += titulo[i] + ":\n";
                }
            }
            this.horas = horas;
        }
    }
    public class DadosViewModel
    {
        private ObservableCollection<Dados> defaultDados;
        public ObservableCollection<Dados> DefaultDados
        {
            get
            {
                if (defaultDados == null)
                    return new ObservableCollection<Dados>();
                else
                    return defaultDados;
            }
            set
            {
                defaultDados = value;
            }
        }
        public DadosViewModel()
        {
        }
    }
}
