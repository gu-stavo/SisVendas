﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVenda.Model
{
    internal class modeloCategoria
    {
        private int idCategoria;
        private string nomeCategoria;

        public int IdCategoria
        {
            get => idCategoria;
            set => idCategoria = value;
        }

        public string NomeCategoria
        {
            get => nomeCategoria; set => nomeCategoria = value;
        }
    }
}