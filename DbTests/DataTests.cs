using CsvSerializer;
using EnglishTrainer.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTests
{
	[TestClass]
	public class DataTests
	{
		private readonly EFVocabularyUnitOfWork unitOfWork;

		public DataTests()
		{
			unitOfWork =
				new EFVocabularyUnitOfWork(
					"Data Source=(local);Initial Catalog=Vocabulary3; Integrated Security= SSPI");
		}

		[TestMethod]
		public void CheckWordCount()
		{
			var querry = (from word in unitOfWork.Words.GetAll()
						  group word by word.TopicId into wordsByTipic
						  select new { topicId = wordsByTipic.Key, qunatity = wordsByTipic.Count() }).ToList();

			var serializer = new CsvSerializer.Serializer();
			var file = new FileStream("actual1.csv", FileMode.Truncate);
			serializer.Serialize(file, querry);
			file.Close();

			CompareFiles("initial1.csv", "actual1.csv", "Result1.csv");
		}

		[TestMethod]
		public void checkTopicNames()
		{
			var serializer = new Serializer();
			var querry2 = (from topic in unitOfWork.Topics.GetAll()
						   orderby topic.Name
						   select new { id = topic.Id, name = topic.Name }).ToList();

			var file = new FileStream("actual2.csv", FileMode.Truncate);
			serializer.Serialize(file, querry2);
			file.Close();

			CompareFiles("initial2.csv", "actual2.csv", "Result2.csv");
		}

		private void CompareFiles(string name1, string name2, string resultName)
		{
			List<string> original = new List<string>();
			var file = new StreamReader(name1);
			string line;
			while ((line = file.ReadLine()) != null)
			{
				original.Add(line);
			}

			file.Close();

			file = new StreamReader(name2);
			List<string> afterMigration = new List<string>();
			while ((line = file.ReadLine()) != null)
			{
				afterMigration.Add(line);
			}
			file.Close();

			foreach (var item in afterMigration)
			{
				original.Remove(item);
			}

			var serializer = new CsvSerializer.Serializer();
			Assert.AreEqual(original.Count(), 0);

			if (original.Count() != 0)
			{
				var outFile = new FileStream(resultName, FileMode.Create);
				serializer.Serialize(outFile, original);
				outFile.Close();
			}
			else
			{
				var outFile = new StreamWriter(resultName);
				outFile.Write("Everything is fine");
				outFile.Close();
			}
		}
	}
}
