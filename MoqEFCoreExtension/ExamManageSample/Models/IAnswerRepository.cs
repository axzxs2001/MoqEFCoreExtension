
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamManageSample.Models
{
    /// <summary>
    /// 答案业务类
    /// </summary>
    public interface IAnswerRepository 
    {



        /// <summary>
        /// 添加答案
        /// </summary>
        /// <param name="answer">科目</param>
        /// <returns></returns>
        bool AddAnswer(Answers answer);
        /// <summary>
        /// 按问题ID查询答案
        /// </summary>
        /// <returns></returns>
         IList GetAnswers(int questionID);
        /// <summary>
        /// 修改答案
        /// </summary>
        /// <param name="answer">答案</param>
        /// <returns></returns>
         bool ModifyAnswer(Answers answer);
        /// <summary>
        /// 删除答案
        /// </summary>
        /// <param name="id">答案ID</param>
        /// <returns></returns>
         bool RemoveAnswer(int id);

    }
}
