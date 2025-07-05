﻿public static class UserRepository
{
    private static List<User> users = new();
    private static int nextId = 1;

    public static List<User> GetAll() => users;

    public static User? GetById(int id) => users.FirstOrDefault(u => u.Id == id);

    public static User Add(User user)
    {
        user.Id = nextId++;
        users.Add(user);
        return user;
    }

    public static bool Update(int id, User updatedUser)
    {
        var user = GetById(id);
        if (user == null) return false;

        user.FullName = updatedUser.FullName;
        user.Email = updatedUser.Email;
        user.Department = updatedUser.Department;
        return true;
    }

    public static bool Delete(int id)
    {
        var user = GetById(id);
        if (user == null) return false;
        users.Remove(user);
        return true;
    }
}
