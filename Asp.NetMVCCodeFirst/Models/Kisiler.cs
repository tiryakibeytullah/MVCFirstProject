using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Asp.NetMVCCodeFirst.Models
{
    [Table("Kisiler")]
    public class Kisiler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20),Required]
        public string Ad { get; set; }

        [StringLength(20), Required]
        public string Soyad { get; set; }
        public int Yas { get; set; }
        public virtual List<Adresler> Adresler { get; set; }


    }
}