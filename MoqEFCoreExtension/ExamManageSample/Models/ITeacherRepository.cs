
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
    public interface ITeacherRepository 
    {

        /// <summary>
        /// 添加教师
        /// </summary>
        /// <param name="teacher">教师</param>
        /// <returns></returns>
        bool AddTeacher(Teachers teacher);

        /// <summary>
        /// 查询全部教师
        /// </summary>
        /// <returns></returns>
        IList GetTeachers();

        /// <summary>
        /// 修改教师
        /// </summary>
        /// <param name="teacher">教师</param>
        /// <returns></returns>
         bool ModifyTeacher(Teachers teacher);

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="id">教师ID</param>
        /// <returns></returns>
        bool RemoveTeacher(int id);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="teacherNo">老师编号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Teachers Login(string teacherNo, string password);

    }
}
