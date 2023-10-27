using automapReflection;

internal class Program
{
    private static void Main(string[] args)
    {
        //singleObjectMap();
        
        
        List<User> users = new List<User>();
        Console.Write("--> list ");
        for (int i = 0; i < 10; i++)
        {
            var user = new User { id = i, Name = $"name {i}", LastName = $"lastname {i}" };
            users.Add(user);
        }
        Console.WriteLine("<--");
      

        Console.Write("--> map ");
        var userResponse = Mapper.Map<IEnumerable<UserResponse>>(users);
        Console.WriteLine("<--");

        foreach (var item in userResponse)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.LastName);
        }

    }

    private static void singleObjectMap()
    {
        User u = new User
        {
            id = 1,
            Name = "test",
            LastName = "test lastname",
        };

        var user = Mapper.Map<UserResponse>(u);
        
        Console.WriteLine(user.Name);
        Console.WriteLine(user.LastName);
    }
}
public class User 
{
    public int id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}
public class UserResponse
{
    public string Name { get; set; }
    public string LastName { get; set; }

}


