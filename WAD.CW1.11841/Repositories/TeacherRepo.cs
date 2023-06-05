using AutoMapper;
using WAD.CW1._11841.Data;
using WAD.CW1._11841.Interfaces;
using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Repositories
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public TeacherRepo(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public ICollection<Teacher> GetAll()
        {
            return _dataContext.Teacher.ToList();
        }

        public Teacher GetById(int id)
        {
            return _dataContext.Teacher.Where(b => b.TeacherId == id).FirstOrDefault();
        }

        public Teacher GetByName(string name)
        {
            return _dataContext.Teacher.Where(b => b.TeacherName == name).FirstOrDefault();
        }

        public bool IsExist(int id)
        {
            return _dataContext.Teacher.Where(b => b.TeacherId == id).Any();
        }

        public bool CreateTeacher(Teacher Teacher)
        {
            //var TeachersBookEntity = _dataContext.Teacher.Where(a => a.TeacherId == bookId).FirstOrDefault();
            _dataContext.Add(Teacher);
            return Save();
        }
        public Teacher GetTeacherTrimToUpper(TeacherDto TeacherCreate)
        {
            return GetAll().Where(c => c.TeacherName.Trim().ToUpper() == TeacherCreate.TeacherName.TrimEnd().ToUpper())
                .FirstOrDefault();
        }
        public bool UpdateTeacher(Teacher Teacher)
        {
            _dataContext.Update(Teacher);
            return Save();
        }

        public bool DeleteTeacher(Teacher Teacher)
        {
            _dataContext.Remove(Teacher);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
