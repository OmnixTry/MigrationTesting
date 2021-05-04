using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTrainer.DAL.Entities
{
    public class TopicResult
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int CorrectPercentage { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Language { get; set; }
        public ICollection<Word> MistakenWords { get; set; }        
    }
}
