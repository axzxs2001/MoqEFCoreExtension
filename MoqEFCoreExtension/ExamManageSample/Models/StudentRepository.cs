
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamManageSample.Models.DataModels;

namespace ExamManageSample.Models
{
    /// <summary>
    /// 学生业务类
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        /// <summary>
        /// 数据操作对象
        /// </summary>
        IDBModel _db;

        public StudentRepository(IDBModel db)
        {
            _db = db;
        }

        /// <summary>
        /// 按学号查询学生
        /// </summary>
        /// <param name="sutNo">学号</param>
        /// <param name="cardID">身份证</param>
        /// <returns></returns>
        public Students GetStudent(string sutNo, string cardID)
        {
            var student = _db.Students.Find(sutNo);
            if (student.CardId == cardID)
            {
                return student;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student">学生</param>
        /// <returns></returns>
        public bool AddStudent(Students student)
        {
            _db.Students.Add(student);
            var result = _db.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 按班级ID查询全部学生
        /// </summary>
        /// <returns></returns>
        public IList GetStudentsByClsID(int clsID)
        {
            return _db.Students.Where(w => w.ClassId == clsID).Select(s => new { 学号 = s.StuNo, 姓名 = s.Name, 性别 = s.Sex, 生日 = s.Birthday, 身份证号 = s.CardId,班级名称 = s.Class.ClassName }).ToList();
        }
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="student">学生</param>
        /// <returns></returns>
        public bool ModifyStudent(Students student)
        {
            var oldStudent= _db.Students.Find(student.StuNo);
            if (oldStudent == null)
            {
                throw new Exception($"查询不到学号为{student.StuNo}的学生");
            }
            else
            {
                oldStudent.Name = student.Name;
                oldStudent.CardId = student.CardId;
                oldStudent.Birthday = student.Birthday;
                oldStudent.ClassId = student.ClassId;
                oldStudent.Sex = student.Sex;
                var result = _db.SaveChanges();
                return result > 0;
            }
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="stuNo">学号</param>
        /// <returns></returns>
        public bool RemoveStudent(string stuNo)
        {
            var oldStudent = _db.Students.Find(stuNo);
            if (oldStudent == null)
            {
                throw new Exception($"查询不到学号为{stuNo}的学生");
            }
            else
            {
                _db.Students.Remove(oldStudent);
                var result = _db.SaveChanges();
                return result > 0;
            }

        }

    }
}
