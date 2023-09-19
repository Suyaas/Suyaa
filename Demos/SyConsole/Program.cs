// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
{
    sy.Console.WriteLine($"Hello, {color}", color);
}

sy.Console.SignInfo("SY", "SignInfo");
sy.Console.Info("Info");
sy.Console.SignWarn("SY", "SignWarn");
sy.Console.Warn("Warn");
sy.Console.SignError("SY", "SignError");
sy.Console.Error("Error");
