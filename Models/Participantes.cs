using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Login.Models
{
    [Table("Participantes")]
    public class Participantes
    {
        [Key]
        [Column("Id")]
        [DisplayName("Identificação")]
        public int Id { get; set; }

        [Column("Nome")]
        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [Column("Telefone")]
        [DisplayName("Telefone")]
        public string? Telefone { get; set; }

        [Column("DataAtualizacao")]
        [DisplayName("Data de Atualização")]
        public DateTime Atualizacao { get; set; } = DateTime.Now;

        [Column("DataCriacao")]
        [DisplayName("Data de Criação")]
        public DateTime Criacao { get; set; } = DateTime.Now;

        [Column("Usuario")]
        [DisplayName("Usuário")]
        public string? Usuario { get; set; }
    }
}
 