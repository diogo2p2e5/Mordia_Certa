using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MordidaCerta.WebAPI.Models;

[Table("Comida")]
public partial class Comida
{
    [Key]
    [StringLength(40)]
    [Unicode(false)]
    public string IdComida { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? IdCategoria { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Comida")]
    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
