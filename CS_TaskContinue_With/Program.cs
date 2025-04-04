namespace CS_TaskContinue_With
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileOperations operations = new FileOperations();
            Console.WriteLine("DEMO Task Continue With");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            try
            {
                Task task = Task.Factory.StartNew<string>(() =>
                {
                    try
                    {
                        string fileContent = operations.ReadFileOne(@"e:\file11.txt");
                        Console.WriteLine(fileContent);
                        return fileContent;
                    }
                    catch (Exception ex)
                    {
                        //Cancel the Task 
                      cts.Cancel();
                        Console.WriteLine("The Task is cancelled");
                       throw ex;
                    }
                }, token).ContinueWith(t =>
                {
                    try
                    {
                        if (t.Status == TaskStatus.Faulted)
                        {
                            Console.WriteLine("Task is at fault status");
                            throw t.Exception;
                        }
                        else if (!token.IsCancellationRequested)
                        {
                            var result = operations.WriteFile(@"e:\file2.txt", t.Result);
                            if (result)
                            {
                                Console.WriteLine("Data is written in the file");
                            }
                            else
                            {
                                Console.WriteLine("Write File Failed..");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred in Task {ex.Message}");
                    }
                },token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
            Console.ReadLine(); 
        }
    }
}
