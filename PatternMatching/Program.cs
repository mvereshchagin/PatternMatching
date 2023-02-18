// See https://aka.ms/new-console-template for more information
using PatternMatching;

Console.WriteLine("Hello, World!");

Person[] persons = {
    new Student(FirstName: "Ivan", SecondName: "Ivanov", Gender: Gender.Male,
        DateOfBirth: new DateOnly(year: 1980, month: 10, day: 9), Address: "Москва, Кремль",
        Phone: "+791111111111", Email: "superpups@mail.ru", Program: "Прикладная математика",
        YearOfEnter: 2015),
    new Student(FirstName: "Maria", SecondName: "Kuznetsova", Gender: Gender.Female,
        DateOfBirth: new DateOnly(year: 1970, month: 12, day: 2), Address: "Калининград, Невского, 14",
        Email: "sexymaria@yandex.ru", Program: "Лингвистика",
        YearOfEnter: 2020, IsMarried: true),
    new Student(FirstName: "Elena", SecondName: "Beloshapskaya", Gender: Gender.Female,
        DateOfBirth: new DateOnly(year: 1978, month: 3, day: 24), Address: "Калининград, Майский пер., 3",
        Phone: "+79555656463", Program: "Юриспруденция",
        YearOfEnter: 2020, IsMarried: false),
    new Teacher(FirstName: "Имммануил", SecondName: "Капуста", Gender: Gender.Male,
        DateOfBirth: new DateOnly(year: 1950, month: 4, day: 22), Address: "Москва, Кремль",
        Phone: "+7922222222", Email: "teacher@mail.ru", Salary: 10000),
    new Teacher(FirstName: "Olesya", SecondName: "Vasilieva", Gender: Gender.Female,
        DateOfBirth: new DateOnly(year: 1922, month: 5, day: 17), Address: "Москва, Кремль",
        Phone: "+7922222222", Email: "teacher@mail.ru", Salary: 10000, IsMarried: true),
    new Teacher(FirstName: "Kristina", SecondName: "Krivova", Gender: Gender.Female,
        DateOfBirth: new DateOnly(year: 1990, month: 4, day: 21), Salary: 5000, IsMarried: false),
};

foreach (var person in persons)
{
    if (person is Teacher teacher)
        Console.WriteLine($"Teacher: \r\n {teacher}");
    if (person is Student student)
        Console.WriteLine($"Student: \r\n {student}");
}

var (firstName, secondName, gender, dateOfBirth, address, phone, email, isMarries) = persons[0];
    
//foreach(var person in persons)
//{
//    Console.WriteLine("------ Start GenerateHello ----------");
//    Console.WriteLine(GenerateHello(person));
//    Console.WriteLine("------ End GenerateHello ----------\r\n");


//    Console.WriteLine("------ Start GenerateHello2 ----------");
//    Console.WriteLine(GenerateHello2(person));
//    Console.WriteLine("------ End GenerateHello2 ----------\r\n");

//    Console.WriteLine("------ Start GenerateHello3 ----------");
//    Console.WriteLine(GenerateHello3(person));
//    Console.WriteLine("------ End GenerateHello3 ----------\r\n");

//    Console.WriteLine("------ Start GenerateHello4 ----------");
//    Console.WriteLine(GenerateHello4(person));
//    Console.WriteLine("------ End GenerateHello4 ----------\r\n");
//}

// PrintHelloForArray(persons, GenerateHello, nameof(GenerateHello));
PrintHelloForArray(persons, GenerateHello2, nameof(GenerateHello2));
PrintHelloForArray(persons, GenerateHello3, nameof(GenerateHello3));
PrintHelloForArray(persons, GenerateHello4, nameof(GenerateHello4));

PrintAppealForArray(persons: persons, appealFunc: GenerateAppeal, label: $"{nameof(GenerateAppeal)} RU", lang: "RU");
PrintAppealForArray(persons: persons, appealFunc: GenerateAppeal, label: $"{nameof(GenerateAppeal)} EN", lang: "EN");
PrintAppealForArray(persons: persons, appealFunc: GenerateAppeal, label: $"{nameof(GenerateAppeal)} FR", lang: "FR");

PrintAppealForArray(persons: persons, appealFunc: GenerateAppeal2, label: $"{nameof(GenerateAppeal2)} RU", lang: "RU");
PrintAppealForArray(persons: persons, appealFunc: GenerateAppeal2, label: $"{nameof(GenerateAppeal2)} EN", lang: "EN");

PrintHelloForArray(persons, GenerateAnswerTo2, nameof(GenerateAnswerTo2));

PrintSatisfactionLevel(persons, nameof(PrintSatisfactionLevel));


string[] names = { "Анна", "Ольга", "Марина", "Карина", "Полина", "Юлия", "Валерия", 
    "Евгения", "Мишель", "Гульназ", "Гюльчатай" };

//foreach (var name in names)
//    if (name is ['А', .., 'а'])
//        Console.WriteLine(name);


//int[] numbers = { 1, 2, 3, 4, 5 };

//if (numbers is [1, ..])
//{

//}

//var n = numbers switch
//{
//    [1, 2, 3, 4, 5] => numbers,
//    _ => numbers
//};





#region GenerateHello

void PrintHelloForArray(IEnumerable<Person> persons, Func<Person, string> helloFunc, string label)
{
    Console.WriteLine($"------ Start {label} ----------");
    foreach (var person in persons)
        Console.WriteLine(helloFunc?.Invoke(person));
    Console.WriteLine($"------ End {label} ----------\r\n");
}

void PrintAppealForArray(IEnumerable<Person> persons, Func<Person, string, string> appealFunc, 
    string lang, string label)
{
    Console.WriteLine($"------ Start {label} ----------");
    foreach (var person in persons)
        Console.WriteLine(appealFunc?.Invoke(person, lang));
    Console.WriteLine($"------ End {label} ----------\r\n");
}

// паттерн классов
string GenerateHello(Person person) => person switch
{
    Student student => $"Student {person} with program {student.Program}",
    Teacher => $"Teacher: {person}",
    Person => $"Person: {person}",
    _ => String.Empty,
};

// Паттерн типов
string GenerateHello2(Person person) => person switch
{
    Student { Gender: Gender.Male, Phone: not null } => $"Male student",
    Student { Gender: Gender.Male } => $"Male student",
    Student { Gender: Gender.Female, Program: "Прикладная математика" } => "Девочка-математик",
    Student { Gender: Gender.Female } => $"Female student",
    Person { Email: not null } => "Пользователь с емейлом",
    Teacher => $"Teacher: {person}",
    Person { Gender: Gender.Male, FirstName: var firstName } => $"Hello, Mr. {firstName}",
    _ => String.Empty,
};

string GenerateHello3(Person person) => person switch
{
    { Gender: Gender.Male, FirstName: var firstName, SecondName: var secondName } => 
        $"Hello, Mr. {firstName} {secondName}",
    { Gender: Gender.Female, IsMarried: true } => $"Hello, Mrs. {person.FirstName} {person.SecondName}",
    { Gender: Gender.Female, IsMarried: false } => $"Hello, Ms. {person.FirstName} {person.SecondName}",
    _ => $"Hello, {person.FirstName} {person.SecondName}"
};

string GenerateHello4(Person person) => (person.Gender, person.IsMarried) switch
{
    (Gender.Male, _) => $"Hello, Mr. {person.FirstName} {person.SecondName}",
    (Gender.Female, true) => $"Hello, Mrs. {person.FirstName} {person.SecondName}",
    (Gender.Female, false) => $"Hello, Ms. {person.FirstName} {person.SecondName}",
    _ => $"Hello, {person.FirstName} {person.SecondName}"
};

#endregion

#region GenerateAppeal

string GenerateAppeal(Person person, string lang) => (person, lang) switch
{
    ( Teacher { Gender: Gender.Male }, "RU") => $"Уважаемый {person.FullName}",
    ( Student { Gender: Gender.Male }, "RU") => $"Уважаемый студент {person.FullName}",
    ( Teacher { Gender: Gender.Female }, "RU") => $"Уважаемая {person.FullName}",
    ( Student { Gender: Gender.Female }, "RU") => $"Уважаемая студентка {person.FullName}",
    ( Teacher { Gender: Gender.Male }, "EN") => $"Dear Mr. {person.FullName}",
    ( Teacher { Gender: Gender.Female, IsMarried: false }, "EN") => $"Dear Ms. {person.FullName}",
    ( Teacher { Gender: Gender.Female, IsMarried: true }, "EN") => $"Dear Mrs. {person.FullName}",
    ( Student, "EN") => $"Dear student {person.FullName}",
    ( Student, _ ) => $"Dear student {person.FullName}",
    ( Teacher, _ ) => $"Dear teacher {person.FullName}",
    _ => $"Dear {person.FullName}",
};

string GenerateAppeal2(Person person, string lang)
{
    if (lang == "RU")
    {
        if (person is Teacher)
        {
            if (person.Gender == Gender.Male)
                return $"Уважаемый {person.FullName}";
            else
                return $"Уважаемая {person.FullName}";
        }

        if (person is Student)
        {
            if (person.Gender == Gender.Male)
                return $"Уважаемый студент {person.FullName}";
            else
                return $"Уважаемая студентка {person.FullName}";
        }
    }
    
    if (lang == "EN")
    {
        if (person is Teacher)
        {
            if (person.Gender == Gender.Male)
                return $"Dear Mr. {person.FullName}";
            else
            {
                if (person.IsMarried)
                    return $"Dear Mrs. {person.FullName}";
                else
                    return $"Dear Ms. {person.FullName}";
            }
        }

        if (person is Student)
            return $"Dear student {person.FullName}";
    }

    return $"Dear {person.FullName}";
}

#endregion

#region GenerateAnswerTo
string GenerateAnswerTo(Person person) => person switch
{
    (_, _, Gender.Male, _, _, not null, not null, _) => 
        $"answer to Mr. {person.FullName} via email: {person.Email} or phone: {person.Phone}",
    (_, _, Gender.Male, _, _, null, not null, _) =>
        $"answer to Mr. {person.FullName} via email: {person.Email}",
    (_, _, Gender.Male, _, _, not null, null, _) =>
         $"answer to Mr. {person.FullName} via phone: {person.Phone}",
    (_, _, Gender.Male, _, _, null, null, _) =>
        $"Mr. {person.FullName} does not expect you to reply",
    (_, _, Gender.Female, _, _, not null, not null, true) =>
        $"answer to Mrs. {person.FullName} via email: {person.Email} or phone: {person.Phone}",
    (_, _, Gender.Female, _, _, not null, not null, false) =>
        $"answer to Ms. {person.FullName} via email: {person.Email} or phone: {person.Phone}",
    (_, _, _, _, _, _, _, _) => $"answer to {person.FullName}"
};

string GenerateAnswerTo2(Person person) => person switch
{
    { Email: not null, Phone: not null } =>
        $"answer to {person.Appeal} via email: {person.Email} or via phone: {person.Phone}",
    { Email: null, Phone: not null } =>
        $"answer to {person.Appeal} via phone: {person.Phone}",
    { Email: not null, Phone: null } =>
        $"answer to {person.Appeal} via email: {person.Email}",
    _ => $"no answer to {person.Appeal}",
};
#endregion

#region PrintSatisfactionLevel
void PrintSatisfactionLevel(IEnumerable<Person> persons, string label)
{
    Console.WriteLine($"------ Start {label} ----------");
    foreach (var person in persons)
        Console.WriteLine(
            $"{person.Appeal} born {person.DateOfBirth:yyyy.MM.dd} of Age {person.Age}: {person.SatisficationLevel}");
    Console.WriteLine($"------ End {label} ----------\r\n");
}
#endregion

