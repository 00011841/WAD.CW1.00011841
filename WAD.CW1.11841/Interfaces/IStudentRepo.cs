using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Interfaces
{
    public interface IStudentRepo
    {
        ICollection<Student> GetAll();
        Student GetById(int id);
        Student GetByName(string name);
        Student GetStudentTrimToUpper(StudentDto studentCreate);
        bool CreateStudent(int teacherId, Student student);
        bool UpdateStudent(int teacherId, Student student);
        bool DeleteStudent(Student student);
        bool Save();
        bool IsExist(int id);
    }
}
