using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        // aqui armazenamos o hash da senha
        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        public List<Tarefa> Tarefas { get; set; } = new();
    }
}
