namespace CS_TaskContinue_With
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileOperations operations = new FileOperations();
            Console.WriteLine("DEMO Task Continue With");
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
                        throw ex;
                    }
                }).ContinueWith(t =>
                {
                    try
                    {
                        if (t.Status == TaskStatus.Faulted)
                        {
                            throw t.Exception;
                        }
                        else
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
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
            Console.ReadLine(); 
        }
    }
}
