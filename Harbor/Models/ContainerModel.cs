using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harbor.Models
{
    public class ContainerModel
    {
        [Required]
        [Key()]
        [Column("id_Container")]
        [Display(Name = "Código do Container")]
        public int IdContainer { get; set; }

        [Required(ErrorMessage = "O nome do cliente é de preenchimento obrigatório!")]
        [Column("nome_Cliente")]
        [Display(Name = "Nome do cliente")]
        public string NomeCliente {get; set; }

        [Required(ErrorMessage = "O número do container é de preenchimento obrigatório!")]
        [StringLength(11)]
        [Column("numero_Container")]
        [Display(Name = "Número do container")]
        public string NumeroContainer { get; set; }

        [Required(ErrorMessage = "O tipo do container é de escolha obrigatória!")]
        [StringLength(2)]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O status do container é de escolha obrigatória!")]
        [StringLength(5)]
        public string Status { get; set; }

        [Required(ErrorMessage = "A categoria do container é de escolha obrigatória!")]
        [StringLength(10)]
        public string Categoria { get; set; }

    }
}
