
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamManageSample.Models
{
    /// <summary>
    /// 班级业务类
    /// </summary>
    public class TeacherRepository : ITeacherRepository
    {
        /// <summary>
        /// 数据操作对象
        /// </summary>
        IDBModel db;

        public TeacherRepository(IDBModel testManageDB)
        {
            db = testManageDB;
        }
        /// <summary>
        /// 添加教师
        /// </summary>
        /// <param name="teacher">教师</param>
        /// <returns></returns>
        public bool AddTeacher(Teachers teacher)
        {
            db.Teachers.Add(teacher);
            var result = db.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 查询全部教师
        /// </summary>
        /// <returns></returns>
        public IList GetTeachers()
        {
            return db.Teachers.Select(s => new { 编号 = s.Id, 名称 = s.Name, 教师编码 = s.TeaacherNo, 密码 = s.Password }).ToList();
        }
        /// <summary>
        /// 修改教师
        /// </summary>
        /// <param name="teacher">教师</param>
        /// <returns></returns>
        public bool ModifyTeacher(Teachers teacher)
        {
            var oldTeacher = db.Teachers.Find(teacher.Id);
            if (oldTeacher == null)
            {
                throw new Exception($"查询不到ID为{teacher.Id}的教师");
            }
            else
            {
                oldTeacher.Name = teacher.Name;
                oldTeacher.TeaacherNo = teacher.TeaacherNo;
                oldTeacher.Password = teacher.Password;
                var result = db.SaveChanges();
                return result > 0;
            }
        }
        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="id">教师ID</param>
        /// <returns></returns>
        public bool RemoveTeacher(int id)
        {
            var oldTeacher = db.Teachers.Find(id);
            if (oldTeacher == null)
            {
                throw new Exception($"查询不到ID为{id}的教师");
            }
            else
            {
                db.Teachers.Remove(oldTeacher);
                var result = db.SaveChanges();
                return result > 0;
            }

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="teacherNo">教师编号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public Teachers Login(string teacherNo, string password)
        {
            return db.Teachers.SingleOrDefault(s => s.TeaacherNo == teacherNo && s.Password == password);
        }

    }
}
