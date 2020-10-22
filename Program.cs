using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Final_Project
{
    class Project
    {
        public static void Main(string[] args)
        {
            //reads a specific .txt file and imports it
            var classFile = System.IO.File.ReadAllLines(@"C:\Users\Lukavago\Desktop\names.txt");
            //converts imported .txt file to list classlist
            var classList = new List<string>(classFile);
            //creates empty list for Student objects
            var studentList = new List<Student>();
            //foreach statement that splits the strings in classlist on the ','
            foreach(string str in classList)
            {
                var line = str.Split(',');
                bool exp = false;
                //if statement that sets exp as a bool depending if the second string has '*' or not
                if(line[1] == "*")
                {
                    line[1] = "true";
                    exp = bool.Parse(line[1]);
                }
                //sets name to the names listed in .txt
                string name = line[0];
                //calls the Student class, creating students with name and exp, adds to studentList
                studentList.Add(new Student(name, exp));
            }
            //creates an empty array called teams
            var teams = new string[13, 3];
            //calls the makeTeams method with studentList
            teams = makeTeams(studentList);
            //calls the writeTeams method with teams
            writeTeams(teams);
        }
        private static String[,] makeTeams(List<Student> studentList)
        {
            //initializes my variables, including random, teams[,] and expStudent<>
            int x = 0, y = 0, n = 0, m = 0;
            var random = new Random();
            var teams = new string[13, 3];
            var expStudent = new List<Student>();
            //while loop finds all students in studentList with experiance and puts them in expStudent, removing them from studentList
            while (x < studentList.Count)
            {
                Student student = studentList[x];
                if (student.exp == true)
                {
                    expStudent.Add(student);
                    studentList.RemoveAt(x);
                }
                x++;
            }
            x = 0;
            //while loop to create teams so long as studentList is populated
            while (y < studentList.Count)
            {
                //ensures that so long as expStudent is populated, each group gains 1 student where exp = true
                if (x < expStudent.Count)
                {
                    //ensures random groups of 2 or 3, removing students from studentList and expStudent as they are placed into groups
                    if (studentList.Count + expStudent.Count > 4)
                    {
                        int index = random.Next(expStudent.Count);
                        Student student = expStudent[index];
                        teams[n, 0] = student.name;
                        expStudent.RemoveAt(index);
                        for (m = 1; m < 3; m++)
                        {
                            index = random.Next(studentList.Count);
                            student = studentList[index];
                            teams[n, m] = student.name;
                            studentList.RemoveAt(index);
                        }
                        n++;
                    }
                    else
                    {
                        int index = random.Next(expStudent.Count);
                        Student student = expStudent[index];
                        teams[n, 0] = student.name;
                        expStudent.RemoveAt(index);
                        for (m = 0; m < 2; m++)
                        {
                            index = random.Next(studentList.Count);
                            student = studentList[index];
                            teams[n, m] = student.name;
                            studentList.RemoveAt(index);
                        }
                        n++;
                    }
                }
                //continues making groups after expStudent is depopulated
                else
                {
                    //again, ensures that groups are three or less
                    if (studentList.Count > 4)
                    {
                        for (m = 0; m < 3; m++)
                        {
                            int index = random.Next(studentList.Count);
                            Student student = studentList[index];
                            teams[n, m] = student.name;
                            studentList.RemoveAt(index);
                        }
                        n++;
                    }
                    else if (studentList.Count == 4)
                    {
                        for (m = 0; m < 2; m++)
                        {
                            int index = random.Next(studentList.Count);
                            Student student = studentList[index];
                            teams[n, m] = student.name;
                            studentList.RemoveAt(index);
                        }
                        n++;
                    }
                    else
                    {
                        for (m = 0; m < 3; m++)
                        {
                            int index = random.Next(studentList.Count);
                            Student student = studentList[index];
                            teams[n, m] = student.name;
                            studentList.RemoveAt(index);
                        }
                        n++;
                    }
                }
            }
            //returns teams to main
            return teams;
        }
        public static void writeTeams(String[,] teams)
        {
            //takes all of the information from teams and writes it to a different .txt file
            int n = 0;
            using (StreamWriter sw = new StreamWriter("C:\\Users\\Lukavago\\Desktop\\groups.txt"))
            {
                while (teams[n, 0] != null)
                {
                    //calls on enum Team to properly name each team
                    var team = (Team)n;
                    sw.WriteLine(team + ":");
                    if (teams[n, 2] == null)
                    {
                        sw.WriteLine($"{teams[n, 0]}, {teams[n, 1]}");
                    }
                    else
                    {
                        sw.WriteLine($"{teams[n, 0]}, {teams[n, 1]}, {teams[n, 2]}");
                    }
                    n++;
                }
            }
        }
        private enum Team
        {
            //private enum for Team names
            Team_1,
            Team_2,
            Team_3,
            Team_4,
            Team_5,
            Team_6,
            Team_7,
            Team_8,
            Team_9,
            Team_10,
            Team_11,
            Team_12,
            Team_13,
        }
    }
    class Student
    {
        //Student class for creating students
        public string name;
        public bool exp;

        public Student(string name, bool exp)
        {
            this.name = name;
            this.exp = exp;
        }
    }
}
