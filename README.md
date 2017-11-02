\# MoqEFCoreExtension
在单元测试(Unit Test)时，如果使用Moq作为隔离框架，并需要模似(Mock) DbContext(Entity Framework Core)下的DbSet&lt;Entity>，本项目给出了解决方案，可以用List&lt;Entity>或Entity[]，模拟DbSet&lt;Entity>完成测试操作。

本例是采用XUnit进行单元测试。
Demo:

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
            var answerSet = new Mock<DbSet<Answers>>().SetUpList(list);      
            //装载测试数据
            dbMock.Setup(db => db.Answers).Returns(answerSet.Object);
            //设用被测试方法
            var answers = answerRepository.GetAnswers(1);
            //断言
            Assert.Equal(2, answers.Count);
        }
