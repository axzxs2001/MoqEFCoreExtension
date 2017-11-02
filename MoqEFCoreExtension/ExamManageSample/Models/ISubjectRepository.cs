
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamManageSample.Models
{
    /// <summary>
    /// 课目业务类
    /// </summary>
    public interface ISubjectRepository
    {

        /// <summary>
        /// 添加科目
        /// </summary>
        /// <param name="subject">科目</param>
        /// <returns></returns>
        bool AddSubject(Subjects subject);

        /// <summary>
        /// 查询全部科目
        /// </summary>
        /// <returns></returns>
        IList GetSubjects();

        /// <summary>
        /// 修改科目
        /// </summary>
        /// <param name="subject">科目</param>
        /// <returns></returns>
         bool ModifySubject(Subjects subject);

        /// <summary>
        /// 删除科目
        /// </summary>
        /// <param name="id">科目ID</param>
        /// <returns></returns>
         bool RemoveSubject(int id);      

    }
}
