using BlazorToDoList.Model;
using System.Data.SQLite;
using Task = BlazorToDoList.Model.Task;


namespace BlazorToDoList.Service
{
    public class ToDoService 
	{
		int userId;
        public ToDoService(UserState userState)
        {
				userId = userState.UserId; //"F:\\Tasks.sqlite"
        }
        // Connection may be different
        private readonly string _connectionString = @"Data Source=C:\Users\gldot\Downloads\Rebuilt_TasksDB.sqlite;";
        private const string sql_simple = "SELECT * FROM Tasks";
		private const string sql_extended =
		"SELECT TaskId, Tasks.SubjectId,Description,DueDate, Priority,IsCompleted,Subject" +
		" FROM Tasks INNER JOIN Subjects ON Subjects.SubjectId = Tasks.SubjectId" + 
		" WHERE UserId=";
		public void AddTask(Task task)
		{
			string insertSql = $"INSERT INTO Tasks (UserId,SubjectId,Description,DueDate,Priority) " +
			 $"VALUES ('{userId.ToString()}', '{task.TaskSubject.SubjectId}', " +
			 $"'{task.Description}'," +
			 $" '{task.DueDate.ToString("yyyy-MM-dd")}'," +
			 $" '{task.Priority}')";
			int result = ExecuteSQL(insertSql);
		}

		public List<Task> GetAllTasks()
		{
			string sql = sql_extended + userId;
			List<Task> list = new List<Task>();
			using (var connection = new SQLiteConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SQLiteCommand(sql, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Task task = new Task();
							task.TaskId = int.Parse(reader["TaskId"].ToString());
							task.Description = reader["Description"].ToString();
							task.DueDate = DateTime.Parse(reader["DueDate"].ToString());
							task.Priority = int.Parse(reader["Priority"].ToString());
							int subjId = int.Parse(reader["SubjectId"].ToString());
							string subjname = reader["Subject"].ToString();
							task.TaskSubject = new Subject(subjId, subjname);
							int done = int.Parse(reader["IsCompleted"].ToString());
                            if (done > 0) 
								task.IsCompleted= true;
							list.Add(task);
						}
					}
				}
			}
			return list;
		}

		public Task GetTaskById(int id)
		{
			var service = new Task();
			string sql = $"SELECT * FROM Tasks WHERE TaskId = {id}";
			Task task = new Task();
			using (var connection = new SQLiteConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SQLiteCommand(sql, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							task.TaskId = int.Parse(reader["TaskId"].ToString());
							task.Description = reader["Description"].ToString();
							task.DueDate = DateTime.Parse(reader["DueDate"].ToString());
							task.Priority = int.Parse(reader["Priority"].ToString());
							int subjId = int.Parse(reader["SubjectId"].ToString());
							task.TaskSubject = new Subject(subjId);
                            int done = int.Parse(reader["IsCompleted"].ToString());
                            if (done > 0)
                                task.IsCompleted = true;
                        }
                    }
				}
			}
			return task;
		}

		public int RemoveTask(int taskId)
		{
			string sql = $"DELETE FROM Tasks WHERE TaskId = {taskId}";
			int num = ExecuteSQL(sql);
			return num;
		}

		public void UpdateTask(Task task)
		{
			string updateSql = $"UPDATE Tasks SET Description = '{task.Description}', " +
				$"Priority = '{task.Priority}', SubjectId = '{task.TaskSubject.SubjectId}' , " +
				$"DueDate='{task.DueDate.ToString("yyyy-MM-dd")}',IsCompleted='{Convert.ToInt32(task.IsCompleted)}'"  +
				$" WHERE TaskId = '{task.TaskId}'";
			int updated = ExecuteSQL(updateSql);
		}
		// Generic SQL Command
		private int ExecuteSQL(string sql)
		{
			// Connect to DB
			var connection = new SQLiteConnection(_connectionString);
			connection.Open();
			// Run the Command and read data
			var command = new SQLiteCommand(sql, connection);
			// Execute Update/Insert/Delete
			int result = command.ExecuteNonQuery();
			return result;
		}
        public List<Subject> GetAllSubjects()
        {
            string sql = "SELECT * FROM Subjects";
            List<Subject> list = new List<Subject>();
            // Connect to DB
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            var command = new SQLiteCommand(sql, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["SubjectId"].ToString());

                    string name = reader["Subject"].ToString();
                    Subject subj = new Subject(id, name);
                    list.Add(subj);
                }
            }

            return list;

        }
    }
}
