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
    public class Mail: IEntityModel<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string From { get; set; }

        [Required]
        [StringLength(255)]
        public string To { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }

        
        public string Body { get; set; }

        
        public DateTime SentAt { get; set; }
        //public DateTime? ReceivedAt { get; set; }
        //public DateTime? ReadAt { get; set; }
        //public DateTime? RepliedAt { get; set; }
        //public DateTime? ForwardedAt { get; set; }


        [StringLength(50)]
        public string Status { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public Mail()
        {
            Attachments = new HashSet<Attachment>();
        }


    }
public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Filename { get; set; }

        [Required]
        [StringLength(255)]
        public string Path { get; set; }

        [Required]
        [StringLength(255)]
        public string MimeType { get; set; }

        [ForeignKey("IdMail")]
        public Mail Mail { get; set; }
        public int IdMail { get; set; }
    }

}
