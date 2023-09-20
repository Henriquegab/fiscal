using System.ComponentModel.DataAnnotations.Schema;

namespace Fiscalio.Models
{
    public class NotaFiscal
    {
        public int IdNota { get; set; }

        public string Emissor { get; set; }

        public DateTime Data { get; set; }

        public ICollection<Item> Itens { get; set; }

        [NotMapped]
        public float ValorTotal { get; set; }
    }
}