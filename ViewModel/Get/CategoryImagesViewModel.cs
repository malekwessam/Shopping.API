namespace Application.API.ViewModel.Get
{
    public class CategoryImagesViewModel
    {
        public int Id { get; set; }
        public string Base64Image { get; set; }
        public string Mime { get; set; }
        public short CategoryId { get; set; }
    }
}
