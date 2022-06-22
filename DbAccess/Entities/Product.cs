namespace DbAccess.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

/// <summary>
///     Продукт
/// </summary>
[Table("product", Schema = "product_information"), Comment("Продукт")]
public class Product
{
    /// <summary>
    /// 
    /// </summary>
    [Key, Column("id"), Comment("Идентификатор продукта")]
    public virtual Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Column("product_name", TypeName = "varchar(250)"), Comment("Название продукта"), Required]
    public virtual string ProductName { get; set; }

    /// <summary>
    ///     Ссылка на штрих-код продукта
    /// </summary>
    [ForeignKey("barcode_id"), Comment("Ссылка на штрих-код продукта"), Required]
    public virtual BarCode BarCode { get; set; }
    
    public virtual ICollection<ProductComposition> ProductCompositions { get; set; }
}