
## MoqEFCoreExtension
在单元测试(Unit Test)时，如果使用Moq作为隔离框架，并需要模似(Mock) DbContext(Entity Framework Core)下的DbSet&lt;Entity>，本项目给出了解决方案，可以用List&lt;Entity>或Entity[]，模拟DbSet&lt;Entity>完成测试操作。
本例是采用XUnit进行单元测试。
Demo:
### 模拟DbSet
```C# 
        /// <summary>
        /// 按问题查询答案
        /// </summary>
        [Fact]
        public void GetAnswer_Default_ReturnCount()
        {
            //Mock DbContext
            var dbMock = new Mock<TestManageDBContext>();
            //实例化被测试方法
            var answerRepository = new AnswerRepository(dbMock.Object);
            //模拟集合
            var list = new List<Answers> {
                new Answers { Id = 1, Answer = "答案1",IsAnswer=true,QuestionId=1 },
                new Answers { Id = 2, Answer = "答案2",IsAnswer=false,QuestionId=1 }
            };
            //Mock  DbSet<Answers>
            var answerSet = new Mock<DbSet<Answers>>().SetupList(list);      
            //装载测试数据
            dbMock.Setup(db => db.Answers).Returns(answerSet.Object);
            //设用被测试方法，在该方中用到了TestManageDBContext.Answers.Where执行查询，所以上面代码要把List<Answers>装载成DbSet<Answers>
            var answers = answerRepository.GetAnswers(1);
            //断言
            Assert.Equal(2, answers.Count);
        }
```
模拟DbSet下的SingleOrDefault
 ```C#
        /// <summary>
        /// ModifyAnswer正常测试
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ModifyAnswer_Default_ReturnTrue(int result)
        {   
            //Mock DbContext
            var dbMock = new Mock<TestManageDBContext>();
            //实例化被测试方法
            var answerRepository = new AnswerRepository(dbMock.Object);  
            //模拟集合
            var list = new List<Answers> {
                new Answers { Id = 1, Answer = "答案1",IsAnswer=true,QuestionId=1 },
                new Answers { Id = 2, Answer = "答案2",IsAnswer=false,QuestionId=1 }
            };
            //Mock DbContext DbSet<Answers>
            var answerSet = new Mock<DbSet<Answers>>().SetupList(list);
            //用list装载DbSet<Answers>
            dbMock.Setup(db => db.Answers).Returns(answerSet.Object);
            //模拟保存方法返回值
            dbMock.Setup(db => db.SaveChanges()).Returns(value: result);
            //修改方法的入参
            var answer = new Answers { Id = 1 };
            //调用修改方法，修改方法用中到了 db.Answers.SingleOrDefault(s=>s.Id==answer.Id)，只要模拟了DbSet<Answers>，SingleOrDefault就可调用了
            var backResult = answerRepository.ModifyAnswer(answer);
            Assert.Equal(result == 1, backResult);
        }
```
