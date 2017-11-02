using Moq;
using System;
using System.Collections.Generic;
using MoqEFCoreExtension;
using Xunit;
using ExamManageSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamManageSample.XUnitTest
{
    /// <summary>
    /// SubjectRepository测试类
    /// </summary>
    [Trait("TestManage", "StudentRepositoryTest测试")]
    public class StudentRepositoryTest
    {
        /// <summary>
        /// DB Mock对象
        /// </summary>
        Mock<IDBModel> _dbMock;
        /// <summary>
        /// 被测试对象
        /// </summary>
        IStudentRepository _studentRepository;
        public StudentRepositoryTest()
        {
            _dbMock = new Mock<IDBModel>();
            _studentRepository = new StudentRepository(_dbMock.Object);
        }
        /// <summary>
        /// 查询学生测试
        /// </summary>
        [Fact]
        public void GetStudent_Default_ReturnCount()
        {
            var student = new Students { StuNo = "SZ201701001", Name = "张三", CardId = "412214198808082526" };
            _dbMock.Setup(db => db.Students.Find(student.StuNo)).Returns(value: student);
            var newStuent = _studentRepository.GetStudent(student.StuNo, student.CardId);
            Assert.NotNull(newStuent);
        }
        /// <summary>
        /// 查询学生测试
        /// </summary>
        [Fact]
        public void GetStudent_Default_ReturnNull()
        {
            var student = new Students { StuNo = "SZ201701001", Name = "张三", CardId = "412214198808082526" };
            _dbMock.Setup(db => db.Students.Find(student.StuNo)).Returns(value: student);
            var newStuent = _studentRepository.GetStudent(student.StuNo, "412214198808082527");
            Assert.Null(newStuent);
        }
        /// <summary>
        /// 查询班级全部学生测试
        /// </summary>
        [Fact]
        public void GetStudentsByClsID_Default_ReturnCount()
        {
            var student = new Students { StuNo = "SZ201701001", Name = "张三", CardId = "412214198808082526",ClassId=1,Class=new Classes { ClassName="一班" } };
            var data = new List<Students> {
                new Students {
                    StuNo = "SZ201701001",
                    Name = "张三",
                    CardId = "412214198808082526",
                    ClassId = 1,
                    Class = new Classes { ClassName = "一班" }
                },
                new Students {
                    StuNo = "SZ201701002",
                    Name = "张三丰",
                    CardId = "412214198808082522",
                    ClassId = 1,
                    Class = new Classes { ClassName = "一班" }
                },
                new Students {
                    StuNo = "SZ201702001",
                    Name = "张三",
                    CardId = "412214198808082526",
                    ClassId = 2,
                    Class = new Classes { ClassName = "二班" }
                }
            };
            var studentSet = new Mock<DbSet<Students>>().SetUpList(data);
       
            _dbMock.Setup(db => db.Students).Returns(studentSet.Object);
            var students = _studentRepository.GetStudentsByClsID(1);
            Assert.Equal(2, students.Count);
        }
        /// <summary>
        /// AddStuden异常测试
        /// </summary>
        [Fact]
        public void AddStudent_AddNull_ThrowException()
        {
            var message = "AddStudent异常";
            _dbMock.Setup(db => db.Students.Add(null)).Throws(new Exception(message));
            var ext = Assert.Throws<Exception>(() => _studentRepository.AddStudent(null));
            Assert.Contains(message, ext.Message);
        }
        /// <summary>
        /// AddStudent正常添加
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void AddStudent_Default_ReturnTrue(int result)
        {
            _dbMock.Setup(db => db.Students.Add(new Students()));
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _studentRepository.AddStudent(new Students());
            Assert.Equal(result == 1, backResult);
        }
        /// <summary>
        /// ModifyStudent异常测试
        /// </summary>
        [Fact]
        public void ModifyStudent_NotFind_ThrowException()
        {

            _dbMock.Setup(db => db.Students.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _studentRepository.ModifyStudent(new Students { StuNo="SZ20180808001" }));
            Assert.Contains("查询不到学号为SZ20180808001的学生", ext.Message);
        }
        /// <summary>
        /// ModifyStudent正常测试
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ModifyStudent_Default_ReturnTrue(int result)
        {
            var student = new Students { StuNo = "SZ20180808001" };
            _dbMock.Setup(db => db.Students.Find(student.StuNo)).Returns(value: new Students());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _studentRepository.ModifyStudent(student);
            Assert.Equal(result == 1, backResult);
        }

        /// <summary>
        /// RemoveStudent异常测试
        /// </summary>
        [Fact]
        public void RemoveStudent_NotFind_ThrowException()
        {
            _dbMock.Setup(db => db.Students.Find()).Returns(value: null);
            var ext = Assert.Throws<Exception>(() => _studentRepository.RemoveStudent("SZ20180808001"));
            Assert.Contains("查询不到学号为SZ20180808001的学生", ext.Message);
        }

        /// <summary>
        /// RemoveStudent正常
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void RemoveStudent_Default_ReturnClass(int result)
        {
            var student = new Students { StuNo = "SZ20180808001" };
            _dbMock.Setup(db => db.Students.Find(student.StuNo)).Returns(value: new Students());
            _dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            var backResult = _studentRepository.RemoveStudent("SZ20180808001");
            Assert.Equal(result == 1, backResult);
        }

    }



}
