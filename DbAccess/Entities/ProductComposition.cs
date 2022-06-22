namespace DbAccess.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

/// <summary>
///     Описание вредных компонентов продукта
/// </summary>
[Table("product_composition", Schema = "product_information"), Comment("Состав продукта с его вредностью/безопасностью")]
public class ProductComposition
{
    /// <summary>
    ///     Идентификатор компонента пищевого продукта
    /// </summary>
    [Key, Column("id"), Comment("Идентификатор компонента пищевого продукта")]
    public virtual Guid Id { get; set; }
    
    /// <summary>
    ///     Название компонента пищевого продукта
    /// </summary>
    [Column("product_composition_name", TypeName = "varchar(250)"), Comment("Название компонента пищевого продукта"), Required]
    public virtual string ProductCompositionName { get; set; }
    
    /// <summary>
    ///     Описание вредности компонента пищевого продукта
    /// </summary>
    [Column("hazard_description", TypeName = "varchar(1000)"), Comment("Описание вредности компонента пищевого продукта"), Required]
    public virtual string HazardDescription { get; set; }
    
    
    /// <summary>
    ///     Ссылка на продукт
    /// </summary>
    [ForeignKey("product_id"), Comment("Ссылка на продукт"), Required]
    public virtual Product Product { get; set; }
}