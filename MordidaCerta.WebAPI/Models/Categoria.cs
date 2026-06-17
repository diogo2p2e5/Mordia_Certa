using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MordidaCerta.WebAPI.Models;

[Table("Categoria")]
public partial class Categoria
{
    [Key]
    [StringLength(40)]
    [Unicode(false)]
    public string IdCategoria { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Comida> Comida { get; set; } = new List<Comida>();
}
