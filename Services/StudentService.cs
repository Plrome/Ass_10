using Ass_10.Data;
using Ass_10.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ass_10.Services;

public class StudentService : IStudentService
{
    private readonly MyDbContext _context;
    public StudentService(MyDbContext context)
    {
        _context = context;
    }
    public async Task<IList<Student>> GetAllAsync()
    {
        return _context.Students != null ? await _context.Students.ToListAsync() : new List<Student>();

    }

    public async Task<Student?> GetOneAsync(int id)
    {
        if (_context.Students == null) return null;

        return await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Student?> AddAsync(Student entity)
    {
        if (_context.Students == null) return null;

        await _context.Students.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public Student? Edit(int id ,Student entity)
    {
        if (_context.Students == null) return null;
        
        var student = _context.Students.FirstOrDefault(x=>x.Id == entity.Id);

        student.FirstName = entity.FirstName;
        student.LastName = entity.LastName;
        student.City = entity.City;
        student.State = entity.State;

        _context.SaveChanges();

        return student;

    }

    public void Remove(int id)
    {
        var student = _context.Students.FirstOrDefault(x => x.Id == id);
        
        _context.Students.Remove(student);
        _context.SaveChanges();

    }
}