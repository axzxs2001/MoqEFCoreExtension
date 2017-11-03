
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
    public class AnswerRepository : IAnswerRepository
    {
        /// <summary>
        /// 数据操作对象
        /// </summary>
        IDBModel db;

        public AnswerRepository(IDBModel testManageDB)
        {
            db = testManageDB;
        }
        /// <summary>
        /// 添加答案
        /// </summary>
        /// <param name="answer">科目</param>
        /// <returns></returns>
        public bool AddAnswer(Answers answer)
        {
            db.Answers.Add(answer);
            var result = db.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 按问题ID查询答案
        /// </summary>
        /// <returns></returns>
        public IList GetAnswers(int questionID)
        {
            return db.Answers.Where(w=>w.QuestionId==questionID).Select(s => new { 编号 = s.Id, 答案 = s.Answer, 是否正确答案=s.IsAnswer }).ToList();
        }
        /// <summary>
        /// 修改答案
        /// </summary>
        /// <param name="answer">答案</param>
        /// <returns></returns>
        public bool ModifyAnswer(Answers answer)
        {
            var oldAnswer = db.Answers.SingleOrDefault(s=>s.Id==answer.Id);
            if (oldAnswer == null)
            {
                throw new Exception($"查询不到ID为{answer.Id}的答案");
            }
            else
            {
                oldAnswer.IsAnswer = answer.IsAnswer;
                oldAnswer.QuestionId = answer.QuestionId;
              
                var result = db.SaveChanges();
                return result > 0;
            }
        }
        /// <summary>
        /// 删除答案
        /// </summary>
        /// <param name="id">答案ID</param>
        /// <returns></returns>
        public bool RemoveAnswer(int id)
        {
            var oldAnswer = db.Answers.Find(id);
            if (oldAnswer == null)
            {
                throw new Exception($"查询不到ID为{id}的答案");
            }
            else
            {
                db.Answers.Remove(oldAnswer);
                var result = db.SaveChanges();
                return result > 0;
            }

        }

    }
}
