namespace ViewModel
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            FooterData = new FooterViewModel();
        }

        public string UserName { get; set; }
        public FooterViewModel FooterData { get; set; }//New Property
    }
}