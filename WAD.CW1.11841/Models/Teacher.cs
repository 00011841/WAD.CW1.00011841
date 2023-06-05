namespace WAD.CW1._11841.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set;}
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Subjetc { get; set; }
        public string Shift { get;set; }

        public ICollection<Student> Students { get; set; }
    }
}
