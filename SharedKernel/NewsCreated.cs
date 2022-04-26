namespace SharedKernel
{
    public class NewsCreated : INewsEvent
    {
        public Guid NewsId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public string NewsAuthor { get; set; }
    }
}

