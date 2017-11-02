using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamManageSample.Models
{
    public interface IClassTestRepository
    {
        /// <summary>
        /// 添加考试班级
        /// </summary>
        /// <param name="clsTest">考试班级</param>
        /// <returns></returns>
        bool AddClassTest(ClassTests clsTest);
        /// <summary>
        /// 查询全部考试班级
        /// </summary>
        /// <returns></returns>
        IList GetClassTests();
        /// <summary>
        /// 修改考试班级
        /// </summary>
        /// <param name="clsTest">考试班级</param>
        /// <returns></returns>
        bool ModifyClassTest(ClassTests clsTest);
        /// <summary>
        /// 删除考试班级
        /// </summary>
        /// <param name="id">考试班级ID</param>
        /// <returns></returns>
        bool RemoveClassTest(int id);

        /// <summary>
        /// 按班级获取试卷
        /// </summary>
        /// <param name="clsID">班级ID</param>
        /// <returns></returns>
        Tests GetTestByClassID(int clsID);
    }
}
