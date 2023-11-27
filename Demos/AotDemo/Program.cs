// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.Title = sy.Assembly.FullName;

sy.Logger.Factory.UseFile(sy.IO.GetExecutionPath("./log"));
sy.Logger.Create();

sy.Logger.Info(sy.Assembly.FullName);
