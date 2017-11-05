namespace ImageGallery.Client.ViewModels
{
    public class OrderFrameViewModel
    {
        public OrderFrameViewModel(string address)
        {
            Address = address;
        }

        public string Address { get; set; }
    }
}
