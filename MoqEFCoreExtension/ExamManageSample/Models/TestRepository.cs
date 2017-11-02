
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
    public class TestRepository : ITestRepository
    {
        /// <summary>
        /// 数据操作对象
        /// </summary>
        IDBModel db;

        public TestRepository(IDBModel testManageDB)
        {
            db = testManageDB;
        }
        /// <summary>
        /// 添加考试
        /// </summary>
        /// <param name="test">考试</param>
        /// <returns></returns>
        public bool AddTest(Tests test)
        {
            db.Tests.Add(test);
            var result = db.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 按科目ID查询试卷
        /// </summary>
        /// <returns></returns>
        public IList GetTests(int subjectID)
        {
            return db.Tests.Where(w => w.SubjectId == subjectID).Select(s => new { 编号 = s.Id, 试卷名称 = s.TestName, 老师编号 = s.TeacherId, 教师 = s.Teacher.Name, 科目编号 = s.SubjectId, 科目名称 = s.Subject.Name }).ToList();
        }
        /// <summary>
        /// 按ID查询试卷
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public Tests GetTest(int id)
        {
            return db.Tests.Find(id);
        }

        /// <summary>
        /// 修改试卷
        /// </summary>
        /// <param name="test">答案</param>
        /// <returns></returns>
        public bool ModifyTest(Tests test)
        {
            var oldTest = db.Tests.Find(test.Id);
            if (oldTest == null)
            {
                throw new Exception($"查询不到ID为{test.Id}的试卷");
            }
            else
            {
                oldTest.TestName = test.TestName;
                oldTest.SubjectId = test.SubjectId;
                oldTest.TeacherId = test.TeacherId;
                var result = db.SaveChanges();
                return result > 0;
            }
        }
        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id">试卷ID</param>
        /// <returns></returns>
        public bool RemoveTest(int id)
        {
            var oldTest = db.Tests.Find(id);
            if (oldTest == null)
            {
                throw new Exception($"查询不到ID为{id}的试卷");
            }
            else
            {
                db.Tests.Remove(oldTest);
                var result = db.SaveChanges();
                return result > 0;
            }

        }

    }
}
