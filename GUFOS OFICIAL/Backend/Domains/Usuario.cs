﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("Usuario_id")]
        public int UsuarioId { get; set; }
        [StringLength(255)]
        public string Nome { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Senha { get; set; }
        [Column("Tipo_Usuario_id")]
        public int? TipoUsuarioId { get; set; }

        [ForeignKey(nameof(TipoUsuarioId))]
        [InverseProperty("Usuario")]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
