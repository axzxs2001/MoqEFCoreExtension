
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
    public class SubjectRepository : ISubjectRepository
    {
        /// <summary>
        /// 数据操作对象
        /// </summary>
        IDBModel db;

        public SubjectRepository(IDBModel testManageDB)
        {
            db = testManageDB;
        }
        /// <summary>
        /// 添加科目
        /// </summary>
        /// <param name="subject">科目</param>
        /// <returns></returns>
        public bool AddSubject(Subjects subject)
        {
            db.Subjects.Add(subject);
            var result = db.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 查询全部科目
        /// </summary>
        /// <returns></returns>
        public IList GetSubjects()
        {
            return db.Subjects.Select(s => new { 编号 = s.Id, 科目名称 = s.Name }).ToList();
        }
        /// <summary>
        /// 修改科目
        /// </summary>
        /// <param name="subject">科目</param>
        /// <returns></returns>
        public bool ModifySubject(Subjects subject)
        {
            var oldSubject = db.Subjects.Find(subject.Id);
            if (oldSubject == null)
            {
                throw new Exception($"查询不到ID为{subject.Id}的科目");
            }
            else
            {
                oldSubject.Name = subject.Name;
                var result = db.SaveChanges();
                return result > 0;
            }
        }
        /// <summary>
        /// 删除科目
        /// </summary>
        /// <param name="id">科目ID</param>
        /// <returns></returns>
        public bool RemoveSubject(int id)
        {
            var oldSubject = db.Subjects.Find(id);
            if (oldSubject == null)
            {
                throw new Exception($"查询不到ID为{id}的科目");
            }
            else
            {
                db.Subjects.Remove(oldSubject);
                var result = db.SaveChanges();
                return result > 0;
            }

        }

    }
}
