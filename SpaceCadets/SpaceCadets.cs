﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Student{
    public string Name = "";
    public string Group = "";
    public string Discipline = "";
    public int Mark = 0;
}

class Program
{
    static void Main(string[] args){
        string pathInp = args[0];
        string pathOut = args[1];

        JObject readFile = JObject.Parse(File.ReadAllText(pathInp));
        string taskName = readFile["taskName"]?.ToString() ?? "";
        List<Student> marks = readFile["data"]?.ToObject<List<Student>>() 
        ?? new List<Student>();

        List<dynamic> result = new List<dynamic>();

        switch (taskName){
            case "GetStudentsWithHighestGPA":
                result = GetStudentsWithHighestGPA(marks);
                break;
            case "CalculateGPAByDiscipline":
                result = CalculateGPAByDiscipline(marks);
                break;
            case "GetBestGroupsByDiscipline":
                result = GetBestGroupsByDiscipline(marks);
                break;
            default:
                result = new List<dynamic>();
                break;
        } 

        string response = JsonConvert.SerializeObject(new {Response = result}, Formatting.Indented);

        File.WriteAllText(pathOut, response);

        static List<dynamic> GetStudentsWithHighestGPA(List<Student> marks){
            var studentsWithHighestGPA = marks
            .Where(m => m.Mark == marks.Max(m => m.Mark))
            .Select(m => new {Cadet = m.Name, GPA = m.Mark}).Distinct().ToList<dynamic>();
            return studentsWithHighestGPA;
        }

        static List<dynamic> CalculateGPAByDiscipline(List<Student> marks){
            var GPAByDiscipline = marks
            .GroupBy(m => m.Discipline)
            .Select(d => new Dictionary<string, double> {[d.Key] = d.Average(m => m.Mark)})
            .ToList<dynamic>();
            return GPAByDiscipline;
        }

        static List<dynamic> GetBestGroupsByDiscipline(List<Student> marks){
            var BestGroupsByDiscipline = marks.GroupBy(m => new {m.Discipline, m.Group})
            .Select(d => new
            {
                Discipline = d.Key.Discipline, 
                Group = d.Key.Group, 
                GPA = d.Average(m => m.Mark)
            }).GroupBy(d => d.Discipline).Select(d => new 
            {
                Discipline = d.Key, 
                Group = d.OrderByDescending(dd => dd.GPA).FirstOrDefault()?.Group,
                GPA = d.Max(d => d.GPA)
            }).ToList<dynamic>();
            
            return BestGroupsByDiscipline;
        }
    }
}