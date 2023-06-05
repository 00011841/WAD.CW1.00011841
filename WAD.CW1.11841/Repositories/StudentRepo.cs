using AutoMapper;
using WAD.CW1._11841.Data;
using WAD.CW1._11841.Interfaces;
using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public StudentRepo(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public ICollection<Student> GetAll()
        {
            return _dataContext.Students.OrderBy(p => p.StudentId).ToList();
        }

        public Student GetById(int id)
        {
            return _dataContext.Students.Where(b => b.StudentId == id).FirstOrDefault();
        }

        public Student GetByName(string title)
        {
            return _dataContext.Students.Where(b => b.Name == title).FirstOrDefault();
        }

        public bool IsExist(int id)
        {
            return _dataContext.Students.Where(b => b.StudentId == id).Any();
        }

        public bool CreateStudent(int teacherId, Student student)
        {
            var StudentteacherEntity = _dataContext.Students.Where(a => a.StudentId == teacherId).FirstOrDefault();
            _dataContext.Add(student);
            return Save();
        }
        public Student GetStudentTrimToUpper(StudentDto pokemonCreate)
        {
            return GetAll().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }
        public bool UpdateStudent(int teacherId, Student student)
        {
            _dataContext.Update(student);
            return Save();
        }

        public bool DeleteStudent(Student Student)
        {
            _dataContext.Remove(Student);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
