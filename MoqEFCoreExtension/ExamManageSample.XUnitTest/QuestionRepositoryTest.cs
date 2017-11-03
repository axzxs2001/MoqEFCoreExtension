using Moq;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Xunit;
using MoqEFCoreExtension;
using ExamManageSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamManageSample.XUnitTest
{
    /// <summary>
    /// SubjectRepository测试类
    /// </summary>
    [Trait("TestManage", "QuestionRepository测试")]
    public class QuestionRepositoryTest
    {
        /// <summary>
        /// DB Mock对象
        /// </summary>
        Mock<IDBModel> _dbMock;
        /// <summary>
        /// 被测试对象
        /// </summary>
        IQuestionRepository _questionRepository;
        public QuestionRepositoryTest()
        {
            _dbMock = new Mock<IDBModel>();
            _questionRepository = new QuestionRepository(_dbMock.Object);
        }
        /// <summary>
        /// 查询科目测试
        /// </summary>
        [Fact]
        public void GetQuestionsByTestID_Default_ReturnCount()
        {
            var data = new List<Questions> { new Questions { Id = 1, Question = "题目1",TestId=1 }, new Questions { Id = 2, Question = "题目2" ,TestId=1} };
            var questionSet = new Mock<DbSet<Questions>>().SetupList(data);
    
            _dbMock.Setup(db => db.Questions).Returns(questionSet.Object);
            var list = _questionRepository.GetQuestionsByTestID(1);
            Assert.Equal(2, list.Count);
        }

        /// <summary>
        /// AddQuestion异常测试
        /// </summary>
        [Fact]
        public void AddQuestion_AddNull_ThrowException()
        {
            var message = "AddQuestion异常";
            _dbMock.Setup(db => db.Questions.Add(null)).Throws(new Exception(message));
            var ext = Assert.Throws<Exception>(() => _questionRepository.AddQuestion(null));
            Assert.Contains(message, ext.Message);
        }
        /// <summary>
        /// AddQuestion正常添加
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AddSubject_Default_ReturnTrue(int result)
        {
            _dbMock.Setup(db => db.Questions.Add(new Questions()));
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _questionRepository.AddQuestion(new Questions());
            Assert.Equal(result == 1, backResult);
        }
        /// <summary>
        /// ModifySubject异常测试
        /// </summary>
        [Fact]
        public void ModifyQuestion_NotFind_ThrowException()
        {

            _dbMock.Setup(db => db.Questions.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _questionRepository.ModifyQuestion(new Questions { Id = 111 }));
            Assert.Contains("查询不到ID为111的题目", ext.Message);
        }
        /// <summary>
        /// ModifyQuestion正常测试
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ModifySubject_Default_ReturnTrue(int result)
        {
            var question = new Questions { Id = 111 };
            _dbMock.Setup(db => db.Questions.Find(question.Id)).Returns(value: new Questions());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _questionRepository.ModifyQuestion(question);
            Assert.Equal(result == 1, backResult);
        }

        /// <summary>
        /// RemoveQuestion异常测试
        /// </summary>
        [Fact]
        public void RemoveSubject_NotFind_ThrowException()
        {
            _dbMock.Setup(db => db.Questions.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _questionRepository.RemoveQuestion(111));
            Assert.Contains("查询不到ID为111的题目", ext.Message);
        }

        /// <summary>
        /// RemoveQuestion正常
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void RemoveSubject_Default_ReturnClass(int result)
        {
            var question = new Questions { Id = 111 };
            _dbMock.Setup(db => db.Questions.Find(question.Id)).Returns(value: new Questions());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _questionRepository.RemoveQuestion(111);
            Assert.Equal(result == 1, backResult);
        }

    }



}
