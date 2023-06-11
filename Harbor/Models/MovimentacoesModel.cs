using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harbor.Models
{
    public class MovimentacoesModel
    {

        [Required]
        [Key()]
        [Column("id_Movimentacao")]
        [Display(Name = "Código da movimentação")]
        public int IdMovimentacao { get; set; }


        [ForeignKey("Container")]
        [Column("id_Container")]
        [Display (Name = "Código do Container")]
        public int IdContainer { get; set; }
        public ContainerModel Container { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório!")]
        [Column("nome_Cliente")]
        [Display(Name = "Nome do cliente")]
        public string NomeCliente { get; set; }


        [Required(ErrorMessage = "O número do container é obrigatório!")]
        [Column("numero_Container")]
        [Display(Name = "Número do container")]
        public string NumeroContainer { get; set; }

        [Required(ErrorMessage = "O tipo de movimentação é obrigatório!")]
        [Column("tipo_Movimentacao")]
        [Display(Name = "Tipo de movimentação")]
        public string TipoMovimentacao { get; set; }

        [Required(ErrorMessage = "A data de início é escolha obrigatória!")]
        [Column("data_Inicio")]
        [Display(Name = "Data de início")]
        public DateTime DataInicio { get; set; }


        [Required(ErrorMessage = "A data de término é escolha obrigatória!")]
        [Column("data_Termino")]
        [Display(Name = "Data de término")]
        public DateTime DataTermino { get; set; }
    }
}
