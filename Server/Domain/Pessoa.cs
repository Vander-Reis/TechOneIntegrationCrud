﻿using System.Text.Json.Serialization;

namespace PersonApi.Domain
{
    public class Pessoa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
