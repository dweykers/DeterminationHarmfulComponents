namespace DbAccess.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

/// <summary>
///     Штрих-код продукта
/// </summary>
[Table("barcode", Schema = "product_information"), Comment("Штрих-код продукта")]
public class BarCode
{
    /// <summary>
    ///     Идентификатор штрих-кода продукта
    /// </summary>
    [Key, Column("id"), Comment("Идентификатор штрих-кода продукта")]
    public virtual Guid Id { get; set; }
    
    /// <summary>
    ///     Номер штрих-кода продукта
    /// </summary>
    [Column("barcode_number", TypeName = "varchar(13)"), Comment("Номер штрих-кода продукта"), Required]
    public virtual string BarCodeNumber { get; set; }
}