using BackEnd.Services.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Services.Models.Afip
{
    public class CertificadoDigital : IEntityModel<string>, IEntityNameModel
    {
        [Key, Required, MaxLength(10)]
        public string Id { get; set; }
        [Required, MaxLength(60)]
        public string Nombre { get; set; }
        public string Path { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
