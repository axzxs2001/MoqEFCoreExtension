using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using ExamManageSample.Models;
using Microsoft.EntityFrameworkCore;
using MoqEFCoreExtension;
using System.Linq;

namespace ExamManageSample.XUnitTest
{
    /// <summary>
    /// AnswerRepository测试类
    /// </summary>
    [Trait("TestManage", "AnswerRepository测试类")]
    public class AnswerRepositoryTest
    {
        /// <summary>
        /// DB Mock对象
        /// </summary>
        Mock<IDBModel> _dbMock;
        /// <summary>
        /// 被测试对象
        /// </summary>
        IAnswerRepository _answerRepository;
        /// <summary>
        /// 模似answerList
        /// </summary>
        List<Answers> answerList;
        public AnswerRepositoryTest()
        {
            _dbMock = new Mock<IDBModel>();
            _answerRepository = new AnswerRepository(_dbMock.Object);
            //模拟集合
            answerList = new List<Answers> {
                new Answers { Id = 1, Answer = "答案1",IsAnswer=true,QuestionId=1 },
                new Answers { Id = 2, Answer = "答案2",IsAnswer=false,QuestionId=1 }
            };
        }
        /// <summary>
        /// 按问题查询答案
        /// </summary>
        [Fact]
        public void GetAnswer_Default_ReturnCount()
        {
            //Mock  DbSet<Answers>
            var answerSet = new Mock<DbSet<Answers>>().SetupList(answerList);
            //装载测试数据
            _dbMock.Setup(db => db.Answers).Returns(answerSet.Object);
            //设用被测试方法
            var answers = _answerRepository.GetAnswers(1);
            //断言
            Assert.Equal(2, answers.Count);
        }

        /// <summary>
        /// AddAnswer异常测试
        /// </summary>
        [Fact]
        public void AddAnswer_AddNull_ThrowException()
        {
            var message = "AddAnswer异常";
            _dbMock.Setup(db => db.Answers.Add(null)).Throws(new Exception(message));
            var ext = Assert.Throws<Exception>(() => _answerRepository.AddAnswer(null));
            Assert.Contains(message, ext.Message);
        }
        /// <summary>
        /// AddAnswer正常添加
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AddAnswer_Default_ReturnTrue(int result)
        {
            _dbMock.Setup(db => db.Answers.Add(new Answers()));
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _answerRepository.AddAnswer(new Answers());
            Assert.Equal(result == 1, backResult);
        }
        /// <summary>
        /// ModifyAnswer异常测试
        /// </summary>
        [Fact]
        public void ModifyAnswer_NotFind_ThrowException()
        {
            var answerSet = new Mock<DbSet<Answers>>().SetupList(answerList);
            _dbMock.Setup(db => db.Answers).Returns(answerSet.Object);
            var ext = Assert.Throws<Exception>(() => _answerRepository.ModifyAnswer(new Answers { Id = 111 }));
            Assert.Contains("查询不到ID为111的答案", ext.Message);
        }
        /// <summary>
        /// ModifyAnswer正常测试
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ModifyAnswer_Default_ReturnTrue(int result)
        {
            //Mock DbContext DbSet<Answers>
            var answerSet = new Mock<DbSet<Answers>>().SetupList(answerList);
            //用list装载DbSet<Answers>
            _dbMock.Setup(db => db.Answers).Returns(answerSet.Object);
            //模拟保存方法返回值
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            //修改方法的入参
            var answer = new Answers { Id = 1 };
            //调用修改方法，修改方法用中到了 db.Answers.SingleOrDefault(s=>s.Id==answer.Id)，只要模拟了DbSet<Answers>，SingleOrDefault就可调用了
            var backResult = _answerRepository.ModifyAnswer(answer);
            Assert.Equal(result == 1, backResult);
        }

        /// <summary>
        /// RemoveAnswer异常测试
        /// </summary>
        [Fact]
        public void RemoveSubject_NotFind_ThrowException()
        {
            _dbMock.Setup(db => db.Answers.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _answerRepository.RemoveAnswer(111));
            Assert.Contains("查询不到ID为111的答案", ext.Message);
        }

        /// <summary>
        /// RemoveAnswer正常
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void RemoveAnswer_Default_ReturnClass(int result)
        {
            var answer = new Answers { Id = 111 };
            _dbMock.Setup(db => db.Answers.Find(answer.Id)).Returns(value: new Answers());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _answerRepository.RemoveAnswer(111);
            Assert.Equal(result == 1, backResult);
        }

    }



}
