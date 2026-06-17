using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MordidaCerta.WebAPI.Models;

[Table("Usuario")]
[Index("Email", Name = "UQ__Usuario__A9D105349F1F4D69", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [StringLength(40)]
    [Unicode(false)]
    public string IdUsuario { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Email { get; set; } = null!;
}
