namespace PatternMatching;

internal enum Gender { Male, Female }

internal enum SatisficationLevel { VerySad, Sad, Ordinary, Happy, VeryHappy}

internal record Person(string FirstName, string SecondName, Gender Gender,
    DateOnly DateOfBirth, string? Address = null, string? Phone = null, string? Email = null, bool IsMarried = false)
{
    public string FullName => $"{FirstName} {SecondName}";

    public string ShortName => 
        (!String.IsNullOrEmpty(FirstName) && FirstName.Length > 0) ? 
            $"{SecondName} {FirstName.Substring(0, 1).ToUpper()}." : SecondName;

    public string Prefix
    {
        get
        {
            return this switch
            {
                { Gender: Gender.Male } => "Mr",
                { Gender: Gender.Female, IsMarried: true }  => "Mrs",
                { Gender: Gender.Female, IsMarried: false } => "Ms",
                _ => String.Empty
            };
        }
    }

    public string Appeal => $"{Prefix}. {FullName}";
    public string ShortAppeal => $"{Prefix}. {ShortName}";

    public int Age 
    { 
        get
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var month = DateOfBirth.Month;
            if (DateOfBirth.Day > today.Day)
                month--;

            var year = DateOfBirth.Year;
            if (month > today.Month)
                year--;

            return (today.Year - year);
        }
    }

    public virtual SatisficationLevel SatisficationLevel => SatisficationLevel.Happy;
}

internal record Student(string FirstName, string SecondName, Gender Gender,
    DateOnly DateOfBirth, string Program, int YearOfEnter, string? Address = null, string? Phone = null, 
    string? Email = null, bool IsMarried = false)
    : Person(FirstName, SecondName, Gender, DateOfBirth, Address, Phone, Email, IsMarried) 
{
    public override SatisficationLevel SatisficationLevel
    {
        get
        {
            return Age switch
            {
                < 18 => SatisficationLevel.VerySad,
                >= 18 and < 30 => SatisficationLevel.VeryHappy,
                >= 30 and < 50 => SatisficationLevel.Happy,
                >= 50 and < 100 => SatisficationLevel.Ordinary,
                >= 100 => SatisficationLevel.Sad,
            };
        }
    }
}


internal record Teacher(string FirstName, string SecondName, Gender Gender, DateOnly DateOfBirth, 
    string? Address = null, string? Phone = null, string? Email = null, bool IsMarried = false,
        double Salary = 0)
    : Person(FirstName, SecondName, Gender, DateOfBirth, Address: Address, Phone, Email, IsMarried)
{
    public override SatisficationLevel SatisficationLevel
    {
        get
        {
            return Salary switch
            {
                < 5000 => SatisficationLevel.VerySad,
                >= 5000 and < 20000 => SatisficationLevel.Sad,
                >= 20000 and < 40000 => SatisficationLevel.Ordinary,
                >= 40000 and > 100000 => SatisficationLevel.Happy,
                >= 100000 => SatisficationLevel.VeryHappy
            };
        }
    }
}
