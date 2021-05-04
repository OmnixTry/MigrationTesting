using EnglishTrainer.DAL.Entities;
using EnglishTrainer.DAL.Interfaces;
using EnglishTrainer.DAL.Migrations;
using EnglishTrainer.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CsvSerializer;
using ServiceStack.Text;
using System;
using System.IO;

namespace DbTests
{
	[TestClass]
	public class Preparations
	{
		private readonly EFVocabularyUnitOfWork unitOfWork;
		public Preparations()
		{
			unitOfWork =
				new EFVocabularyUnitOfWork(
					"Data Source=(local);Initial Catalog=Vocabulary3; Integrated Security= SSPI");
		}
		[TestMethod]
		public void PopulateDb()
		{
			var topics = new List<Topic>()
			{
				new Topic(){ Name="Fruits"},
				new Topic(){ Name="Society"}
			};

			topics.First().Words = new List<Word>()
			{
				new Word() { EnglshTranslation = "Apple", UkrainianTranslation = "Яблуко"},
				new Word() { EnglshTranslation = "Fruit", UkrainianTranslation = "Фрукт"},
				new Word() { EnglshTranslation = "Vegetable", UkrainianTranslation = "Овоч" }
			};

			topics[1].Words = new List<Word>()
			{
				new Word() { EnglshTranslation = "Girl", UkrainianTranslation = "Дівчинка" },
				new Word() { EnglshTranslation = "Car", UkrainianTranslation = "Автомобіль" }
			};

			foreach (var item in topics)
			{
				unitOfWork.Topics.Create(item);
			}

			//unitOfWork.Save();
		}

		[TestMethod]
		public void ReadDB()
		{
			var querry = (from word in unitOfWork.Words.GetAll()
						 group word by word.TopicId into wordsByTipic
						 select new { topicId = wordsByTipic.Key, qunatity = wordsByTipic.Count() }).ToList();

			var serializer = new CsvSerializer.Serializer();
			var file = new FileStream("initial1.csv", FileMode.Truncate);
			serializer.Serialize(file, querry);

			var querry2 = (from topic in unitOfWork.Topics.GetAll()
						   orderby topic.Name
						   select new { id = topic.Id, name = topic.Name}).ToList();

			file.Close();
			file = new FileStream("initial2.csv", FileMode.Truncate);
			serializer.Serialize(file, querry2);
			file.Close();
		}

		[TestMethod]
		public void ApplyMigration()
		{
			unitOfWork.Migrate();
		}

		
	}
}
