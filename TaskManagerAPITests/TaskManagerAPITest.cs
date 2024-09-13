using TaskManagerAPI;

namespace TaskManagerAPITests
{
    public class TaskManagerAPITest
    {
        [Fact]
        public void Test1()
        {
            int a = 2; int b = 2;
            Assert.Equal(a, b);
        }

        [Fact]
        public void Test2()
        {
            // tester om der er hul igennem. Denne test kan slettes. 
            TaskManager test = new TaskManager();
            var data = test.GetAllTasks();
            Assert.True(true);
        }
    }
}