using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Models.Comun;
using BackEnd.Services.Services.Venta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Services.Models.Mail
{    
    public class MailServer: IEntityModel<int>, IEntityNameModel
    {
        [Key, Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,MaxLength(60)]
        public string Nombre { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Server { get; set; }
        public int Puerto { get; set; }
        //Pop-imap-local
        [MaxLength(10)]
        public string TipoServer { get; set; } = "pop";
        public bool EsSSL { get; set; } = false;
        [MaxLength(100)]
        public string Usuario { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        public int Prioridad { get; set; }

    }
}
