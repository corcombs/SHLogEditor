using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using SampleNamespace;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Data.Pdf;

using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void openLogs(object sender, RoutedEventArgs e)
        {
             var picker = new Windows.Storage.Pickers.FileOpenPicker();
             picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
             picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
             picker.FileTypeFilter.Add(".pdf");
             Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
             if (file != null)
             {
                 // Application now has read/write access to the picked file
                 this.txtEmployeeCode.Text = "Picked photo: " + file.Path;
                PdfDocument doc = await PdfDocument.LoadFromFileAsync(file);
                displayPdf(doc);
            }
             else
             {
                 this.txtEmployeeCode.Text = "Operation cancelled.";
             }

        }
        private async void displayPdf(PdfDocument pdfDoc)
        {
            BitmapImage image = new BitmapImage();
            var page = pdfDoc.GetPage(0);

            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);
                await image.SetSourceAsync(stream);
            }
            imgPdf.Source = image;
        }
    }
}
