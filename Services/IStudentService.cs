using Ass_10.Data.Entities;

namespace Ass_10.Services;

public interface IStudentService
{
    public Task<IList<Student>> GetAllAsync();
    public Task<Student>? GetOneAsync(int id);
    public Task<Student>? AddAsync(Student entity);
    public Student? Edit(int id , Student entity);
    public void Remove(int id);
}