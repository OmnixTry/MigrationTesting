using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTrainer.DAL.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string EnglshTranslation { get; set; }
        public string UkrainianTranslation { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public ICollection<Mistake> Mistakes { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
