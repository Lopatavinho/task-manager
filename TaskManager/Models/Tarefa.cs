using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public string Status { get; set; } = "Pendente";

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
