using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Interfaces
{
    public interface ITeacherRepo
    {
        ICollection<Teacher> GetAll();
        Teacher GetById(int id);
        Teacher GetByName(string name);
        Teacher GetTeacherTrimToUpper(TeacherDto reacherCreate);
        bool CreateTeacher(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool DeleteTeacher(Teacher teacher);
        bool Save();
        bool IsExist(int id);
    }
}
