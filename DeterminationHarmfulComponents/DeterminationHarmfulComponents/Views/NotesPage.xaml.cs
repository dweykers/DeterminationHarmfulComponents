namespace Notes.Views
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using DeterminationHarmfulComponents.Result;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Newtonsoft.Json;
    
    public partial class NotesPage : ContentPage
    {
        private readonly string UploadBarCodeUrl = "http://10.15.7.167:5185/api/UploadBarCode/GetProductInformation";
        private readonly string UploadImageUrl = "http://10.15.7.167:5185/api/UploadImage/GetProductInformation";
        private readonly string barCodeValue = "barCode";
        private readonly string fileValue = "file";
        
        private Button takePhotoBtn;
        private Button getPhotoBtn;
        private Label label;

        public NotesPage()
        {
            takePhotoBtn = new Button {Text = "Сделать изображение"};
            getPhotoBtn = new Button {Text = "Выбрать изображение"};
            label = new Label()
            {
                MinimumHeightRequest = 5000,
                HeightRequest = 5000,
                Margin = new Thickness(10)
            };

            // съемка изображения
            takePhotoBtn.Clicked += TakePhotoAsync;
            
            // выбор изображения
            getPhotoBtn.Clicked += GetPhotoAsync;

            Title = "Определение вредных компонентов в составе пищевого продукта";
            
            var stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new StackLayout
                    {
                        Children = {takePhotoBtn, getPhotoBtn},
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    },
                    new StackLayout
                    {
                        Children = { label },
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness(5),
                    }
                },
            };

            var scrollView = new ScrollView { Content = stackLayout };

            Content = scrollView;
        }

        /// <summary>
        ///     Обработчик события выбора изображения
        /// </summary>
        private async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                // Выбираем изображение
                var photo = await MediaPicker.PickPhotoAsync();
                
                using var ms = await photo.OpenReadAsync();

                var data = await GetProductInformationAsync(ms);

                label.Text = data?.Data;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Произошла ошибка при загрузке изображения", ex.Message, "OK");
            }
        }

        /// <summary>
        ///     Обработчик события создания изображения с помощью камеры
        /// </summary>
        private async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.png"
                });
                
                using var ms = await photo.OpenReadAsync();

                var data = await GetProductInformationAsync(ms);

                label.Text = data?.Data;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Произошла ошибка при загрузке изображения", ex.Message, "OK");
            }
        }

        /// <summary>
        ///     Получить информацию о вредных компонентах состава пищевого продукта через запрос на сервер
        /// </summary>
        /// <param name="ms">
        ///     Фотография состава пищевого продукта/штрих-кода продукта
        /// </param>
        /// <returns>
        ///     Исследованный состав пищевого продукта 
        /// </returns>
        /// <exception cref="Exception"></exception>
        private async Task<ResponseResult?> GetProductInformationAsync(Stream ms)
        {
            var result = new ResponseResult();
            
            try
            {
                if (ms == null)
                {
                    await DisplayAlert("Внимание", "Произошла ошибка при загрузке изображения", "OK");
                }
            
                ms.Seek(0, SeekOrigin.Begin);

                var current = Connectivity.NetworkAccess;

                if (current != NetworkAccess.Internet)
                {
                    await DisplayAlert("Внимание", 
                        "В данный момент у вас отсутствует сеть, попробуйте повторить процедуру позже", 
                        "OK");
                }

                using var multipartFormContent = new MultipartFormDataContent();
                
                var fileStreamContent = new StreamContent(ms);
                
                var client = new HttpClient();

                var action = await DisplayActionSheet("Выберите тип получения информации о составе пищевого продукта", 
                                                           "Отмена", 
                                                           string.Empty, 
                                                           "Через фотографию штрих-кода пищевого продукта", 
                                                           "Через фотографию состава пищевого продукта");

                switch (action)
                {
                    case "Через фотографию штрих-кода пищевого продукта":
                    {
                        multipartFormContent.Add(fileStreamContent, barCodeValue, "photo");

                        var response = await client.PostAsync(UploadBarCodeUrl, multipartFormContent);

                        var text = await response.Content.ReadAsStringAsync();

                        result = JsonConvert.DeserializeObject<ResponseResult>(text);
                        break;
                    }
                    case "Через фотографию состава пищевого продукта":
                    {
                        multipartFormContent.Add(fileStreamContent, fileValue, "photo");

                        var response = await client.PostAsync(UploadImageUrl, multipartFormContent);

                        var text = await response.Content.ReadAsStringAsync();

                        result = JsonConvert.DeserializeObject<ResponseResult>(text);
                        break;
                    }
                }

                if (result?.Success == true)
                {
                    return result;
                }
                
                await DisplayAlert("Внимание", "Произошла ошибка при загрузке изображения", "OK");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}