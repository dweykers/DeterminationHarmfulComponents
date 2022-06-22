namespace Services;

using System.Drawing;
using Contracts;
using IronBarCode;

/// <inheritdoc/>
public class ObtainingInformationBarCodeService : IObtainingInformationBarCodeService
{
    /// <inheritdoc/>
    public Task<string?> GetCompositionInformationAsync(MemoryStream stream)
    {
        if (stream == null)
        {
            throw new ArgumentNullException("Ошибка получения номера штрих-кода с изображения");
        }
        
        stream.Seek(0, SeekOrigin.Begin);
        
        using var bitmap = new Bitmap(stream);

        var barcodeRead = BarcodeReader.ReadASingleBarcode(bitmap, 
            BarcodeEncoding.EAN8 | BarcodeEncoding.EAN13, 
            BarcodeReader.BarcodeRotationCorrection.Extreme,
            BarcodeReader.BarcodeImageCorrection.DeepCleanPixels);

        var barCodeResult = barcodeRead?.Text;

        return Task.FromResult(barCodeResult);
    }
}