using Moq;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Xunit;
using ExamManageSample.Models;
using Microsoft.EntityFrameworkCore;
using MoqEFCoreExtension;
namespace ExamManageSample.XUnitTest
{
    /// <summary>
    /// ClassTestRepositoryTest测试类
    /// </summary>
    [Trait("TestManage", "ClassTestRepositoryTest测试")]
    public class ClassTestRepositoryTest
    {
        /// <summary>
        /// DB Mock对象
        /// </summary>
        Mock<IDBModel> _dbMock;
        /// <summary>
        /// 被测试对象
        /// </summary>
        IClassTestRepository _classTestRepository;
        public ClassTestRepositoryTest()
        {
            _dbMock = new Mock<IDBModel>();
            _classTestRepository = new ClassTestRepository(_dbMock.Object);
        }
        /// <summary>
        /// 查询考试班级测试
        /// </summary>
        [Fact]
        public void GetClassTests_Default_ReturnCount()
        {
            var data = new List<ClassTests> { new ClassTests { Id = 1, ClassId = 1, TestId = 1, IsValidate = true, Class = new Classes(),Test=new Tests() }, new ClassTests { Id = 2, ClassId = 1, TestId = 2,IsValidate=true, Class = new Classes(), Test = new Tests() } };
            var clsTestSet = new Mock<DbSet<ClassTests>>().SetUpList(data);
         
            _dbMock.Setup(db => db.ClassTests).Returns(clsTestSet.Object);
            var list = _classTestRepository.GetClassTests();
            Assert.Equal(2, list.Count);
        }
        /// <summary>
        /// 按班级ID查询试卷测试
        /// </summary>
        [Fact]
        public void GetTestByClassID_Default_ReturnCount()
        {
            var data = new List<ClassTests> { new ClassTests { Id = 1, ClassId = 1, TestId = 1, IsValidate = true, Class = new Classes(), Test = new Tests() }, new ClassTests { Id = 2, ClassId = 1, TestId = 2, IsValidate = false, Class = new Classes(), Test = new Tests() } };
            var clsTestSet = new Mock<DbSet<ClassTests>>().SetUpList(data);

            _dbMock.Setup(db => db.ClassTests).Returns(clsTestSet.Object);
            var test = _classTestRepository.GetTestByClassID(1);
            Assert.NotNull(test);
        }   /// <summary>
            /// 按班级ID查询试卷测试
            /// </summary>
        [Fact]
        public void GetTestByClassID_ErrorClassID_ReturnNull()
        {
            var data = new List<ClassTests> { new ClassTests { Id = 1, ClassId = 1, TestId = 1, IsValidate = true, Class = new Classes(), Test = new Tests() }, new ClassTests { Id = 2, ClassId = 1, TestId = 2, IsValidate = false, Class = new Classes(), Test = new Tests() } };
            var clsTestSet = new Mock<DbSet<ClassTests>>().SetUpList(data);
            _dbMock.Setup(db => db.ClassTests).Returns(clsTestSet.Object);
            var test = _classTestRepository.GetTestByClassID(2);
            Assert.Null(test);
        }
        /// <summary>
        /// AddClassTest异常测试
        /// </summary>
        [Fact]
        public void AddClassTest_AddNull_ThrowException()
        {
            _dbMock.Setup(db => db.ClassTests.Add(null)).Throws(new Exception("AddClassTest异常"));
            var ext = Assert.Throws<Exception>(() => _classTestRepository.AddClassTest(null));
            Assert.Contains("AddClassTest异常", ext.Message);
        }
        /// <summary>
        /// AddClassTest正常添加
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AddClassTest_Default_ReturnTrue(int result)
        {
            _dbMock.Setup(db => db.ClassTests.Add(new ClassTests()));
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _classTestRepository.AddClassTest(new ClassTests());
            Assert.Equal(result == 1, backResult);
        }
        /// <summary>
        /// ModifyClass异常测试
        /// </summary>
        [Fact]
        public void ModifyClassTest_NotFind_ThrowException()
        {

            _dbMock.Setup(db => db.ClassTests.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _classTestRepository.ModifyClassTest(new ClassTests { Id = 111 }));
            Assert.Contains("查询不到ID为111的考试班级", ext.Message);
        }
        /// <summary>
        /// ModifyClassTest正常测试
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ModifyClassTest_Default_ReturnTrue(int result)
        {
            var clsTest = new ClassTests { Id = 111 };
            _dbMock.Setup(db => db.ClassTests.Find(clsTest.Id)).Returns(value: new ClassTests());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _classTestRepository.ModifyClassTest(clsTest);
            Assert.Equal(result == 1, backResult);
        }

        /// <summary>
        /// RemoveClassTest异常测试
        /// </summary>
        [Fact]
        public void RemoveClassTest_NotFind_ThrowException()
        {
            _dbMock.Setup(db => db.ClassTests.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _classTestRepository.RemoveClassTest(111));
            Assert.Contains("查询不到ID为111的考试班级", ext.Message);
        }

        /// <summary>
        /// RemoveClassTest正常
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void RemoveClassTest_Default_ReturnClass(int result)
        {
            var clsTest = new ClassTests { Id = 111 };
            _dbMock.Setup(db => db.ClassTests.Find(clsTest.Id)).Returns(value: new ClassTests());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _classTestRepository.RemoveClassTest(111);
            Assert.Equal(result == 1, backResult);
        }

    }



}
