using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using MoqEFCoreExtension;
using ExamManageSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamManageSample.XUnitTest
{
    /// <summary>
    /// TeacherRepository测试类
    /// </summary>
    [Trait("TestManage", "TeacherRepository测试")]
    public class TeacherRepositoryTest
    {
        /// <summary>
        /// DB Mock对象
        /// </summary>
        Mock<IDBModel> _dbMock;
        /// <summary>
        /// 被测试对象
        /// </summary>
        ITeacherRepository _teacherRepository;
        public TeacherRepositoryTest()
        {
            _dbMock = new Mock<IDBModel>();
            _teacherRepository = new TeacherRepository(_dbMock.Object);
        }
        
        /// <summary>
        /// Login正常返回
        /// </summary>
        [Fact]
  
        public void Login_Default_ReturnTeacher()
        {
            var teacher = new Teachers { Id = 1, TeaacherNo = "0001", Password = "111111" };
            var data = new List<Teachers> { teacher };
            var teacherSet = new Mock<DbSet<Teachers>>().SetupList(data);
 
            _dbMock.Setup(db => db.Teachers).Returns(teacherSet.Object);
            var backTeacher = _teacherRepository.Login("0001","111111");
            Assert.Same(teacher, backTeacher);
        }

        /// <summary>
        /// 错误密码测试
        /// </summary>
        [Fact]

        public void Login_ErrorPassword_ReturnNull()
        {
            var teacher = new Teachers { Id = 1, TeaacherNo = "0001", Password = "111111" };
            var data = new List<Teachers> { teacher };
            var teacherSet = new Mock<DbSet<Teachers>>().SetupList(data);        

            _dbMock.Setup(db => db.Teachers).Returns(teacherSet.Object);
            var backTeacher = _teacherRepository.Login("0001", "222222");
            Assert.Null(backTeacher);
        }


        /// <summary>
        /// 查询教师测试
        /// </summary>
        [Fact]
        public void GetClasses_Default_ReturnCount()
        {
            var data = new List<Teachers> { new Teachers(), new Teachers()};
            var teacherSet = new Mock<DbSet<Teachers>>().SetupList(data);

           
            _dbMock.Setup(db => db.Teachers).Returns(teacherSet.Object);
            var list = _teacherRepository.GetTeachers();
            Assert.Equal(2, list.Count);
        }

        /// <summary>
        /// AddTeacher异常测试
        /// </summary>
        [Fact]
        public void AddTeacher_AddNull_ThrowException()
        {
            var excetionMessage = "AddTeacher异常";
            _dbMock.Setup(db => db.Teachers.Add(null)).Throws(new Exception(excetionMessage));
            var ext = Assert.Throws<Exception>(() => _teacherRepository.AddTeacher(null));
            Assert.Contains(excetionMessage, ext.Message);
        }
        /// <summary>
        /// AddTeacher正常添加
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AddTeacher_Default_ReturnTrue(int result)
        {
            _dbMock.Setup(db => db.Teachers.Add(new Teachers()));
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _teacherRepository.AddTeacher(new Teachers());
            Assert.Equal(result == 1, backResult);
        }
        /// <summary>
        /// ModifyTeacher异常测试
        /// </summary>
        [Fact]
        public void ModifyTeacher_NotFind_ThrowException()
        {
            _dbMock.Setup(db => db.Teachers.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _teacherRepository.ModifyTeacher(new Teachers { Id = 111 }));
            Assert.Contains("查询不到ID为111的教师", ext.Message);
        }
        /// <summary>
        /// ModifyTeacher正常测试
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ModifyTeacher_Default_ReturnTrue(int result)
        {
            var teacher = new Teachers { Id = 111 };
            _dbMock.Setup(db => db.Teachers.Find(teacher.Id)).Returns(value: new Teachers());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _teacherRepository.ModifyTeacher(teacher);
            Assert.Equal(result == 1, backResult);
        }

        /// <summary>
        /// RemoveTeacher异常测试
        /// </summary>
        [Fact]
        public void RemoveTeacher_NotFind_ThrowException()
        {
            _dbMock.Setup(db => db.Teachers.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _teacherRepository.RemoveTeacher(111));
            Assert.Contains("查询不到ID为111的教师", ext.Message);
        }

        /// <summary>
        /// RemoveTeacher正常
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void RemoveTeacher_Default_ReturnClass(int result)
        {
            var teacher = new Teachers { Id = 111 };
            _dbMock.Setup(db => db.Teachers.Find(teacher.Id)).Returns(value: new Teachers());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _teacherRepository.RemoveTeacher(111);
            Assert.Equal(result == 1, backResult);
        }
    }



}
