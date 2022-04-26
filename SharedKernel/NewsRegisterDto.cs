namespace SharedKernel
{


    public class NewsRegisterDto : INewsCommand
    {
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public string NewsAuthor { get; set; }
    }
}

